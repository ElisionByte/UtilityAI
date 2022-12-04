using System.Collections.Generic;
using CodeBase.Gameplay.AI.Calculation;

namespace CodeBase.Gameplay.AI.Utility
{
  public class Brains
  {
    private AIInput Input => new AIInput();

    private Convolutions _convolutions;

    public Brains()
    {
      _convolutions = new()
      {
        {When.SkillIsDamage, GetInput.PercentageDamage, Score.ScaleBy(100), "Basic Damage"},
        {When.SkillIsDamage, GetInput.IsKillingBlow, Score.IfTrueThen(+150), "Killing Blow"},
        // {When.SkillIsDamage, Input.TargetHpPercentage, Score.FocusDamage, "Focus Damage"},
        {When.SkillIsBasicAttack, GetInput.IsKillingBlow, Score.IfTrueThen(+30), "Basic Skill Killing Blow"},
      
        {When.SkillIsHeal, GetInput.HealPercentage, Score.CullByTargetHp, "Heal"},
        
        {When.SkillIsInitiativeBurn, GetInput.InitiativeBurn, Score.CullByTargetInitiative(scaleBy: 50f, cullThreshold: 0.25f), "Initiative Burn"},
        {When.SkillIsInitiativeBurn, GetInput.TargetUltimateIsReady, Score.IfTrueThen(+30), "Initiative Burn Ultimate Is Ready"},
      };
    }

    public ICollection<IUtilityFunction> GetUtilityFunctions()
    {
      return _convolutions;
    }
  }
}