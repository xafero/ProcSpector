using System;
using System.Linq;

namespace ProcSpector.Core
{
    public static class EnumTool
    {
        public static T[] ParseArg<T>(string arg)
            where T : struct, Enum
        {
            var args = arg.Split(" ");
            var codes = args.Select(a =>
                Enum.Parse<T>(a, ignoreCase: true)
            ).ToArray();
            return codes;
        }
    }
}