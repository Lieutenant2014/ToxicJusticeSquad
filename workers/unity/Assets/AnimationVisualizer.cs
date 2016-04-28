using UnityEngine;
using System.Collections;
using Improbable.Entity.Physical;
using Improbable.Navigation;
using Improbable.Unity.Visualizer;

public class AnimationVisualizer : MonoBehaviour
{

    [Require] private RigidbodyEngineDataReader RigidbodyEngineDataReader;

    public Transform ChararacterTransform;
    public Animator Animator;

    void FixedUpdate () {
        
        float angle = Mathf.Atan2((float) RigidbodyEngineDataReader.Velocity.x, (float) RigidbodyEngineDataReader.Velocity.z) * Mathf.Rad2Deg;
        ChararacterTransform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        var sqMag = (float) RigidbodyEngineDataReader.Velocity.SquareMagnitude();
        Animator.SetFloat("Speed_f", sqMag * sqMag);
    }
}
