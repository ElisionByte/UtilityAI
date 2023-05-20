using System;
using System.Collections.Generic;

namespace CodeBase.Demo_V2
{
    //Utility Config Item Collection
    public class MobConvolutions : List<IMobUtilityFunction>
    {
        public void Add(
          Func<MobAction, IMob, bool> when,
          Func<MobAction, IMob, float> input,
          Func<float, IMob, float> score,
          MobSkillKind skillKind)
        {
            Add(new MobUtilityFunction(when, input, score, skillKind));
        }
    }
}