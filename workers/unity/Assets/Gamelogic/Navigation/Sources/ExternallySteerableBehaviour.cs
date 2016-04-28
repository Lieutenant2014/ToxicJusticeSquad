using Improbable.Life;
using Improbable.Navigation;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Navigation.Sources
{
    [EngineType(EnginePlatform.FSim)]
    public class ExternallySteerableBehaviour : MonoBehaviour
    {
        //Authority check
        [Require]
        public TargetVelocityStateWriter TargetVelocity;

        [Require]
        public LifeStateReader LifeState;

        private ISteeringSourceAggregator SteeringAggregator;

        void Awake()
        {
            SteeringAggregator = GetComponent<SteeringAggregatorBehaviour>();
        }

        public void AddSteeringInfluence(Vector3 velocity)
        {
            if (LifeState.Alive)
            {
                SteeringAggregator.UpdateSteeringSource(SteeringSourceData.SteeringSource.Herd, velocity);
            }
            else
            {
                SteeringAggregator.UpdateSteeringSource(SteeringSourceData.SteeringSource.Herd, Vector3.zero);
            }
        }
    }
}
