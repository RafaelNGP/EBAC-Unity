using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDOTweenAnimation : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] Ease ease;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveZ(-10f, duration)
            .SetEase(ease)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
