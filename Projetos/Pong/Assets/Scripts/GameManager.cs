using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform playerPaddle;
    public Transform enemyPaddle;
    public BallController ballController;

    public int playerScore = 0;
    public int enemyScore = 0;
    public TextMeshProUGUI textPointsPlayer;
    public TextMeshProUGUI textPointsEnemy;

    public int winPoints;

    private const float playerStartX = -7f;
    private const float enemyStartX = 7f;
    private const float startY = 0f;

    public delegate void GameEvent();
    public static event GameEvent OnScoreChanged;
    public static event GameEvent OnGameReset;

    private void Start()
    {
        ResetGame();
    }

    public void CheckWin()
    {
        if (enemyScore >= winPoints || playerScore >= winPoints)
        {
            ResetGame();
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
        OnGameReset?.Invoke();
    }

    public void ScorePlayer()
    {
        playerScore++;
        UpdateScoreUI();
        CheckWin();
        OnScoreChanged?.Invoke();
    }

    public void ScoreEnemy()
    {
        enemyScore++;
        UpdateScoreUI();
        CheckWin();
        OnScoreChanged?.Invoke();
    }

    private void UpdateScoreUI()
    {
        textPointsEnemy.text = enemyScore.ToString();
        textPointsPlayer.text = playerScore.ToString();
    }
}
