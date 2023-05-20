using System;

namespace CodeBase.Demo_V2
{
    //Utility Config Function (Item)
    public class MobUtilityFunction : IMobUtilityFunction
    {
        private readonly Func<MobAction, IMob, bool> _when = default;
        private readonly Func<MobAction, IMob, float> _input = default;
        private readonly Func<float, IMob, float> _score = default;
        public MobSkillKind SkillKind { get; }

        public MobUtilityFunction(
          Func<MobAction, IMob, bool> when,
          Func<MobAction, IMob, float> input,
          Func<float, IMob, float> score,
          MobSkillKind kind
            )
        {
            SkillKind = kind;
            _when = when;
            _input = input;
            _score = score;
        }

        public bool When(MobAction action, IMob mob) => _when(action, mob);

        public float Input(MobAction action, IMob mob) => _input(action, mob);

        public float Score(float input, IMob mob) => _score(input, mob);
    }
}