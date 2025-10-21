using System;
using System.Diagnostics;

namespace ProcSpector.Impl.Net.Tools
{
    public static class ProcExt
    {
        public static ProcessModule? TryMainModule(this Process item)
        {
            try
            {
                return item.MainModule;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}