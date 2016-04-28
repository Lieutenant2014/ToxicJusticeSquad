using UnityEngine;
using DG.Tweening;

public class MainImageJiggle : MonoBehaviour
{
    public RectTransform Jiggle;

	// Use this for initialization
	void Start () {
	   Jiggle.DOShakePosition(8.0f, new Vector3(15, 15, 15), 3).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
       Jiggle.DOShakeRotation(8.0f, new Vector3(0, 4, 0), 3).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);

    }
}
