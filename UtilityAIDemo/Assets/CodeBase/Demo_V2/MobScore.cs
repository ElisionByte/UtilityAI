using System;

namespace CodeBase.Demo_V2
{
    //Score Sensor
    public sealed class MobScore
    {
        public static Func<float, IMob, float> IfTrueThen(float value)
        {
            return (input, _) => input + value;
        }

        public static Func<float, IMob, float> IfCriticalThen(float value)
        {
            return (input, _) =>
            {
                int chance = UnityEngine.Random.Range(0, 2);

                if (chance == 0)
                {
                    return input + value;
                }
                return 0;
            };
        }

        public static Func<float, IMob, float> IfOneShootThen(float value)
        {
            return (input, _) =>
            {
                int chance = UnityEngine.Random.Range(0, 3);

                if (chance == 0)
                {
                    return input + value;
                }

                return 0;
            };
        }
    }
}