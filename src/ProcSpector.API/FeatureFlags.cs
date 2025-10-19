using System;

namespace ProcSpector.API
{
    [Flags]
    public enum FeatureFlags : long
    {
        None = 0,

        GetProcesses = 1L << 0,

        B = 1L << 1,
        C = 1L << 2,
        D = 1L << 3,
        E = 1L << 4,
        F = 1L << 5
    }
}