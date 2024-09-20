using System;
using UnityEngine;

/*
 * Exemplo de como construir uma StateMachine para não precisar ficar recriando toda hora. 
 */

public class StateMachine : MonoBehaviour
{
    public GameManager gameManager;
    public GameState currentState;
    private GameState gameMode;

    //[Header("Game Screens")]
    //Exemplo aqui de colocar 

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
              //mainMenu.SetActive(true);
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

    public enum GameState
    {
        MainMenu,
        VsCPU,
        VsPlayer,
        MenuOptions,
        EndGame
    }
}
