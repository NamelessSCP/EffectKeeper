﻿namespace EffectKeeperRework 
{
    using CustomPlayerEffects;
    using HarmonyLib;
    using PlayerRoles;
    
#if EXILED
    using Exiled.API.Enums;
    using Exiled.API.Extensions;
#endif
    
    [HarmonyPatch(typeof(PlayerEffectsController), nameof(PlayerEffectsController.OnRoleChanged))]
    public static class EffectPatch
    {
        private static Config config => EffectKeeper.Instance.Config;

        private static bool Prefix(PlayerEffectsController __instance,
            ReferenceHub targetHub,
            PlayerRoleBase oldRole,
            PlayerRoleBase newRole)
        {
            if (targetHub != __instance._hub)
                return false;

            bool isDeath = config.DeathCancels && (oldRole.Team == Team.Dead || newRole.Team == Team.Dead);

            foreach (StatusEffectBase statusEffectBase in __instance.AllEffects)
            {
                if (isDeath)
                {
                    statusEffectBase.OnDeath(oldRole);
                    continue;
                }

                bool shouldCancel = false;

#if EXILED
                if (!statusEffectBase.TryGetEffectType(out EffectType effectType))
                    continue;

                EffectCategory categories = effectType.GetCategories();
                if (!config.KeepEffects.Contains(effectType) && !config.KeepCategories.Any(c => categories.HasFlag(c)))
                    shouldCancel = true;
#else
                if (!config.KeepEffects.Contains(statusEffectBase.name))
                    shouldCancel = true;
                
                if (!config.KeepCategories.Contains(statusEffectBase.Classification))
                    shouldCancel = true;
#endif
                if (shouldCancel)
                    statusEffectBase.OnRoleChanged(oldRole, newRole);
            }

            return false;
        }
    }
}