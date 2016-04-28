using Improbable.Life;
using Improbable.Navigation;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Gamelogic.Navigation.Sources
{
    [EngineType(EnginePlatform.FSim)]
    class WanderBehaviour : MonoBehaviour
    {
        //Authority check
        [Require] public TargetVelocityStateWriter TargetVelocity;


        [Require]
        public LifeStateReader LifeState;

        public float MaxSpeedInMetersPerSecond = 5.0f;
        public float TimeBetweenNewDirections = 5.0f;

        private ISteeringSourceAggregator SteeringAggregator;

        void Awake()
        {
            SteeringAggregator = GetComponent<SteeringAggregatorBehaviour>();
        }

        void OnEnable()
        {
            InvokeRepeating("MoveInRandomDirection", 0, TimeBetweenNewDirections);
        }

        void MoveInRandomDirection()
        {
            if(LifeState.Alive)
            {
                var newDirection = Random.insideUnitCircle * MaxSpeedInMetersPerSecond;
                var newDirectionInPlane = new Vector3(newDirection.x, 0, newDirection.y);
                SteeringAggregator.UpdateSteeringSource(SteeringSourceData.SteeringSource.Wander, newDirectionInPlane);
            }
            else
            {
                SteeringAggregator.UpdateSteeringSource(SteeringSourceData.SteeringSource.Wander, Vector3.zero);
            }

        }

        void OnDisable()
        {
            CancelInvoke("MoveInRandomDirection");
        }
    }
}
