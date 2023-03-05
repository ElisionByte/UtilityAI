using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Demo_V2
{
    public sealed class RipperMob : MonoBehaviour, IMob
    {
        [SerializeField] private Slider _hpSlider = default;
        public float MaxHp { get => 1; }

        private float _hp = default;
        public float Hp
        {
            get => _hp;
            set => _hp = value;
        }

        private MobAction[] _actions = default;
        public IEnumerable<MobAction> Actions => _actions;

        public MobType Type => MobType.Ripper;

        public void Initialise()
        {
            _hp = 1;
            _hpSlider.value = _hp;

            _actions = new MobAction[3]
            {
                new MobAction(MobSkillType.Damage, MobSkillKind.BaseAtack, 0.2f),
                new MobAction(MobSkillType.CriticalDamage, MobSkillKind.Critical, 0.5f),
                new MobAction(MobSkillType.CriticalDamage, MobSkillKind.HeadShoot, 1f)
            };
        }

        public void ReceiveAction(MobAction action)
        {
            _hp -= action.Value;
            _hpSlider.value = _hp;

            Debug.Log($"{Type} : received {action.Value} dmg");
        }
    }
}