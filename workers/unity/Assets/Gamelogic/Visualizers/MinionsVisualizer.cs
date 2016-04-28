using UnityEngine;
using UnityEngine.UI;

namespace Assets.Gamelogic.Visualizers
{
    public class MinionsVisualizer : MonoBehaviour
    {
        public Text NumberMinions;

        public void SetNumMinions(long minions)
        {
            NumberMinions.text = "Citizens Left: " + minions;
//            TimeText.rectTransform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 0.3f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutCubic);
        }
    }
}
