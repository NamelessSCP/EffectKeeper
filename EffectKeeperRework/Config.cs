namespace EffectKeeperRework
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
        public List<EffectType> KeepEffects { get; set; } = new()
        {
            EffectType.Scp207,
        };

        public List<EffectCategory> KeepCategories { get; set; } = new()
        {
            EffectCategory.Movement,
            EffectCategory.Positive,
        };
#else
        public List<string> KeepEffects { get; set; } = new()
        {
            "Scp207",
        };
        
        public List<StatusEffectBase.EffectClassification> KeepCategories { get; set; } = new()
        {
            StatusEffectBase.EffectClassification.Positive,
        };
#endif
    }
}