namespace Exerussus._1Lab.Scripts.Data.GamesConfigurations.Luguages
{
    public interface ILanguage
    {
        string IsFalling { get; }
        string TouchCollider { get; }
        string AutoRun { get; }
        string None { get; }
        string True { get; }
        string False { get; }
        string Idle { get; }
        string Fall { get; }
        string Jump { get; }
        string Run { get; }
        string Tags { get; }
        public string FrameDelay { get; }
    }
}