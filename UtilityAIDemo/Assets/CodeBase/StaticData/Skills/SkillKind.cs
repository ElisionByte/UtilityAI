namespace CodeBase.StaticData.Skills
{
    public enum SkillKind
    {
        Unknown = 0,
        Damage = 1,
        Heal = 2,
        // MagicalDamage = 3,
        InitiativeBurn = 4,
    }

    public enum MobSkillKind
    {
        None = 0,
        BaseAtack = 1,
        Critical,
        HeadShoot
    }

    public enum MobSkillType
    {
        None = 0,
        Damage = 1
    }
}