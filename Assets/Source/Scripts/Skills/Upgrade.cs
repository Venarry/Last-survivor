using Configs;
using Language;

namespace Skills
{
    public abstract class Upgrade
    {
        protected LanguageProvider LanguageProvider;

        public abstract UpgradeType UpgradeType { get; }
        public virtual SkillTickType SkillTickType { get; }
        public virtual bool HasCooldown { get; }
        public virtual int MaxLevel { get; }
        public int CurrentLevel { get; private set; }

        protected Upgrade(LanguageProvider languageProvider)
        {
            LanguageProvider = languageProvider;
        }

        public void SetLevel(int level)
        {
            CurrentLevel = level;
            OnLevelChange();
        }

        public abstract void Apply();

        public abstract void Disable();

        public bool TryIncreaseLevel()
        {
            if (CurrentLevel >= MaxLevel)
            {
                return false;
            }

            CurrentLevel++;
            OnLevelChange();

            return true;
        }

        public virtual void IncreaseTimeLeft()
        {
        }

        public abstract string GetUpLevelDescription();

        protected virtual void OnLevelChange()
        {
        }

        protected string Decorate(string text) =>
            $"{GameParameters.TextColorStart}{text}{GameParameters.TextColorEnd}";
    }
}