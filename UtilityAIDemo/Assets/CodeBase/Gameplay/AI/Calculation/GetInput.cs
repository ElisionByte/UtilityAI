using System.Linq;
using CodeBase.Gameplay.AI.Utility;
using CodeBase.Gameplay.Battle;
using CodeBase.Gameplay.Heroes;
using CodeBase.Gameplay.Skills;

namespace CodeBase.Gameplay.AI.Calculation
{
    //Status check for action
    public static class GetInput
    {
        private const int True = 1;
        private const int False = 0;

        public static float TargetHpPercentage(BattleSkill skill, IHero target, ISkillSolver skillSolver) =>
          target.State.HpPercentage;

        public static float PercentageDamage(BattleSkill skill, IHero target, ISkillSolver skillSolver)
        {
            float damage = skillSolver.CalculateSkillValue(skill.CasterId, skill.TypeId, target.Id);
            return damage / target.State.MaxHp;
        }

        public static float IsKillingBlow(BattleSkill skill, IHero target, ISkillSolver skillSolver)
        {
            float damage = skillSolver.CalculateSkillValue(skill.CasterId, skill.TypeId, target.Id);
            return damage >= target.State.CurrentHp
              ? True
              : False;
        }

        public static float HealPercentage(BattleSkill skill, IHero target, ISkillSolver skillSolver) =>
          skillSolver.CalculateSkillValue(skill.CasterId, skill.TypeId, target.Id);

        public static float InitiativeBurn(BattleSkill skill, IHero target, ISkillSolver skillSolver)
        {
            float burn = skillSolver.CalculateSkillValue(skill.CasterId, skill.TypeId, target.Id);
            return burn / target.State.MaxInitiative;
        }

        public static float TargetUltimateIsReady(BattleSkill skill, IHero target, ISkillSolver skillSolver) =>
          target.State.SkillStates.Last().IsReady
            ? True
            : False;
    }

    public static class MobInput
    {
        public static float BasicAtack(MobAction action, IMob opponent)
        {
            return action.Value;
        }

        public static float CriticalAtack(MobAction action, IMob opponent)
        {
            return action.Value;
        }
    }
}