using UnityEngine;

namespace CodeBase.Demo_V1
{
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
}
