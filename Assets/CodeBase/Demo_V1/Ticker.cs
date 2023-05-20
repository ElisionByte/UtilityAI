using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

namespace CodeBase.Demo_V1
{
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

    }
}

