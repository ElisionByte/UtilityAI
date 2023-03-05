using CodeBase.Gameplay.Battle;
using CodeBase.StaticData.Skills;

namespace CodeBase.Gameplay.Skills
{
  public interface ISkillSolver
  {
    void ProcessHeroAction(HeroAction heroAction);
    void SkillDelaysTick();
    float CalculateSkillValue(string casterId, SkillTypeId skillTypeId, string targetId);
  }
}