using CodeBase.Gameplay.Battle;
using CodeBase.Gameplay.Heroes;
using CodeBase.Gameplay.Skills;
using CodeBase.StaticData.Skills;

namespace CodeBase.Gameplay.AI.Utility
{
    public interface IUtilityFunction
    {
        bool AppliesTo(BattleSkill skill, IHero hero);
        float GetInput(BattleSkill skill, IHero hero, ISkillSolver skillSolver);
        float Score(float input, IHero hero);
        SkillType SkillType { get; }
    }

    public interface IMobUtilityFunction
    {
        bool When(MobAction action, IMob mob);
        float Input(MobAction action, IMob mob);
        float Score(float input, IMob mob);
        MobSkillType SkillType { get; }
    }
}