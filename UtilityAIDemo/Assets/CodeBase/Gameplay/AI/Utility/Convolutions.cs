using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Heroes;
using CodeBase.Gameplay.Skills;

namespace CodeBase.Gameplay.AI.Utility
{
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
}