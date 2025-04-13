using UnityEngine;
using DG.Tweening;

public class Spinning2dSprite : MonoBehaviour
{

void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), 1f, RotateMode.FastBeyond360)
                 .SetLoops(-1, LoopType.Restart)
                 .SetEase(Ease.InOutSine);
    }
}
