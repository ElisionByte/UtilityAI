namespace CodeBase.Demo_V2
{
    //Input Sensor
    public static class MobInput
    {
        public static float BasicAtack(MobAction action, IMob opponent)
        {
            return action.Value;
        }

        public static float CriticalAtack(MobAction action, IMob opponent)
        {
            return action.Value;
        }
    }
}