using CodeBase.Gameplay.AI.Calculation;
using CodeBase.Gameplay.Battle;
using CodeBase.StaticData.Skills;

namespace CodeBase.Gameplay.AI.Utility
{
    //Consideration
    public sealed class MobsBrain : BaseBrain
    {
        public override Convolutions LoadBrain()
        {
            return new()
            {
                {When.SkillIsDamage, GetInput.PercentageDamage, Score.ScaleBy(100), SkillType.BasicDamage},
                {When.SkillIsDamage, GetInput.IsKillingBlow, Score.IfTrueThen(+150), SkillType.KillingBlow},
                {When.SkillIsBasicAttack, GetInput.IsKillingBlow, Score.IfTrueThen(+30), SkillType.BasicSkillKillingBlow},

                {When.SkillIsHeal, GetInput.HealPercentage, Score.CullByTargetHp, SkillType.Heal},

                {When.SkillIsInitiativeBurn, GetInput.InitiativeBurn, Score.CullByTargetInitiative(scaleBy: 50f, cullThreshold: 0.25f), SkillType.InitiativeBurn},
                {When.SkillIsInitiativeBurn, GetInput.TargetUltimateIsReady, Score.IfTrueThen(+30), SkillType.InitiativeBurnUltimateIsReady},
            };
        }
    }
}