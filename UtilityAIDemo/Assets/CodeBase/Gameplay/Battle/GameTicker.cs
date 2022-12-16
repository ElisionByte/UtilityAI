using UnityEngine;

namespace CodeBase.Gameplay.Battle
{
    public sealed class GameTicker : MonoBehaviour
    {
        [SerializeField] private WizardMob _wizard = default;
        [SerializeField] private RipperMob _ripper = default;

        private void Start()
        {
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
            }
        }
    }
}