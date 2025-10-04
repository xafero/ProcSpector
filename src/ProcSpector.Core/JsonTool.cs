using System.IO;
using System.Text.Json;

namespace ProcSpector.Core
{
    public static class JsonTool
    {
        public static void WriteJson(this StreamWriter writer, object obj)
        {
            var json = JsonSerializer.Serialize(obj);
            writer.WriteLine(json);
            writer.Flush();
        }
    }
}