using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameState currentState;
    private GameState gameMode;

    [Header("Game Screens")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject vsCPU;
    [SerializeField] private GameObject vsPlayer;
    [SerializeField] private GameObject endGame;

    public event Action OnMainMenu;
    public event Action OnVsCPU;
    public event Action OnVsPlayer;
    public event Action OnMenuOptions;
    public event Action OnEndGame;


    void Start()
    {
        OnMainMenu += () => SelectOption(0);
        OnVsCPU += () => SelectOption(1);
        OnVsPlayer += () => SelectOption(2);
        OnMenuOptions += () => SelectOption(3);
        OnEndGame += () => SelectOption(4);

        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        Debug.Log("Estado do jogo alterado para: " + currentState);
        HandleStateChange();
    }
    public void SelectOption(int index)
    {
        switch (index)
        {
            case 0: // Main Menu
                mainMenu.SetActive(true);
                vsCPU.SetActive(false);
                vsPlayer.SetActive(false);
                endGame.SetActive(false);
                options.SetActive(false);
                break;
            case 1: // Gameplay vs Comp
                mainMenu.SetActive(false);
                vsCPU.SetActive(true);
                vsPlayer.SetActive(false);   
                endGame.SetActive(false);
                options.SetActive(false);
                gameManager.DetectGameMode();
                break;
            case 2: // Extra - vsPlayer
                mainMenu.SetActive(false);
                vsCPU.SetActive(false);
                vsPlayer.SetActive(true);
                endGame.SetActive(false);
                options.SetActive(false);
                gameManager.DetectGameMode();
                break;
            case 3: // Options
                mainMenu.SetActive(false);
                vsCPU.SetActive(false);
                vsPlayer.SetActive(false);
                endGame.SetActive(false);
                options.SetActive(true);
                break;
            case 4: // End of game
                mainMenu.SetActive(false);
                vsCPU.SetActive(false);
                vsPlayer.SetActive(false);
                endGame.SetActive(true);
                options.SetActive(false);
                break;
        }
    }
    public void ClickOnUI(int i)
    {
        switch (i)
        {
            case 0:
                ChangeState(GameState.MainMenu); 
                break;
            case 1:
                ChangeState(GameState.VsCPU);
                gameMode = GameState.VsCPU;
                gameManager.ResetGame();
                break;
            case 2:
                ChangeState(GameState.VsPlayer);
                gameMode = GameState.VsPlayer;
                gameManager.ResetGame(); 
                break;
            case 3:
                ChangeState(GameState.MenuOptions);
                break;
            case 4:
                ChangeState(GameState.EndGame);
                break;
        }
    }
    public void Rematch()
    {
        if (gameMode == GameState.VsCPU)
        {
            ChangeState(GameState.VsCPU);
        }
        if (gameMode == GameState.VsPlayer)
        {
            ChangeState(GameState.VsPlayer);
        }
    }

    public void HandleStateChange()
    {
        switch (currentState)
        {
            case GameState.MainMenu:
                OnMainMenu?.Invoke();
                break;
            case GameState.VsCPU:
                OnVsCPU?.Invoke();
                gameManager.ResetGame();
                break;
            case GameState.VsPlayer:
                OnVsPlayer?.Invoke();
                gameManager.ResetGame();
                break;
            case GameState.MenuOptions:
                OnMenuOptions?.Invoke();
                break;
            case GameState.EndGame:
                OnEndGame?.Invoke();
                break;
        }
    }
}
