namespace EffectKeeper;

using Exiled.API.Features;
using Exiled.API.Enums;
using HarmonyLib;

public class Plugin : Plugin<Config>
{
     public override string Name => "EffectKeeper";
     public override string Author => "@misfiy";
     public override Version Version => new(1, 0, 6);
     public override Version RequiredExiledVersion => new(8, 3, 7);
     public static Plugin Instance { get; private set; } = null!;
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