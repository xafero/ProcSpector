using System;

namespace ProcSpector.API
{
    [Flags]
    public enum FeatureFlags : long
    {
        None = 0,

        GetUserInfo = 1L << 0,

        GetProcesses = 1L << 1,

        GetModules = 1L << 2,

        GetWindows = 1L << 3,

        GetMemory = 1L << 4,

        CopyScreen = 1L << 5,

        SaveMemory = 1L << 6
    }
}