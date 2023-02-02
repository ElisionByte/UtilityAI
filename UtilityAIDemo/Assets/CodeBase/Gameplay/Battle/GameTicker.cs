using CodeBase.Gameplay.AI.Utility;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Gameplay.Battle
{
    public sealed class GameTicker : MonoBehaviour
    {
        [SerializeField] private WizardMob _wizard = default;
        [SerializeField] private RipperMob _ripper = default;

        //private MobsUtilityBasedAgent _agent = default;

        private void Start()
        {
            //_agent = new MobsUtilityBasedAgent();
            //_agent.Initialise(new UtilityMobsBrain());

            _wizard.Initialise();
            _ripper.Initialise();

            Debug.Log("Game Started!");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //MobAction wizardBestAction = _agent.ProceedBestDecision(_wizard, _ripper);
                //MobAction ripperBestAction = _agent.ProceedBestDecision(_ripper, _wizard);

                //_wizard?.ReceiveAction(ripperBestAction);
                //_ripper?.ReceiveAction(wizardBestAction);

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
}