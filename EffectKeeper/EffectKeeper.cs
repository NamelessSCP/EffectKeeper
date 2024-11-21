namespace EffectKeeper
{
    using HarmonyLib;
    
#if EXILED
    using Exiled.API.Features;
#else
    using PluginAPI.Core.Attributes;
#endif
    
#if EXILED 
    public class EffectKeeper : Plugin<Config>
#else
    public class EffectKeeper
#endif
    {
        private Harmony harmony;
        
        public static EffectKeeper Instance { get; private set; }
        
#if EXILED
        public override string Name { get; } = "EffectKeeper";
        public override string Author { get; } = "@misfiy";
        public override Version Version { get; } = new(1, 2, 0);

        public override void OnEnabled()
#else
        [PluginConfig]
        public Config Config; 
        
        [PluginEntryPoint("EffectKeeper", "1.2.0", "Keeps your effects on escape,", "@misfiy")]
        public void OnEnabled()
#endif
        {
            #if !EXILED
            if (!Config.IsEnabled)
                return;
            #endif
            
            Instance = this;
            harmony = new("EffectKeeper-" + DateTime.Now);
            harmony.PatchAll();
        }
    }
}