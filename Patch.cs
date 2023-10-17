namespace EffectKeeper.Patches;

using CustomPlayerEffects;
using Exiled.API.Enums;
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
		bool isDead = oldRole.Team == Team.Dead && config.DeathDisablesEffects;

		foreach (StatusEffectBase statusEffectBase in __instance.AllEffects)
		{
			if (isDead)
			{
				statusEffectBase.OnDeath(oldRole);
				continue;
			}

			EffectType effect = statusEffectBase.GetEffectType();
			bool shouldRemove = !config.AllowedEffects.Contains(effect) &&
			    !config.AllowedCategories.Any(c => effect.GetCategories().HasFlag(c));

			if (shouldRemove)
				statusEffectBase.OnRoleChanged(oldRole, newRole);
		}
		return false;
	}
}