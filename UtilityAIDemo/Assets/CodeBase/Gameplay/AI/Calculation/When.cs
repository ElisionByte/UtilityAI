using CodeBase.Gameplay.AI.Utility;
using CodeBase.Gameplay.Battle;
using CodeBase.Gameplay.Heroes;
using CodeBase.StaticData.Skills;

namespace CodeBase.Gameplay.AI.Calculation
{
    //Condition for some action
    public static class When
    {
        public static bool SkillIsDamage(BattleSkill skill, IHero hero) =>
          skill.Kind == SkillKind.Damage;

        public static bool SkillIsBasicAttack(BattleSkill skill, IHero hero) =>
          skill.Kind == SkillKind.Damage && skill.MaxCooldown == 0;

        public static bool SkillIsHeal(BattleSkill skill, IHero target) =>
          skill.Kind == SkillKind.Heal;

        public static bool SkillIsInitiativeBurn(BattleSkill skill, IHero target) =>
          skill.Kind == SkillKind.InitiativeBurn;
    }

    //Condition for some action
    public static class MobWhen
    {
        public static bool SkillIsDamage(MobAction action, IMob opponent) =>
          action.ActionType == MobSkillType.Damage;
    }
}