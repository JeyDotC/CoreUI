
using System;
using System.IO;

namespace CoreUI
{
    public interface ICoreUIApp : IDisposable
    {
        public ICoreUIApp WithFont(FileInfo fontFile, string fontName);

        public ICoreUIWindow CreateWindow(string title, int width = 800, int heitght = 600);

        public void WaitForExit();
    }
}
/*
using(var app = new CoreUIApp()){
    
    app.WaitForExit();
}
     */