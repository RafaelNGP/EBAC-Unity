using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuButtonManager : MonoBehaviour
{
    public List<GameObject> list;
    public float duration = .2f;
    public float delay = .05f;

    private void Awake()
    {
        HideAllButtons();
        ShowButtons();
    }

    private void HideAllButtons()
    {
        foreach (var b in list)
        {
            b.transform.localScale = Vector3.zero;
            b.SetActive(false);
        }
    }

    public void ShowButtons() {

        for (int i = 0; i < list.Count; i++)
        {
            var b = list[i];
            b.SetActive(true);
            b.transform.DOScale(1, duration).SetDelay(i * delay);
        }

    }
}
