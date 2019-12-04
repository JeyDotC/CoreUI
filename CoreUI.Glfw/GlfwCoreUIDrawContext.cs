using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using CoreUI.Styles;
using GLFW;
using SkiaSharp;
using GL = GLFW.Glfw;

namespace CoreUI.Glfw
{
    internal class GlfwCoreUIDrawContext : ICoreUIDrawContext
    {
        private readonly NativeWindow _hostWindow;
        private readonly GRContext _context;

        private SKSurface _surface;
        private SKCanvas _canvas;
        
        public PaintStyle ClearStyle {
            get => _currentState.ClearStyle;
            set => _currentState.ClearStyle = value;
        }

        public PaintStyle FillStyle
        {
            get => _currentState.FillStyle;
            set => _currentState.FillStyle = value;
        }

        public PaintStyle StrokeStyle
        {
            get => _currentState.StrokeStyle;
            set => _currentState.StrokeStyle = value;
        }

        public int LineWidth
        {
            get => _currentState.LineWidth;
            set => _currentState.LineWidth = value;
        }

        public FontStyles Font
        {
            get => _currentState.Font;
            set => _currentState.Font = value;
        }

        private DrawingContextState _currentState = new DrawingContextState();

        private DrawingContextState _savedState;

        public GlfwCoreUIDrawContext(NativeWindow nativeWindow)
        {
            _hostWindow = nativeWindow;
            _context = GenerateSkiaContext(_hostWindow);
            _surface = GenerateSkiaSurface(_context, nativeWindow.ClientSize);
            _canvas = _surface.Canvas;
            _hostWindow.SizeChanged += RegenerateSurface;
        }

        private void RegenerateSurface(object sender, SizeChangeEventArgs e)
        {
            _surface.Dispose();
            _canvas.Dispose();

            _surface = GenerateSkiaSurface(_context, _hostWindow.ClientSize);
            _canvas = _surface.Canvas;
        }

        private static GRContext GenerateSkiaContext(NativeWindow nativeWindow)
        {
            var nativeContext = GetNativeContext(nativeWindow);
            var glInterface = GRGlInterface.AssembleGlInterface(nativeContext, (contextHandle, name) => GL.GetProcAddress(name));
            return GRContext.Create(GRBackend.OpenGL, glInterface);
        }

        private static object GetNativeContext(NativeWindow nativeWindow)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Native.GetWglContext(nativeWindow);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // XServer
                return Native.GetGLXContext(nativeWindow);
                // Wayland
                //return Native.GetEglContext(nativeWindow);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return Native.GetNSGLContext(nativeWindow);
            }

            throw new PlatformNotSupportedException();
        }

        private static SKSurface GenerateSkiaSurface(GRContext skiaContext, Size surfaceSize)
        {
            var frameBufferInfo = new GRGlFramebufferInfo((uint)new UIntPtr(0), GRPixelConfig.Rgba8888.ToGlSizedFormat());
            var backendRenderTarget = new GRBackendRenderTarget(surfaceSize.Width,
                                                                surfaceSize.Height,
                                                                0,
                                                                8,
                                                                frameBufferInfo);
            return SKSurface.Create(skiaContext, backendRenderTarget, GRSurfaceOrigin.BottomLeft, SKImageInfo.PlatformColorType);
        }


        public ICoreUIDrawContext BeginPath()
        {
            _currentState.Path = new SKPath();
            return this;
        }

        public ICoreUIDrawContext Clear()
        {
            _canvas.Clear(ClearStyle.Color.ToSKColor());
            return this;
        }

        public ICoreUIDrawContext ClearRect(Rectangle rectangle)
        {
            var oldFillStyle = FillStyle;
            FillStyle = ClearStyle;
            this.FillRect(rectangle);
            FillStyle = oldFillStyle;

            return this;
        }

        public ICoreUIDrawContext ClosePath()
        {
            _currentState.Path?.Close();
            return this;
        }

        public ICoreUIDrawContext Fill()
        {
            if (FillStyle.Type == PaintStyleType.None)
            {
                return this;
            }

            using (var paint = FillStyle.ToSKPaint())
            {
                paint.IsStroke = false;
                paint.IsAntialias = true;

                _canvas.DrawPath(_currentState.Path, paint);
            }

            return this;
        }

        public ICoreUIDrawContext FillText(string text, Point position)
        {
            if (FillStyle.Type == PaintStyleType.None)
            {
                return this;
            }

            using (var typeface = SKTypeface.FromFamilyName(Font.FontFamily))
            {
                using (var paint = FillStyle.ToSKPaint())
                {
                    paint.TextSize = Font.FontSize;
                    paint.IsAntialias = true;
                    paint.IsStroke = false;
                    paint.Typeface = typeface;

                    _canvas.DrawText(text, position.X, position.Y - paint.FontMetrics.Ascent, paint);
                }
            }

            return this;
        }

        public Size MeasureText(string text)
        {
            using (var typeface = SKTypeface.FromFamilyName(Font.FontFamily))
            {
                using (var paint = new SKPaint
                {
                    TextSize = Font.FontSize,
                    IsStroke = false,
                    Typeface = typeface,
                })
                {
                    var metrics = paint.FontMetrics;
                    var width = paint.MeasureText(text);
                    var height = -metrics.Ascent + metrics.Descent;
                    return new Size((int)width, (int)height);
                }

            }
        }

        public ICoreUIDrawContext MoveTo(Point point)
        {
            _currentState.Path.MoveTo(point.X, point.Y);
            return this;
        }

        public ICoreUIDrawContext LineTo(Point point)
        {
            _currentState.Path.LineTo(point.X, point.Y);
            return this;
        }

        public ICoreUIDrawContext Restore()
        {
            _currentState = _savedState ?? _currentState;
            _savedState = null;
            return null;
        }

        public ICoreUIDrawContext Save()
        {
            _savedState = new DrawingContextState(_currentState);
            return this;
        }

        public ICoreUIDrawContext Stroke()
        {
            if (FillStyle.Type == PaintStyleType.None)
            {
                return this;
            }

            using (var paint = StrokeStyle.ToSKPaint())
            {
                paint.IsStroke = true;
                paint.StrokeWidth = LineWidth;
                paint.IsAntialias = true;

                _canvas.DrawPath(_currentState.Path, paint);
            }
            return this;
        }

        internal void Flush() => _canvas.Flush();

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _surface?.Dispose();
                    _context?.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
