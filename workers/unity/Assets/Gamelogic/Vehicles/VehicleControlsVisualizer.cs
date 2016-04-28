using Improbable.Player.Controls;
using Improbable.Unity;
using Improbable.Unity.Common.Core.Math;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Visualizers
{
    [EngineType(EnginePlatform.Client)]
    public class VehicleControlsVisualizer : MonoBehaviour
    {
        [Require] public VehicleControlsStateWriter VehicleControls;

        public Transform DirectionIndicator;

        public float Force = 70.0f;

        public void Update()
        {
            var movementDirection = (GetMovementDirection()*Force);

            if (movementDirection.magnitude > 0.1f)
            {
                DirectionIndicator.rotation = Quaternion.RotateTowards(DirectionIndicator.rotation, Quaternion.LookRotation(movementDirection), 3);
            }
          
            VehicleControls.Update.MovementDirection(movementDirection.ToNativeVector()).FinishAndSend();
        }

        private Vector3 GetMovementDirection()
        {
            if (Camera.current != null)
            {
                return Camera.current.transform.parent.rotation * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            }
            return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
    }
}