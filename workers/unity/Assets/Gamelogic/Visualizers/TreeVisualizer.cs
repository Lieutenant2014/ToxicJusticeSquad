using Improbable.TreeState;
using Improbable.Math;
using Improbable.Unity.Visualizer;
using UnityEngine;
using System.Collections;

namespace Assets.Gamelogic.Visualizers
{
    public class TreeVisualizer : MonoBehaviour
    {
        [Require]
        public TreeStateReader TreeState;

        public void OnEnable()
        {
            TreeState.SizeUpdated += HandleSizeUpdated;
        }

        public void OnDisable()
        {
            TreeState.SizeUpdated -= HandleSizeUpdated;
        }

        private void HandleSizeUpdated(float size)
        {
            transform.localScale = new Vector3(size, size, size);
        }
    }
}