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
        private SKPath _path;

        public Color FillStyle { get; set; } = Color.White;
        public Color StrokeStyle { get; set; } = Color.Black;
        public int LineWidth { get; set; } = 1;
        public FontStyles Font { get; set; } = FontStyles.Default;

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
            _path = new SKPath();
            return this;
        }

        public ICoreUIDrawContext Clear()
        {
            _canvas.Clear(Color.White.ToSKColor());
            return this;
        }

        public ICoreUIDrawContext ClearRect(Rectangle rectangle)
        {
            throw new NotImplementedException();
        }

        public ICoreUIDrawContext ClosePath()
        {
            _path?.Close();
            return this;
        }

        public ICoreUIDrawContext Fill()
        {
            using (var paint = new SKPaint
            {
                Color = FillStyle.ToSKColor(),
                IsStroke = false,
                IsAntialias = true
            })
            {
                _canvas.DrawPath(_path, paint);
            }

            return this;
        }

        public ICoreUIDrawContext FillText(string text, Point position)
        {
            using (var typeface = SKTypeface.FromFamilyName(Font.FontFamily))
            {
                using (var paint = new SKPaint
                {
                    TextSize = Font.FontSize,
                    Color = FillStyle.ToSKColor(),
                    IsAntialias = true,
                    IsStroke = false,
                    Typeface = typeface,
                })
                {
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
                    Color = FillStyle.ToSKColor(),
                    IsAntialias = true,
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
            _path.MoveTo(point.X, point.Y);
            return this;
        }

        public ICoreUIDrawContext LineTo(Point point)
        {
            _path.LineTo(point.X, point.Y);
            return this;
        }

        public ICoreUIDrawContext Restore()
        {
            throw new NotImplementedException();
        }

        public ICoreUIDrawContext Save()
        {
            throw new NotImplementedException();
        }

        public ICoreUIDrawContext Stroke()
        {
            using (var paint = new SKPaint
            {
                Color = StrokeStyle.ToSKColor(),
                IsStroke = true,
                StrokeWidth = LineWidth,
                IsAntialias = true
            })
            {
                _canvas.DrawPath(_path, paint);
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
