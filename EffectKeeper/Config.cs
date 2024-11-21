namespace EffectKeeper
{
#if EXILED
    using Exiled.API.Interfaces;
    using Exiled.API.Enums;
#else
    using CustomPlayerEffects;
#endif
    
#if EXILED
    public class Config : IConfig
#else
    public class Config
#endif
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; }

        public bool DeathCancels { get; set; } = true;

#if EXILED
        public List<EffectType> AllowedEffects { get; set; } = new()
        {
            EffectType.Scp207,
        };

        public List<EffectCategory> AllowedCategories { get; set; } = new()
        {
            EffectCategory.Movement,
            EffectCategory.Positive,
        };
#else
        public List<string> AllowedEffects { get; set; } = new()
        {
            "Scp207",
        };
        
        public List<StatusEffectBase.EffectClassification> AllowedCategories { get; set; } = new()
        {
            StatusEffectBase.EffectClassification.Positive,
        };
#endif
    }
}