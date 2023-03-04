using CodeBase.Gameplay.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Gameplay.Battle
{
    public sealed class GameTicker : MonoBehaviour
    {
        [SerializeField] private WizardMob _wizard = default;
        [SerializeField] private RipperMob _ripper = default;

        private MobsUtilityBasedAgent _agent = default;

        private void Start()
        {
            _agent = new MobsUtilityBasedAgent();
            _agent.Initialise(new UtilityMobsBrain());

            _wizard.Initialise();
            _ripper.Initialise();

            Debug.Log("Game Started!");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MobAction wizardBestAction = _agent.ProceedBestDecision(_wizard, _ripper);
                MobAction ripperBestAction = _agent.ProceedBestDecision(_ripper, _wizard);

                _wizard?.ReceiveAction(ripperBestAction);
                _ripper?.ReceiveAction(wizardBestAction);

                if (_wizard.Hp <= 0)
                {
                    GameObject.Destroy(_wizard.gameObject);
                }

                if (_ripper.Hp <= 0)
                {
                    GameObject.Destroy(_ripper.gameObject);
                }

                Debug.Log("New turn!");
            }
        }
    }

    //Action (mob skill & result action from agent)
    public sealed class MobAction
    {
        public MobSkillType ActionType { get; }
        public MobSkillKind ActionKind { get; }
        public float Value { get; }

        public MobAction(MobSkillType actionType, MobSkillKind actionKind, float value)
        {
            ActionType = actionType;
            Value = value;
            ActionKind = actionKind;
        }
    }

    //Mob contract & Precept for agent
    public interface IMob
    {
        float MaxHp { get; }
        float Hp { get; set; }

        IEnumerable<MobAction> Actions { get; }
        MobType Type { get; }

        void Initialise();
        void ReceiveAction(MobAction action);
    }

    //Like a general Atack, Critical , Heal etc.
    public enum MobSkillKind
    {
        None = 0,
        BaseAtack = 1,
        Critical = 2,
        HeadShoot = 3
    }

    //More concrete if atack single target or multi or even burn
    public enum MobSkillType
    {
        None = 0,
        Damage = 1,
        CriticalDamage = 2
    }

    public enum MobType
    {
        None = 0,
        Wizard = 1,
        Ripper = 2
    }

    //Utility-Based Agent
    public sealed class MobsUtilityBasedAgent
    {
        private ICollection<IMobUtilityFunction> _utilityFunctions = default;

        public void Initialise(BaseMobBrain brain)
        {
            _utilityFunctions = brain.LoadBrain();
        }

        public MobAction ProceedBestDecision(IMob caster, IMob target)
        {
            Dictionary<MobAction, float> actionAndScore = new Dictionary<MobAction, float>();

            //Iterate through all actions mob have
            foreach (MobAction action in caster.Actions)
            {
                //Collect sum of actions for example single damage can be happy for 50 points
                //but area damage can be happy for 150 points
                float actionsScoreSum =
                (
                    from utilityFunction in _utilityFunctions
                        //Check with When Sensor here just validate action type
                    where utilityFunction.When(action, target)
                    //Check with Input Sensor can be sum of hp opponents or just your dmg
                    let input = utilityFunction.Input(action, target)
                    //Check with Score Sensor (just score calculation)
                    select utilityFunction.Score(input, target)
                )
                .Sum();

                //Handle it Action-Score
                actionAndScore.Add(action, actionsScoreSum);

                Debug.Log($"{caster.Type}->{target.Type} : {action.ActionType} : {action.ActionKind} : {actionsScoreSum}");
            }

            //Finding best scored action with Aggregate
            MobAction bestAction = actionAndScore.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

            Debug.Log($"Best for {caster.Type}->{target.Type} : {bestAction.ActionType} : {bestAction.ActionKind} | {bestAction.Value}");

            return bestAction;
        }
    }
}

//Utility Config Function Contract
public interface IMobUtilityFunction
{
    bool When(MobAction action, IMob mob);
    float Input(MobAction action, IMob mob);
    float Score(float input, IMob mob);
    MobSkillKind SkillKind { get; }
}

//Utility Config Function (Item)
public class MobUtilityFunction : IMobUtilityFunction
{
    private readonly Func<MobAction, IMob, bool> _when = default;
    private readonly Func<MobAction, IMob, float> _input = default;
    private readonly Func<float, IMob, float> _score = default;
    public MobSkillKind SkillKind { get; }

    public MobUtilityFunction(
      Func<MobAction, IMob, bool> when,
      Func<MobAction, IMob, float> input,
      Func<float, IMob, float> score,
      MobSkillKind kind
        )
    {
        SkillKind = kind;
        _when = when;
        _input = input;
        _score = score;
    }

    public bool When(MobAction action, IMob mob) => _when(action, mob);

    public float Input(MobAction action, IMob mob) => _input(action, mob);

    public float Score(float input, IMob mob) => _score(input, mob);
}

//Utility Config
public abstract class BaseMobBrain
{
    public abstract MobConvolutions LoadBrain();
}

//Utility Config Item Collection
public class MobConvolutions : List<IMobUtilityFunction>
{
    public void Add(
      Func<MobAction, IMob, bool> when,
      Func<MobAction, IMob, float> input,
      Func<float, IMob, float> score,
      MobSkillKind skillKind)
    {
        Add(new MobUtilityFunction(when, input, score, skillKind));
    }
}

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

//Score Sensor
public sealed class MobScore
{
    public static Func<float, IMob, float> IfTrueThen(float value)
    {
        return (input, _) => input + value;
    }

    public static Func<float, IMob, float> IfCriticalThen(float value)
    {
        return (input, _) =>
        {
            int chance = UnityEngine.Random.Range(0, 2);

            if (chance == 0)
            {
                return input + value;
            }
            return 0;
        };
    }

    public static Func<float, IMob, float> IfOneShootThen(float value)
    {
        return (input, _) =>
        {
            int chance = UnityEngine.Random.Range(0, 3);

            if (chance == 0)
            {
                return input + value;
            }

            return 0;
        };
    }
}
