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

    [SerializeField] private UnityEvent applySettings;

    private void Awake()
    {
        //aplicar as cores que estão no GameSettings
        enemyColor.color = gameSettings.enemyPaddleColor;
        playerColor.color = gameSettings.playerPaddleColor;
        ballColor.color = gameSettings.ballColor;
        maxScore.text = gameSettings.MAX_score.ToString();
        playerSpeed.text = gameSettings.playerSpeed.ToString();
        enemySpeed.text = gameSettings.difficultyLevel.ToString();
        applySettings?.Invoke();

        /* TODO: 
         * Create preset for difficult mode
         * set ball velocity
         * set recommended values label.
         * adjust textMeshProUGUI to display only 3 caracteres (0.3)
         */
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
        gameSettings.difficultyLevel += speed;
        enemySpeed.text = gameSettings.difficultyLevel.ToString();
    }
    public void ChangePlayerSpeed(float speed)
    {
        gameSettings.playerSpeed += speed;
        playerSpeed.text = gameSettings.playerSpeed.ToString();
    }
}
