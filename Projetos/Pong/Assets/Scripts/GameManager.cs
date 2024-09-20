using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private GameStateManager gameState;

    [Header("Game Objects")]
    [SerializeField] public Transform playerPaddle;
    [SerializeField] public Transform enemyPaddle;
    [SerializeField] public BallController ballController;

    [Header("SFX")]
    [SerializeField] public AudioSource menuSound;
    [SerializeField] public AudioSource ballSFX;
    [SerializeField] public AudioSource endingSFX;

    [Header("Game Modes")]
    [SerializeField] private GameObject vsCPU;
    [SerializeField] private GameObject vsPlayer;

    [Header("Points")]
    [SerializeField] private TextMeshProUGUI textPointsPlayer;
    [SerializeField] private TextMeshProUGUI textPointsEnemy;
    [SerializeField] private TextMeshProUGUI gameResult;
    private int enemyScore = 0;
    private int playerScore = 0;

    [Header("Reset Values")]
    private const float playerStartX = -7f;
    private const float enemyStartX = 7f;
    private const float startY = 0f;

    public void ScorePlayer()
    {
        playerScore++;
        gameSettings.legacyScorePlayer++;
        UpdateScoreUI();
        CheckWin();
    }
    public void ScoreEnemy()
    {
        enemyScore++;
        gameSettings.legacyScoreEnemy++;
        UpdateScoreUI();
        CheckWin();
    }
    private void UpdateScoreUI()
    {
        textPointsEnemy.text = enemyScore.ToString();
        textPointsPlayer.text = playerScore.ToString();
    }
    public void CheckWin()
    {
        if (gameState.currentState == GameState.VsCPU)
        {
            if (enemyScore >= gameSettings.MAX_score || playerScore >= gameSettings.MAX_score)
            {
                endingSFX.Play();
                CheckIfAnyName();
                // Check who won
                gameResult.text = (playerScore > enemyScore) ? gameSettings.player1 + " win!" : "You lost!";
            
                // Ativar o canvas e oferecer opções para voltar menu ou reiniciar jogo
                gameState.ChangeState(GameState.EndGame);
            }
        }

        if (gameState.currentState == GameState.VsPlayer)
        {
            if (enemyScore >= gameSettings.MAX_score || playerScore >= gameSettings.MAX_score)
            {
                endingSFX.Play();
                CheckIfAnyName();
                // Check who won
                gameResult.text = (playerScore > enemyScore) ? gameSettings.player1 + " win!" : gameSettings.player2+ " win!";

                // Ativar o canvas e oferecer opções para voltar menu ou reiniciar jogo
                gameState.ChangeState(GameState.EndGame);
            }
        }
    }
    public void ResetGame()
    {
        playerPaddle.position = new Vector3(playerStartX, startY, 0f);
        enemyPaddle.position = new Vector3(enemyStartX, startY, 0f);
        ballController.ResetBall();

        playerScore = 0;
        enemyScore = 0;
        UpdateScoreUI();
    }

    public void DetectGameMode()
    {
        // Método que vai verificar vsComp ou VsPlayer, e identificar seus componentes
        if (gameState.currentState == GameState.VsCPU)
        {
            playerPaddle = vsCPU?.GetComponentInChildren<PlayerPaddleController>().transform;
            enemyPaddle = vsCPU?.GetComponentInChildren<EnemyPaddleController>().transform;
            ballController = vsCPU?.GetComponentInChildren<BallController>();
            
            Canvas canvas = vsCPU?.GetComponentInChildren<Canvas>();
            textPointsPlayer = canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            textPointsEnemy = canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }

        if (gameState.currentState == GameState.VsPlayer)
        {
            playerPaddle = vsPlayer?.GetComponentInChildren<PlayerPaddleController>().transform;
            enemyPaddle = vsPlayer?.GetComponentInChildren<SecondPlayerPaddleController>().transform;
            ballController = vsPlayer?.GetComponentInChildren<BallController>();

            Canvas canvas = vsPlayer?.GetComponentInChildren<Canvas>();
            textPointsPlayer = canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            textPointsEnemy = canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }
    }

    private void CheckIfAnyName()
    {
        if (string.IsNullOrEmpty(gameSettings.player1) || string.IsNullOrEmpty(gameSettings.player2))
        {
            gameSettings.player1 = "Player 1";
            gameSettings.player2 = "Player 2";
        }
    }
}