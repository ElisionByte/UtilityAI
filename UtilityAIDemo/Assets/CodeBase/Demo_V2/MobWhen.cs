namespace CodeBase.Demo_V2
{
    //When Sensor
    public static class MobWhen
    {
        public static bool SkillIsDamage(MobAction action, IMob opponent) =>
          action.ActionType == MobSkillType.Damage;
        public static bool SkillIsCriticalDamage(MobAction action, IMob opponent) =>
          action.ActionType == MobSkillType.CriticalDamage &&
          action.ActionKind == MobSkillKind.Critical;
        public static bool SkillIsHeadShoot(MobAction action, IMob opponent) =>
          action.ActionType == MobSkillType.CriticalDamage &&
          action.ActionKind == MobSkillKind.HeadShoot;
    }
}