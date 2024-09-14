using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI youWinText;

    private void Start()
    {
        gameOverText.gameObject.SetActive(false);
        youWinText.gameObject.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameOverText.gameObject.SetActive(true);
    }

    public void ShowYouWin()
    {
        youWinText.gameObject.SetActive(true);
    }

    public void HideAll()
    {
        gameOverText.gameObject.SetActive(false);
        youWinText.gameObject.SetActive(false);
    }
}
