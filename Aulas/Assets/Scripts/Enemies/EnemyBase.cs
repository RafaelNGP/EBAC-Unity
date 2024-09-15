using TMPro;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;

    public void Awake()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().material.color = enemyData.Color;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "HP: " + enemyData.baseLife;
    }
}
