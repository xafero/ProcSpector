namespace ProcSpector.API
{
    public interface IProcess
    {
        int Id { get; }
        string Name { get; }
        
        void Kill();
        void CreateMemSave();
        void CreateScreenShot();
        void CreateMiniDump();
        void OpenFolder();
    }
}