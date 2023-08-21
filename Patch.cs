namespace EffectKeeper.Patches;

using CustomPlayerEffects;
using Exiled.API.Extensions;
using HarmonyLib;
using PlayerRoles;

[HarmonyPatch(typeof(PlayerEffectsController), nameof(PlayerEffectsController.OnRoleChanged))]
public static class EffectsControllerPatch
{
     private static readonly Config config = Plugin.Instance.Config;
     public static bool Prefix(PlayerEffectsController __instance, ReferenceHub targetHub, PlayerRoleBase oldRole, PlayerRoleBase newRole)
     {
          if (targetHub != __instance._hub) return false;
          if (oldRole.Team != Team.Dead && newRole.Team == Team.Dead && config.DeathDisablesEffects)
          {
               __instance.DisableAllEffects();
               return false;
          }

          foreach (StatusEffectBase statusEffectBase in __instance.AllEffects)
          {
               if (!config.AllowedEffects.Contains(statusEffectBase.GetEffectType())) statusEffectBase.DisableEffect();
          }
          return false;
     }
}