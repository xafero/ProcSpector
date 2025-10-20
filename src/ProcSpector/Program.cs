using System;
using ProcSpector.Impl;
using Avalonia;
using ProcSpector.Config;
using ProcSpector.Core;
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
            Env.Cfg = ConfigTool.ReadJsonObj<AppSettings>();
            if (Env.Cfg.Client?.Address != null)
                Factory.ClientCfg = Env.Cfg.Client;
        }
    }
}