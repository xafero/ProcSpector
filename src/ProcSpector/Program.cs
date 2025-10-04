using System;
using Avalonia;
using ProcSpector.Config;
using ProcSpector.Core;
using ProcSpector.Impl;
using ProcSpector.Tools;

// ReSharper disable ClassNeverInstantiated.Global

namespace ProcSpector
{
    internal sealed class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            InitCfg();
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();

        private static void InitCfg()
        {
            Factory.ClientCfg = (Env.Cfg = ConfigTool.ReadJsonObj<AppSettings>()).Client;
        }
    }
}