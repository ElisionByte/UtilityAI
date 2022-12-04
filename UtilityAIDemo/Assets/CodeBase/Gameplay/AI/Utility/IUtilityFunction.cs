using CodeBase.Gameplay.Heroes;
using CodeBase.Gameplay.Skills;

namespace CodeBase.Gameplay.AI.Utility
{
  public interface IUtilityFunction
  {
    bool AppliesTo(BattleSkill skill, IHero hero);
    float GetInput(BattleSkill skill, IHero hero, ISkillSolver skillSolver);
    float Score(float input, IHero hero);
    string Name { get; }
  }
}