using UnityEngine;
using UnityEngine.UI;

public class ScoreVisualizer : MonoBehaviour
{
    public Text ScoreText;

    public void ScoreUpdated(int score)
    {        
            ScoreText.text = "Citizens Saved: " + score;
//            ScoreText.rectTransform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 0.7f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutCubic);
    }
}

