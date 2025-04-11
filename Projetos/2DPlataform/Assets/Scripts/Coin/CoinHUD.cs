using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;

    private void OnEnable()
    {
        ItemManager.OnCoinChanged += UpdateText;
    }

    private void OnDisable()
    {
        ItemManager.OnCoinChanged -= UpdateText;
    }

    private void Start()
    {
        // Atualiza com valor atual logo no início
        UpdateText(ItemManager.Instance.coins);
    }

    private void UpdateText(int currentCoins)
    {
        coinText.text = currentCoins.ToString();
    }
}
