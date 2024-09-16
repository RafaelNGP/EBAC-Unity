using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] private Transform playerPaddle;
    [SerializeField] private Transform enemyPaddle;
    [SerializeField] private BallController ballController;    
    
    [Header("Points")]
    [SerializeField] private TextMeshProUGUI textPointsPlayer;
    [SerializeField] private TextMeshProUGUI textPointsEnemy;
    [SerializeField] private int winPoints;
    private int enemyScore = 0;
    private int playerScore = 0;

    [Header("Reset Values")]
    private const float playerStartX = -7f;
    private const float enemyStartX = 7f;
    private const float startY = 0f;

    [Header("Game Screens")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject gameplay;
    [SerializeField] private GameObject endGame;

    /*
    public delegate void GameEvent();
    public static event GameEvent OnScoreChanged;
    public static event GameEvent OnGameReset;
    */
    private void Start()
    {
        ResetGame();
    }

    public void ScorePlayer()
    {
        playerScore++;
        UpdateScoreUI();
        CheckWin();
        //OnScoreChanged?.Invoke();
    }

    public void ScoreEnemy()
    {
        enemyScore++;
        UpdateScoreUI();
        CheckWin();
        //OnScoreChanged?.Invoke();
    }

    private void UpdateScoreUI()
    {
        textPointsEnemy.text = enemyScore.ToString();
        textPointsPlayer.text = playerScore.ToString();
    }
    public void CheckWin()
    {
        if (enemyScore >= winPoints || playerScore >= winPoints)
        {
            // Ativar o canvas e oferecer opções para voltar menu ou reiniciar jogo
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
    }

    public void SelectOption(int index)
    {
        switch (index)
        {
            case 0: // Main Menu
                mainMenu.SetActive(true);
                gameplay.SetActive(false);
                endGame.SetActive(false);
                options.SetActive(false);
                break;
            case 1: // Gameplay
                mainMenu.SetActive(false);
                gameplay.SetActive(true);
                endGame.SetActive(false);
                options.SetActive(false);
                break;
            case 2: // End of game
                mainMenu.SetActive(false);
                gameplay.SetActive(false);
                endGame.SetActive(true);
                options.SetActive(false);
                break;
            case 3: // Options
                mainMenu.SetActive(false);
                gameplay.SetActive(false);
                endGame.SetActive(false);
                options.SetActive(true);
                break;
        }
    }
}
