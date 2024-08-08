namespace Exerussus._1Lab.Scripts.Data.GamesConfigurations.Luguages
{
    public class Ru : ILanguage
    {
        public string IsFalling { get; } = "Падает";
        public string TouchCollider { get; } = "Касающийся коллайдер";
        public string AutoRun { get; } = "Автоматический запуск";
        public string None { get; } = "Отсутствует";
        public string True { get; } = "Да";
        public string False { get; } = "Нет";
        public string Idle { get; } = "Покой";
        public string Fall { get; } = "Падение";
        public string Jump { get; } = "Прыжок";
        public string Run { get; } = "Бег";
        public string Tags { get; } = "Тэги";
        public string FrameDelay { get; } = "Задержка между кадрами";
    }
}