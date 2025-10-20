using System;
using Avalonia;
using ProcSpector.Config;
using ProcSpector.Core;
using ProcSpector.Core.Plugins;
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
            InitPlug();
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

        private static void InitPlug()
        {
            _ = PluginTool.Plugins.Value;
            PluginTool.Context.S = Factory.Platform.Value.System;
        }
    }
}