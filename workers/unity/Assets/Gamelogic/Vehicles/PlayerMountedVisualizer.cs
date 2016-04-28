using UnityEngine;
using Improbable.CoreLibrary.Transforms.Hierarchy;
using Parent = Improbable.Corelibrary.Transforms.Parent;

namespace Assets.Gamelogic.Vehicles
{
    class PlayerMountedVisualizer : MonoBehaviour
    {
        private TransformChildHierarchyBehaviour cachedTransformChildHierarchyVisualizer;

        public Collider PlayerCollider;

        protected void Awake()
        {
            cachedTransformChildHierarchyVisualizer = GetComponent<TransformChildHierarchyBehaviour>();
            if (cachedTransformChildHierarchyVisualizer == null)
            {
                Debug.LogError("MyVisualizer expects a TransformChildHierarchyVisualizer.");
            }
        }

        protected void OnEnable()
        {
            cachedTransformChildHierarchyVisualizer.OnTransformParented += OnTransformParented;
            cachedTransformChildHierarchyVisualizer.OnTransformUnparented += OnTransformUnParented;
        }

        protected void OnDisable()
        {
            cachedTransformChildHierarchyVisualizer.OnTransformParented -= OnTransformParented;
            cachedTransformChildHierarchyVisualizer.OnTransformUnparented -= OnTransformUnParented;
        }

        private void OnTransformParented(Parent parent)
        {
            PlayerCollider.isTrigger = true;
        }

        private void OnTransformUnParented()
        {
            PlayerCollider.isTrigger = false;
        }
    }
}
