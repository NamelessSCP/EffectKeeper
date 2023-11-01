namespace EffectKeeper.Patches;

using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using HarmonyLib;
using PlayerRoles;

[HarmonyPatch(typeof(PlayerEffectsController), nameof(PlayerEffectsController.OnRoleChanged))]
internal static class EffectsControllerPatch
{
	private static Config config = Plugin.Instance.Config;
	private static bool Prefix(PlayerEffectsController __instance, ReferenceHub targetHub, PlayerRoleBase oldRole, PlayerRoleBase newRole)
	{
		if (targetHub != __instance._hub) return false;
		
		bool isDead = (oldRole.Team == Team.Dead || newRole.Team == Team.Dead) && config.DeathDisablesEffects;
		
		foreach (StatusEffectBase statusEffectBase in __instance.AllEffects)
		{
			if (isDead)
			{
				statusEffectBase.OnDeath(oldRole);
				continue;
			}

			EffectType effect;
			
			try
			{
				effect = statusEffectBase.GetEffectType();
			}
			catch
			{
				Log.Warn($"Invalid EffectType encountered with {statusEffectBase}!");
				continue;
			}
			
			bool shouldRemove = !config.AllowedEffects.Contains(effect) &&
			                    !config.AllowedCategories.Any(c => effect.GetCategories().HasFlag(c));

			if (shouldRemove)
				statusEffectBase.OnRoleChanged(oldRole, newRole);
		}
		return false;
	}
}