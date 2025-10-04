using System.Net;

namespace ProcSpector.Core
{
    public static class NetTool
    {
        public const string Any = "Any";

        public static IPAddress? Parse(string? rawAddress)
        {
            if (rawAddress.TrimOrNull() is not { } address)
                return null;

            if (address.Equals(Any))
                return IPAddress.Any;

            var res = IPAddress.Parse(address);
            return res;
        }
    }
}