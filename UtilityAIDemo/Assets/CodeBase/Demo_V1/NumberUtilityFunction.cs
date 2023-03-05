using System;

namespace CodeBase.Demo_V1
{
    public sealed class NumberUtilityFunction : INumberUtilityFunction
    {
        private readonly Func<int, bool> _when = default;
        private readonly float _scoreHappiness = default;

        public NumberUtilityFunction(
          Func<int, bool> when,
          float scoreHappiness
          )
        {
            _when = when;
            _scoreHappiness = scoreHappiness;
        }

        public bool WhenSensor(int number) => _when(number);

        public float ScoreHappiness => _scoreHappiness;
    }
}