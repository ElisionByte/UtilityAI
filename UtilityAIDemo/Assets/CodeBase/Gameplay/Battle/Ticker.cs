using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

public sealed class Ticker : MonoBehaviour
{
    private ICollection<INumberUtilityFunction> _utilityFunctions = default;

    private void Awake()
    {
        _utilityFunctions = new NumberBrain().UtilityFunctions;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int generated = Random.Range(1, 11);

            Debug.Log($"___New Number : {generated} ___");

            ValidateBestChoise(generated);
        }
    }

    private void ValidateBestChoise(int number)
    {
        float scoreHapiness =
        (
            from utilityFunction in _utilityFunctions
            where utilityFunction.WhenSensor(number)
            select utilityFunction.ScoreHappiness
        )
        .Sum();

        //Action example
        Debug.Log($"For number : {number} we got result action with {scoreHapiness} scoreHapiness!");
    }

    public interface INumberUtilityFunction
    {
        bool WhenSensor(int number);
        float ScoreHappiness { get; }
    }

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

    public static class NumberWhenSensor
    {
        public static bool NumberIsOdd(int number)
        {
            bool result = number % 2 != 0;

            if (result)
            {
                Debug.Log("Number Is Odd!");
            }

            return result;
        }

        public static bool NumberIsBinate(int number)
        {
            bool result = number % 2 == 0;

            if (result)
            {
                Debug.Log("Number Is Binate!");
            }

            return result;
        }

        public static bool NumberIsMax(int number)
        {
            bool result = number == 10;

            if (result)
            {
                Debug.Log("Number Is Max!");
            }

            return result;
        }

        public static bool NumberIsMin(int number)
        {
            bool result = number == 1;

            if (result)
            {
                Debug.Log("Number Is Min!");
            }

            return result;
        }
    }

    public static class NumberScoreHappiness
    {
        public static float IfTrueThen(int score) => score;
    }
}