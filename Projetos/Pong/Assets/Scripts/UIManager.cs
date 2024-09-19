using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameSettings gameSettings;

    [SerializeField] Image enemyColor;
    [SerializeField] Image playerColor;
    [SerializeField] Image ballColor;
    [SerializeField] TextMeshProUGUI maxScore;
    [SerializeField] TextMeshProUGUI playerSpeed;
    [SerializeField] TextMeshProUGUI enemySpeed;
    [SerializeField] TextMeshProUGUI ballSpeed;

    [SerializeField] private UnityEvent applySettings;

    private void Awake()
    {
        //aplicar as cores que estão no GameSettings
        enemyColor.color = gameSettings.enemyPaddleColor;
        playerColor.color = gameSettings.playerPaddleColor;
        ballColor.color = gameSettings.ballColor;

        maxScore.text = gameSettings.MAX_score.ToString();
        UpdateTextMenu(gameSettings.playerSpeed, playerSpeed);
        UpdateTextMenu(gameSettings.enemySpeed, enemySpeed);
        UpdateTextMenu(gameSettings.ballSpeed, ballSpeed);
        applySettings?.Invoke();
    }

    public void ChangeEnemyColor(int i)
    {
        gameSettings.counterEnemy += i;
        int counterEnemy = gameSettings.VerifyCycle(0);

        Color color = gameSettings.CycleOptionsColor(counterEnemy);
        gameSettings.enemyPaddleColor = color;
        enemyColor.color = color;
        applySettings?.Invoke();
    }
    public void ChangePlayerColor(int i)
    {
        gameSettings.counterPlayer += i;
        int counterPlayer = gameSettings.VerifyCycle(1);

        Color color = gameSettings.CycleOptionsColor(counterPlayer);
        gameSettings.playerPaddleColor = color;
        playerColor.color = color;
        applySettings?.Invoke();
    }
    public void ChangeBallColor(int i)
    {
        gameSettings.counterBall += i;
        int counterBall = gameSettings.VerifyCycle(2);

        Color color = gameSettings.CycleOptionsColor(counterBall);
        gameSettings.ballColor = color;
        ballColor.color = color;
        applySettings?.Invoke();
    }
    public void ChangeVictoryPoints(int i)
    {
        gameSettings.MAX_score += i;
        if (gameSettings.MAX_score <= 1) gameSettings.MAX_score = 1;

        maxScore.text = gameSettings.MAX_score.ToString(); 
    }
    public void ChangeEnemySpeed(float speed)
    {
        gameSettings.enemySpeed += speed;    
        UpdateTextMenu(gameSettings.enemySpeed, enemySpeed);
    }
    public void ChangePlayerSpeed(float speed)
    {
        gameSettings.playerSpeed += speed;
        UpdateTextMenu(gameSettings.playerSpeed, playerSpeed);
    }
    public void ChangeBallAcceleration(float speed)
    {
        gameSettings.ballSpeed += speed;
        if (gameSettings.ballSpeed <= 0.1f) gameSettings.ballSpeed = 0.1f;

        UpdateTextMenu(gameSettings.ballSpeed, ballSpeed);
    }
    public void ChangeGameDifficultyPreset(int i)
    {
        gameSettings.ChangeGameDifficultyPreset(i);

        UpdateTextMenu(gameSettings.playerSpeed, playerSpeed);
        UpdateTextMenu(gameSettings.enemySpeed, enemySpeed);
        UpdateTextMenu(gameSettings.ballSpeed, ballSpeed);
    }
    private void UpdateTextMenu(float value, TextMeshProUGUI menu)
    {
        string texto = FormatFloat(value);
        menu.text = texto;
    }
    private string FormatFloat(float value)
    {
        return value.ToString("F1");
    }
}
