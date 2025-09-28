using System;
using ByteSizeLib;

namespace ProcSpector.Lib
{
    public static class MiscExt
    {
        public static TR? TryGet<TI, TR>(TI obj, Func<TI, TR> func)
        {
            try
            {
                return func.Invoke(obj);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static ByteSize AsBytes(long value)
        {
            return ByteSize.FromBytes(value);
        }
    }
}