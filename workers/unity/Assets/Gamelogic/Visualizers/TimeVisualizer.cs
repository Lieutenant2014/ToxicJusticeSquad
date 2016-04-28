using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Gamelogic.Visualizers
{
    public class TimeVisualizer : MonoBehaviour
    {
        public Text TimeText;

        public void SetTimeInSeconds(long time)
        {
            TimeText.text = "Seconds Left: " + time;
//            TimeText.rectTransform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 0.3f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutCubic);
        }
    }
}
