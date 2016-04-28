using Improbable.Navigation;
using UnityEngine;

namespace Assets.Gamelogic.Navigation
{
    interface ISteeringSourceAggregator
    {
        void UpdateSteeringSource(SteeringSourceData.SteeringSource steeringSource, Vector3 velocity, float weight = 1.0f);
    }
}
