using System;
using ProcSpector.API;

namespace ProcSpector.Impl.Net.Data
{
    public sealed class NetUser : IUserInfo
    {
        public string Host => Environment.MachineName;
        public string Name => Environment.UserName;
    }
}