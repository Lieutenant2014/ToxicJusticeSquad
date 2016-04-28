using System.Linq;
using Improbable.Math;
using Improbable.Navigation;
using Improbable.Unity;
using Improbable.Unity.Common.Core.Math;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Navigation
{
    [EngineType(EnginePlatform.FSim)]
    class SteeringAggregatorBehaviour : MonoBehaviour, ISteeringSourceAggregator
    {
        [Require] public TargetVelocityStateWriter TargetVelocity;
        [Require] public SteeringSourceDataStateWriter SteeringSourcesData;

        public float SumWeights;
        public Vector3 SumWeightedVelocities = Vector3.zero;

        public void UpdateSteeringSource(SteeringSourceData.SteeringSource steeringSource, Vector3 velocity, float weight = 1)
        {
            if (SteeringSourcesData != null && TargetVelocity != null)
            {
                var steeringSources = SteeringSourcesData.SteeringSources.ToDictionary();
                var newValues = new VelocityWeightPair(velocity.ToNativeVector(), weight);
                steeringSources[steeringSource] = newValues;
                SteeringSourcesData.Update.SteeringSources(steeringSources).FinishAndSend();
                UpdateVelocity();
            }
        }

        private void UpdateVelocity()
        {
            SumWeights = SteeringSourcesData.SteeringSources.Values.Select(pair => pair.Weight).Sum();
            SumWeightedVelocities =
                SteeringSourcesData.SteeringSources.Values.Select(pair => pair.Velocity*pair.Weight)
                    .Aggregate((acc, cur) => acc + cur).ToUnityVector();

            if (SumWeights > 0.0f)
            {
                TargetVelocity.Update.Velocity(SumWeightedVelocities.ToNativeVector() / SumWeights).FinishAndSend();
            }
            else
            {
                TargetVelocity.Update.Velocity(Vector3d.ZERO).FinishAndSend();
            }
        }
    }
}
