using PlayerRoles;

namespace EffectKeeper;

using Exiled.API.Enums;
using Exiled.API.Interfaces;
using System.ComponentModel;

public sealed class Config : IConfig
{
     public bool IsEnabled { get; set; } = true;
     public bool Debug { get; set; } = false;

     [Description("What effects shouldn't get removed")]
     public List<EffectType> AllowedEffects { get; set; } = new()
     {
          EffectType.Scp207,
          EffectType.Invisible,
          EffectType.Scp1853
     };

     [Description("A list of effect categories that cannot be removed from player")]
     public List<EffectCategory> AllowedCategories { get; set; } = new()
     {
          EffectCategory.Movement,
          EffectCategory.Positive
     };
     
     [Description("List of roles that will disable effects upon swapping to/from. Overrides previous configs.")]
     public List<RoleTypeId> CancellingRoles { get; set; } = new()
     {
          RoleTypeId.Overwatch,
          RoleTypeId.Spectator,
     };
}