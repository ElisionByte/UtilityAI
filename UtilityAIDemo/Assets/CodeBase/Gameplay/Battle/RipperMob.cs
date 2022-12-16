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

        private ICollection<IMobUtilityFunction> _utilityFunctions = default;
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
            _utilityFunctions = new RipperBrain().LoadBrain();
            _hp = 1;
            _hpSlider.value = _hp;

            _actions = new MobAction[1]
            {
                new MobAction(MobSkillType.Damage, 0.3f)
            };
        }

        public void InvokeAction(MobAction action)
        {
            switch (action.ActionType)
            {
                case MobSkillType.Damage:
                    {
                        _hp -= action.Value;
                        _hpSlider.value = _hp;
                    }
                    break;
            }
        }

        public MobAction ProceedBestDecision(IMob opponent)
        {
            Dictionary<MobAction, float> actionAndScore = new Dictionary<MobAction, float>();

            foreach (MobAction action in _actions)
            {
                float actionsScoreSum =
                 (from utilityFunction in _utilityFunctions
                  where utilityFunction.When(action, opponent)
                  let input = utilityFunction.Input(action, opponent)
                  select utilityFunction.Score(input, opponent)).Sum();

                actionAndScore.Add(action, actionsScoreSum);
            }

            MobAction bestAction = actionAndScore.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

            return bestAction;
        }
    }
}