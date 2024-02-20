namespace EffectKeeper;

using Exiled.API.Features;
using HarmonyLib;

public class Plugin : Plugin<Config>
{
     public static Plugin Instance { get; private set; } = null!;

     public override string Name => "EffectKeeper";
     public override string Author => "@misfiy";
     public override Version Version => new(1, 1, 0);
     public override Version RequiredExiledVersion => new(8, 8, 0);

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