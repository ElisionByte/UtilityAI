using System.Collections.Generic;

namespace CodeBase.Demo_V1
{
    public sealed class NumberBrain
    {
        private List<INumberUtilityFunction> _convulsions = default;
        public ICollection<INumberUtilityFunction> UtilityFunctions => _convulsions;

        public NumberBrain()
        {
            _convulsions = new()
            {
                new NumberUtilityFunction(NumberWhenSensor.NumberIsOdd, NumberScoreHappiness.IfTrueThen(+300)),
                new NumberUtilityFunction(NumberWhenSensor.NumberIsBinate, NumberScoreHappiness.IfTrueThen(+600)),
                new NumberUtilityFunction(NumberWhenSensor.NumberIsMin, NumberScoreHappiness.IfTrueThen(+1000)),
                new NumberUtilityFunction(NumberWhenSensor.NumberIsMax, NumberScoreHappiness.IfTrueThen(+3000))
            };
        }
    }
}