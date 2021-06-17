namespace Audio
{
    public interface ISoundPlayer
    {
        void Play(string soundName);
        void PlayAndStop(string soundName);
        void Stop();

        float Pitch { get; set; }
        float Volume { get; set; }
        bool Mute { get; set; }
        bool Loop { get; set; }
    }
}