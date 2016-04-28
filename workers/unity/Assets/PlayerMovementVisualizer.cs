using UnityEngine;
using Improbable.Player.Controls;
using Improbable.Unity.Common.Core.Math;
using Improbable.Unity.Visualizer;

public class PlayerMovementVisualizer : MonoBehaviour
{
    [Require] private PlayerControlsStateReader playerControls;

    public Animator Animator;
    public Transform ChararacterTransform;

    public float MaxRunSpeed;

    public Transform CameraRoot;

    // Use this for initialization
    private void FixedUpdate()
    {
        if (playerControls.IsAuthoritativeHere && GetComponent<Rigidbody>() != null)
        {
            var ourRigidbody = GetComponent<Rigidbody>();

            var targetVelocity = CameraRoot.transform.rotation*playerControls.MovementDirection.ToUnityVector()*
                                 MaxRunSpeed;
            var velocityDelta = targetVelocity - ourRigidbody.velocity;

            ourRigidbody.AddForce(velocityDelta, ForceMode.VelocityChange);
        }

        float angle = Mathf.Atan2(playerControls.MovementDirection.ToUnityVector().x,
                     playerControls.MovementDirection.ToUnityVector().z) * Mathf.Rad2Deg;
        ChararacterTransform.rotation = Quaternion.AngleAxis(angle, Vector3.up)*CameraRoot.transform.rotation;
        var squareMagnitude = (float) playerControls.MovementDirection.SquareMagnitude();
        Animator.SetFloat("Speed_f", squareMagnitude*squareMagnitude);
    }
}