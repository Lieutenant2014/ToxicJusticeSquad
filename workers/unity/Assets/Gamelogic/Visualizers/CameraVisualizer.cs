using Improbable.Corelibrary.Transforms;
using Improbable.Player;
using Improbable.Unity.Input.Sources;
using Improbable.Unity.Visualizer;
using IoC;
using UnityEngine;
using Transform = UnityEngine.Transform;

namespace Assets.Gamelogic.Visualizers
{
    public class CameraVisualizer : MonoBehaviour
    {
        [Inject] public IInputSource InputSource { protected get; set; }
        
        [Require] protected LocalPlayerCheckStateWriter LocalPlayerCheck;
        [Require] protected TransformStateReader TransformStateReader;

        public Transform CameraRoot;
        public float RotationSpeed;

        public void Update()
        {
            if (InputSource.GetButton("RotateLeft") && TransformStateReader.Parent == null)
            {
                CameraRoot.transform.Rotate(-Vector3.up * Time.deltaTime * RotationSpeed, Space.World);
            }
            else if (InputSource.GetButton("RotateRight") && TransformStateReader.Parent == null)
            {
                CameraRoot.transform.Rotate(Vector3.up*Time.deltaTime*RotationSpeed, Space.World);
            }
            else if(TransformStateReader.Parent != null)
            {
                CameraRoot.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                var cameraRotation = CameraRoot.transform.FindChild("Camera").transform.localRotation;
                var cameraEuler = cameraRotation.eulerAngles;
                CameraRoot.transform.FindChild("Camera").transform.localRotation = Quaternion.Euler(cameraEuler.x, 0, cameraEuler.z);
            }
        }
    }
}
