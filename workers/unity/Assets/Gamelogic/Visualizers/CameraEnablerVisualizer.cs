using Improbable.Player;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Visualizers
{
    public class CameraEnablerVisualizer : MonoBehaviour
    {
        [Require] protected LocalPlayerCheckStateWriter LocalPlayerCheck;

        public GameObject OurCamera;

        public void OnEnable()
        {
            Debug.Log("Enable the camera");
            OurCamera.SetActive(true);
        }

        public void OnDisable()
        {
            Debug.Log("Disable the camera");
            OurCamera.SetActive(false);
        }
    }
}