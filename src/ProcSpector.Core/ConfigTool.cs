using Microsoft.Extensions.Configuration;

namespace ProcSpector.Core
{
    public static class ConfigTool
    {
        public static T ReadJsonObj<T>() where T : new()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true);

            var config = builder.Build();

            var settings = new T();
            config.Bind(settings);

            return settings;
        }
    }
}