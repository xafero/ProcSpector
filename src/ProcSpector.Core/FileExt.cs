using System;
using System.IO;
using ProcSpector.API;

namespace ProcSpector.Core
{
    public static class FileExt
    {
        public static string? Save(this IFile? file)
        {
            if (file?.FullName?.TrimOrNull() is not { } filePath ||
                file.Bytes is not { } bytes)
                return null;

            var dir = Environment.CurrentDirectory;
            var path = Path.Combine(dir, filePath);
            File.WriteAllBytes(path, bytes);
            return path;
        }

        public static string[] List(string path, string pattern)
        {
            const SearchOption o = SearchOption.AllDirectories;
            var res = Directory.GetFiles(path, pattern, o);
            return res;
        }

        public static FileTmp CreateTmp()
        {
            return new FileTmp();
        }
    }
}