using System;

namespace ProcSpector.Lib
{
    public static class MiscExt
    {
        public static T? TryGet<T>(Func<T> func)
        {
            try
            {
                return func.Invoke();
            }
            catch (Exception ex)
            {
                return default;
            }
        }
    }
}