using System.Diagnostics;
using ProcSpector.API;

namespace ProcSpector.Impl.Net
{
    public sealed class StdProc : IProcess
    {
        public Process Proc { get; }

        public StdProc(Process process)
        {
            Proc = process;
        }

        public int Id => Proc.Id;
        public string Name => Proc.ProcessName;

        public void Kill()
        {
            throw new System.NotImplementedException();
        }

        public void CreateMemSave()
        {
            throw new System.NotImplementedException();
        }

        public void CreateScreenShot()
        {
            throw new System.NotImplementedException();
        }

        public void CreateMiniDump()
        {
            throw new System.NotImplementedException();
        }

        public void OpenFolder()
        {
            throw new System.NotImplementedException();
        }
    }
}