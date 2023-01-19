using System.Collections.Generic;
using System.Linq;
using CodeBase.Gameplay.AI.Utility;
using CodeBase.StaticData.Skills;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Gameplay.Battle
{
    public sealed class RipperMob : MonoBehaviour, IMob
    {
        [SerializeField] private Slider _hpSlider = default;

        //private ICollection<IMobUtilityFunction> _utilityFunctions = default;
        private MobAction[] _actions = default;

        public float MaxHp { get => 1; }

        private float _hp = default;
        public float Hp
        {
            get => _hp;
            set => _hp = value;
        }

        public void Initialise()
        {
            _hp = 1;
            _hpSlider.value = _hp;

            //_utilityFunctions = new RipperBrain().LoadBrain();
            //_actions = new MobAction[2]
            //{
            //    new MobAction(MobSkillType.Damage, MobSkillKind.BaseAtack, 0.3f),
            //    //new MobAction(MobSkillType.CriticalDamage, MobSkillKind.HeadShoot, 1f)
            //};
        }
        public MobAction ProceedBestDecision(IMob opponent)
        {
            //Dictionary<MobAction, float> actionAndScore = new Dictionary<MobAction, float>();

            //foreach (MobAction action in _actions)
            //{
            //    float actionsScoreSum =
            //     (from utilityFunction in _utilityFunctions
            //      where utilityFunction.When(action, opponent)
            //      let input = utilityFunction.Input(action, opponent)
            //      select utilityFunction.Score(input, opponent)).Sum();

            //    actionAndScore.Add(action, actionsScoreSum);

            //    Debug.Log($"{nameof(RipperMob)} : {action.ActionType} : {action.ActionKind} : {actionsScoreSum}");
            //}

            //MobAction bestAction = actionAndScore.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

            //Debug.Log($"Best for {nameof(RipperMob)} : {bestAction.ActionType} : {bestAction.ActionKind}");

            //return bestAction;

            return default;
        }

        public void InvokeAction(MobAction action)
        {
            //switch (action.ActionType)
            //{
            //    case MobSkillType.Damage:
            //        {
            //            _hp -= action.Value;
            //            _hpSlider.value = _hp;
            //        }
            //        break;
            //    case MobSkillType.CriticalDamage:
            //        {
            //            _hp -= action.Value;
            //            _hpSlider.value = _hp;
            //        }
            //        break;
            //}
        }

    }
}