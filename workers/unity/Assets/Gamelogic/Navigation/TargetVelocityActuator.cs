using System;
using Improbable.Math;
using Improbable.Navigation;
using Improbable.Unity;
using Improbable.Unity.Common.Core.Math;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Navigation
{
    [EngineType(EnginePlatform.FSim)]
    class TargetVelocityActuator : MonoBehaviour
    {
        [Require] private TargetVelocityStateReader TargetVelocity;

        private Rigidbody OurRigidbody;
        private Vector3 LastestVelocityValue;

        void OnEnable()
        {
            TargetVelocity.VelocityUpdated += OnVelocityUpdated;
        }

        void FixedUpdate()
        {
            if (TargetVelocity.IsAuthoritativeHere)
            {
                var ourRigidbody = GetComponent<Rigidbody>();
                var lastVelocity = ourRigidbody.velocity;
                var targetVelocity = LastestVelocityValue - lastVelocity;
                if (targetVelocity.magnitude >= TargetVelocity.MaxAccel)
                {
                    targetVelocity = targetVelocity.normalized*TargetVelocity.MaxAccel;
                }
                ourRigidbody.AddForce(targetVelocity, ForceMode.VelocityChange);
            }
        }

        private void OnVelocityUpdated(Vector3d velocity)
        {
            LastestVelocityValue = velocity.ToUnityVector();
        }
    }
}
