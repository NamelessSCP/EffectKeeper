namespace EffectKeeper;

using Exiled.API.Features;
using Exiled.API.Enums;
using HarmonyLib;

public class Plugin : Plugin<Config>
{
     public override string Name => "EffectKeeper";
     public override string Prefix => Name;
     public override string Author => "@misfiy";
     public override Version Version => new(1, 0, 5);
     public override PluginPriority Priority => PluginPriority.Default;
     public static Plugin Instance;
     private Harmony harmony = new("EffectKeeper");
     public override void OnEnabled()
     {
          Instance = this;
          harmony.PatchAll();

          base.OnEnabled();
     }

     public override void OnDisabled()
     {
          harmony.UnpatchAll();
          Instance = null!;
          base.OnDisabled();
     }
}