using Improbable.Player;
using Improbable.Player.Controls;
using Improbable.Unity.Input.Sources;
using Improbable.Unity.Visualizer;
using IoC;
using UnityEngine;
using Vector3 = Improbable.Math.Vector3d;

namespace Assets.Gamelogic.Visualizers
{
    public class PlayerControlsVisualizer : MonoBehaviour
    {
        [Inject] public IInputSource InputSource { protected get; set; }

        [Require] protected LocalPlayerCheckStateWriter LocalPlayerCheck;
        [Require] protected PlayerControlsStateWriter PlayerControls;

        public void Update()
        {
            PlayerControls.Update.MovementDirection(GetMovementDirection()).FinishAndSend();            
            PlayerControls.Update.FireDirection(GetFireDirection()).FinishAndSend();

            if (Input.GetButtonDown("Interact"))
            {
                PlayerControls.Update.TriggerPlayerInteract().FinishAndSend();
            }
        }

        private Vector3 GetFireDirection()
        {
            return new Vector3(Input.GetAxis("BuildHoriz"), 0 , Input.GetAxis("BuildVert"));
        }

        private Vector3 GetMovementDirection()
        {
            return new Vector3(InputSource.GetAxis("Horizontal"), 0, InputSource.GetAxis("Vertical"));
        }
    }
}