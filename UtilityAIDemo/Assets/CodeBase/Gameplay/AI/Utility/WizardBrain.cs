using System;
using System.Collections.Generic;

namespace CodeBase.Gameplay.AI.Utility
{
    //1
    //public interface IMobUtilityFunction
    //{
    //    bool When(MobAction action, IMob mob);
    //    float Input(MobAction action, IMob mob);
    //    float Score(float input, IMob mob);
    //    MobSkillKind SkillKind { get; }
    //}
    //public class MobUtilityFunction : IMobUtilityFunction
    //{
    //    private readonly Func<MobAction, IMob, bool> _when = default;
    //    private readonly Func<MobAction, IMob, float> _input = default;
    //    private readonly Func<float, IMob, float> _score = default;
    //    public MobSkillKind SkillKind { get; }

    //    public MobUtilityFunction(
    //      Func<MobAction, IMob, bool> when,
    //      Func<MobAction, IMob, float> input,
    //      Func<float, IMob, float> score,
    //      MobSkillKind kind)
    //    {
    //        SkillKind = kind;
    //        _when = when;
    //        _input = input;
    //        _score = score;
    //    }

    //    public bool When(MobAction action, IMob mob) => _when(action, mob);

    //    public float Input(MobAction action, IMob mob) => _input(action, mob);

    //    public float Score(float input, IMob mob) => _score(input, mob);
    //}

    //2
    //public abstract class BaseMobBrain
    //{
    //    public abstract MobConvolutions LoadBrain();
    //}
    //public class MobConvolutions : List<IMobUtilityFunction>
    //{
    //    public void Add(
    //      Func<MobAction, IMob, bool> when,
    //      Func<MobAction, IMob, float> act,
    //      Func<float, IMob, float> score,
    //      MobSkillKind skillKind)
    //    {
    //        Add(new MobUtilityFunction(when, act, score, skillKind));
    //    }
    //}
    //public sealed class WizardBrain : BaseMobBrain
    //{
    //    public override MobConvolutions LoadBrain()
    //    {
    //        return new()
    //        {
    //            { MobWhen.SkillIsDamage, MobInput.BasicAtack, MobScore.IfTrueThen(+50), MobSkillKind.BaseAtack},
    //            //{ MobWhen.SkillIsCriticalDamage, MobInput.CriticalAtack, MobScore.IfCriticalThen(+150), MobSkillKind.Critical},
    //        };
    //    }
    //}
    //public sealed class RipperBrain : BaseMobBrain
    //{
    //    public override MobConvolutions LoadBrain()
    //    {
    //        return new()
    //        {
    //            { MobWhen.SkillIsDamage, MobInput.BasicAtack, MobScore.IfTrueThen(+50), MobSkillKind.BaseAtack},
    //            //{ MobWhen.SkillIsCriticalDamage, MobInput.CriticalAtack, MobScore.IfOneShootThen(+150), MobSkillKind.HeadShoot}
    //        };
    //    }
    //}

    //3
    //public static class MobWhen
    //{
    //    public static bool SkillIsDamage(MobAction action, IMob opponent) =>
    //      action.ActionType == MobSkillType.Damage;
    //    //public static bool SkillIsCriticalDamage(MobAction action, IMob opponent) =>
    //    //  action.ActionType == MobSkillType.CriticalDamage;
    //}
    //public static class MobInput
    //{
    //    public static float BasicAtack(MobAction action, IMob opponent)
    //    {
    //        return action.Value;
    //    }

    //    //public static float CriticalAtack(MobAction action, IMob opponent)
    //    //{
    //    //    return action.Value;
    //    //}
    //}
    //public sealed class MobScore
    //{
    //    public static Func<float, IMob, float> IfTrueThen(float value)
    //    {
    //        return (input, _) => input + value;
    //    }

    //    //public static Func<float, IMob, float> IfCriticalThen(float value)
    //    //{
    //    //    return (input, _) =>
    //    //    {
    //    //        int chance = UnityEngine.Random.Range(0, 2);

    //    //        if (chance == 0)
    //    //        {
    //    //            return input + value;
    //    //        }
    //    //        return 0;
    //    //    };
    //    //}

    //    //public static Func<float, IMob, float> IfOneShootThen(float value)
    //    //{
    //    //    return (input, _) =>
    //    //    {
    //    //        int chance = UnityEngine.Random.Range(0, 3);

    //    //        if (chance == 0)
    //    //        {
    //    //            return input + value;
    //    //        }
    //    //        return 0;
    //    //    };
    //    //}
    //}
}