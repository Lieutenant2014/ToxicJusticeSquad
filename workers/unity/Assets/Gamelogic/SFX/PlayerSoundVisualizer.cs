using Improbable.Player.Controls;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.SFX
{
    class PlayerSoundVisualizer : MonoBehaviour
    {
        [Require] private PlayerControlsStateReader PlayerControls;

        public AudioSource OurAudioSource;

        public AudioClip AttractSFX;
        public AudioClip RepelSFX;
        public AudioClip StartHoseSFX;
        public AudioSource HoseLoopSFX;

        private bool hoseStarted = false;
        private bool hoseLoop = false;

        void OnEnable()
        {
            PlayerControls.AttractMinions += OnAttractMinions;
            PlayerControls.RepelMinions += OnRepelMinions;
        }

        private void OnAttractMinions(AttractMinions alskfdj)
        {
            OurAudioSource.PlayOneShot(AttractSFX);
        }

        private void OnRepelMinions(RepelMinions alskfdj)
        {
            OurAudioSource.PlayOneShot(RepelSFX);
        }

        void Update()
        {
            if (PlayerControls.FireDirection.SquareMagnitude() > 0.001)
            {
                if (!hoseStarted)
                {
                    Debug.Log("Start hose");
//                    OurAudioSource.PlayOneShot(StartHoseSFX);
                    hoseStarted = true;
                }
                else if(!hoseLoop)
                {
                    Debug.Log("Loop Hose");
                    hoseLoop = true;
                    HoseLoopSFX.loop = true;
                    HoseLoopSFX.Play();
                }
            }
            else
            {
                HoseLoopSFX.Stop();
                hoseLoop = false;
                hoseStarted = false;
            }
        }
    }
}
