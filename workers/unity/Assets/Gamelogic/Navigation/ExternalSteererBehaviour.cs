using System.Collections.Generic;
using System.Linq;
using Assets.Gamelogic.Navigation.Sources;
using Improbable.Player.Controls;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Navigation
{
    [EngineType(EnginePlatform.FSim)]
    class ExternalSteererBehaviour : MonoBehaviour
    {
        [Require] public PlayerControlsStateReader PlayerControls;

        public float Speed = 10.0f;
        public float Range = 40.0f;

        void OnEnable()
        {
            PlayerControls.AttractMinions += AttractSteerables;
            PlayerControls.RepelMinions += ReppelSteerables;
        }

        private void AttractSteerables(AttractMinions notused)
        {
            var steerables = GetSteerables(transform, Range);
            foreach (var steerable in steerables)
            {
                steerable.GetComponent<ExternallySteerableBehaviour>()
                    .AddSteeringInfluence((transform.position - steerable.transform.position).normalized * Speed);
            }
        }

        private void ReppelSteerables(RepelMinions asdflkj)
        {
            var steerables = GetSteerables(transform, Range);
            foreach (var steerable in steerables)
            {
                steerable.GetComponent<ExternallySteerableBehaviour>()
                    .AddSteeringInfluence((steerable.transform.position - transform.position).normalized * Speed);
            }
        }

        public static IEnumerable<Collider> GetSteerables(Transform thisTransform, float range)
        {
            var colliders = Physics.OverlapSphere(thisTransform.position, range);
            var steerables =
                colliders.Where(someCollider => someCollider.gameObject.GetComponent<ExternallySteerableBehaviour>() != null);
            return steerables;
        }
    }
}
