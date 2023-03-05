using UnityEngine;

namespace CodeBase.Demo_V2
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
}