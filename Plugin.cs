namespace EffectKeeper;

using Exiled.API.Features;
using Exiled.API.Enums;
using HarmonyLib;

public class Plugin : Plugin<Config>
{
     public override string Name => "EffectKeeper";
     public override string Prefix => Name;
     public override string Author => "@misfiy";
     public override PluginPriority Priority => PluginPriority.Default;
     public static Plugin Instance;
     private Harmony harmony;
     public override void OnEnabled()
     {
          Instance = this;
          try
          {
               Log.Debug("Beginning patch");
               harmony = new Harmony("EffectKeeper");
               harmony.PatchAll();
          }
          catch (Exception e)
          {
               Log.Error($"Failed to patch: " + e);
               harmony.UnpatchAll();
          }

          base.OnEnabled();
     }

     public override void OnDisabled()
     {
          Instance = null!;
          harmony.UnpatchAll();
          base.OnDisabled();
     }
}