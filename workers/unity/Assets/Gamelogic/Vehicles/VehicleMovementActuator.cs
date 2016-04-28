using Improbable.Math;
using Improbable.Player.Controls;
using Improbable.Unity;
using Improbable.Unity.Common.Core.Math;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Vehicles
{
    [EngineType(EnginePlatform.FSim)]
    internal class VehicleMovementActuator : MonoBehaviour
    {
        private Vector3 LastestVelocityValue;

        [Require] public VehicleControlsStateReader VehicleControls;

        private Rigidbody OurRigidbody;

        void OnEnable()
        {
            VehicleControls.MovementDirectionUpdated += OnMovementDirectionUpdated;
        }

        void FixedUpdate()
        {
            OurRigidbody = GetComponent<Rigidbody>();
            OurRigidbody.AddForce(LastestVelocityValue, ForceMode.Acceleration);
            
            if (OurRigidbody.velocity.sqrMagnitude > 0.3f)
            {
                var flattenedVelocity = OurRigidbody.velocity;
                flattenedVelocity.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(flattenedVelocity), 0.2f);
            }
        }

        private void OnMovementDirectionUpdated(Vector3d velocity)
        {
            LastestVelocityValue = velocity.ToUnityVector();
        }
    }
}