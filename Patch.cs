namespace EffectKeeper.Patches;

using CustomPlayerEffects;
using Exiled.API.Extensions;
using Exiled.API.Features;
using HarmonyLib;
using PlayerRoles;

[HarmonyPatch(typeof(PlayerEffectsController), nameof(PlayerEffectsController.OnRoleChanged))]
public static class EffectsControllerPatch
{
     private static readonly Config config = Plugin.Instance.Config;
     public static bool Prefix(PlayerEffectsController __instance, ReferenceHub targetHub, PlayerRoleBase oldRole, PlayerRoleBase newRole)
     {
          if (targetHub != __instance._hub) return false;
          bool isDead = newRole.Team == Team.Dead && config.DeathDisablesEffects;

          foreach (StatusEffectBase statusEffectBase in __instance.AllEffects)
          {
               if (isDead) statusEffectBase.OnDeath(oldRole);
               if (!config.AllowedEffects.Contains(statusEffectBase.GetEffectType())) statusEffectBase.OnRoleChanged(oldRole, newRole);
          }
          return false;
     }
}