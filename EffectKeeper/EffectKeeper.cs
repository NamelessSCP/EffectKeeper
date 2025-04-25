﻿namespace EffectKeeper;

using HarmonyLib;
    
#if EXILED
using Exiled.API.Features;
#else
using LabApi.Loader.Features.Plugins;
#endif

public class EffectKeeper : Plugin<Config>
{
    private Harmony _harmony;
        
    public static EffectKeeper Instance { get; private set; }
        
    public override string Name => "EffectKeeper";
    public override string Author => "@misfiy";
    public override Version Version => new(2, 0, 0);
    
    #if LABAPI
    public override string Description => "Allow keeping effects on role changes";
    public override Version RequiredApiVersion => new(LabApi.Features.LabApiProperties.CompiledVersion);
#endif

#if EXILED
    public override void OnEnabled()
#else
    public override void Enable()
#endif
    {
        if (!Config!.IsEnabled)
            return;

        Instance = this;
        _harmony = new Harmony("EffectKeeper-" + DateTime.Now);
        _harmony.PatchAll();
    }

#if EXILED
    public override void OnDisabled()
#else
    public override void Disable()
#endif
    {
        _harmony?.UnpatchAll(_harmony.Id);
        _harmony = null;
        Instance = null;
    }
}