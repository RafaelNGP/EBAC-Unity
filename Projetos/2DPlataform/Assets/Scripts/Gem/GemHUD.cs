using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemHUD : MonoBehaviour
{
    [SerializeField] private List<Image> ownedGems;

    private void Awake()
    {
        // Verifica quantos filhos o objeto tem e quantos IMAGE ele tem
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Image childImage = transform.GetChild(i).GetComponent<Image>();
            if (childImage != null)
            {
                ownedGems.Add(childImage);
            }
        }
    }

    private void OnEnable()
    {
        ItemManager.OnGenChanged += UpdateGems;
    }

    private void OnDisable()
    {
        ItemManager.OnGenChanged -= UpdateGems;
    }

    private void UpdateGems(int currentGems)
    {
        ownedGems[currentGems-1].color = Color.white;
    }

    private void ResetHUDImages()
    {
        foreach (Image image in ownedGems)
        {
            image.color = new Color(171, 171, 171, 74);
        }
    }
}
