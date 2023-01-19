using CodeBase.Gameplay.AI.Utility;
using UnityEngine;

namespace CodeBase.Gameplay.Battle
{
    public sealed class GameTicker : MonoBehaviour
    {
        [SerializeField] private WizardMob _wizard = default;
        [SerializeField] private RipperMob _ripper = default;

        private void Start()
        {
            Debug.LogWarning("Game Started!");

            _wizard.Initialise();
            _ripper.Initialise();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MobAction wizardAction = _wizard?.ProceedBestDecision(_ripper);
                MobAction ripperAction = _ripper?.ProceedBestDecision(_wizard);

                _wizard?.InvokeAction(ripperAction);
                _ripper?.InvokeAction(wizardAction);

                if (_wizard.Hp <= 0)
                {
                    GameObject.Destroy(_wizard.gameObject);
                }

                if (_ripper.Hp <= 0)
                {
                    GameObject.Destroy(_ripper.gameObject);
                }

                Debug.LogError("New turn!");
            }
        }
    }

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

    public interface IMob
    {
        float MaxHp { get; }
        float Hp { get; set; }

        void Initialise();
        MobAction ProceedBestDecision(IMob opponent);
        void InvokeAction(MobAction action);
    }

    public enum MobSkillKind
    {
        None = 0,
        BaseAtack = 1,
        Critical = 2,
        HeadShoot = 3
    }

    public enum MobSkillType
    {
        None = 0,
        Damage = 1,
        CriticalDamage = 2
    }
}