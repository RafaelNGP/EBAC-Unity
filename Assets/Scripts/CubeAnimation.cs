using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeAnimation : MonoBehaviour
{
    [SerializeField] private float duration = 90f;
    [SerializeField] private Ease ease = Ease.Linear;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalRotate(new Vector3(transform.rotation.x, -360, transform.rotation.z), duration, RotateMode.FastBeyond360).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
    }
}
 