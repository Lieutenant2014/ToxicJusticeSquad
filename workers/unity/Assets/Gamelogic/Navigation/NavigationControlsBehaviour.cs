using Improbable.Player.Controls;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Navigation
{
    class NavigationControlsBehaviour : MonoBehaviour
    {
        [Require]
        public PlayerControlsStateWriter PlayerControls;

        public void Update()
        {
            if (Input.GetButtonDown("Attract"))
            {
                Debug.Log("Attracting!");
                PlayerControls.Update.TriggerAttractMinions().FinishAndSend();
            }

            if (Input.GetButtonDown("Repel"))
            {
                Debug.Log("Repel!");
                PlayerControls.Update.TriggerRepelMinions().FinishAndSend();
            }
        }
    }
}
