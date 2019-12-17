namespace FinalEngine.Rendering
{
    public interface ISwapChain
    {
        int SyncInterval { get; set; }

        void Present();
    }
}