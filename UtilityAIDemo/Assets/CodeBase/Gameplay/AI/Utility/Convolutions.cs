using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Battle;
using CodeBase.Gameplay.Heroes;
using CodeBase.Gameplay.Skills;
using CodeBase.StaticData.Skills;

namespace CodeBase.Gameplay.AI.Utility
{
    //Appraisal
    public class Convolutions : List<IUtilityFunction>
    {
        public void Add(
          Func<BattleSkill, IHero, bool> appliesTo,
          Func<BattleSkill, IHero, ISkillSolver, float> getInput,
          Func<float, IHero, float> score,
          SkillType skillType)
        {
            Add(new UtilityFunction(appliesTo, getInput, score, skillType));
        }
    }

    public class MobConvolutions : List<IMobUtilityFunction>
    {
        public void Add(
          Func<MobAction, IMob, bool> when,
          Func<MobAction, IMob, float> act,
          Func<float, IMob, float> score,
          MobSkillType skillType)
        {
            Add(new MobUtilityFunction(when, act, score, skillType));
        }
    }
}