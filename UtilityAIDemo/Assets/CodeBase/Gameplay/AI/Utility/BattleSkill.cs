using CodeBase.StaticData.Skills;

namespace CodeBase.Gameplay.AI.Utility
{
  public class BattleSkill
  {
    public string CasterId;
    public SkillTypeId TypeId;
    public SkillKind Kind;
    public TargetType TargetType;
    public bool IsSingleTarget => TargetType is TargetType.Ally or TargetType.Enemy or TargetType.Self;
    public float MaxCooldown;
  }
}