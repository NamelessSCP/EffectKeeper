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
     public List<EffectCategory> AllowedCategories { get; set; } = new()
     {
          EffectCategory.Movement,
          EffectCategory.Positive
     };
     [Description("Whether or not dying should disable all effects (overrides AllowedEffects)")]
     public bool DeathDisablesEffects { get; set; } = true;
}