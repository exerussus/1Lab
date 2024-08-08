using System;
using Exerussus._1Lab.Scripts.Data.GamesConfigurations.Luguages;

namespace Exerussus._1Lab.Scripts.Data.GamesConfigurations
{
    [Serializable]
    public class LanguageLibrary
    {
        public bool isInitialized;
        public ILanguage Language { get; private set; }

        public void Initialize(LanguageType languageType)
        {
            isInitialized = true;
            switch (languageType)
            {
                case LanguageType.En:
                    Language = new En();
                    break;
                case LanguageType.Ru:
                    Language = new Ru();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(languageType), languageType, null);
            }
        }
    }
}