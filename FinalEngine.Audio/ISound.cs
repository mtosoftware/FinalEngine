namespace FinalEngine.Audio;

using FinalEngine.Resources;

public interface ISound : IResource
{
    bool IsLooping { get; set; }

    float Volume { get; set; }

    void Pause();

    void Start();

    void Stop();
}
