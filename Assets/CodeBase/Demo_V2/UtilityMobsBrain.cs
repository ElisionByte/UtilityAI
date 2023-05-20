namespace CodeBase.Demo_V2
{
    public sealed class UtilityMobsBrain : BaseMobBrain
    {
        public override MobConvolutions LoadBrain()
        {
            return new()
        {
            { MobWhen.SkillIsDamage, MobInput.BasicAtack, MobScore.IfTrueThen(+50), MobSkillKind.BaseAtack},
            { MobWhen.SkillIsCriticalDamage, MobInput.CriticalAtack, MobScore.IfCriticalThen(+150), MobSkillKind.Critical},
            { MobWhen.SkillIsHeadShoot, MobInput.CriticalAtack, MobScore.IfOneShootThen(+1000), MobSkillKind.HeadShoot}
        };
        }
    }
}