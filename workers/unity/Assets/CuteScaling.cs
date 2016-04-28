using UnityEngine;
using DG.Tweening;

public class CuteScaling : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
//	    transform.DOScale(new Vector3(1.01f, 1.01f, 1.01f), 0.7f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic);
        transform.DOLocalMoveY(0.05f, 0.7f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }
}
