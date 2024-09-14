using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum States
    {
        IDLE,
        RUNNING,
        DEAD,
        WIN
    }

    public Dictionary<States, StateBase> dictonaryState;

    private StateBase _currentState;
    public PlayerNave player;
    public float timeToStartGame = 1f;

    [SerializeField] private GameUIManager gameUIManager;

    private void Awake()
    {
        dictonaryState = new Dictionary<States, StateBase>();
        dictonaryState.Add(States.IDLE, new StateIdle());
        dictonaryState.Add(States.RUNNING, new StateRunning());
        dictonaryState.Add(States.DEAD, new StateDead());
        dictonaryState.Add(States.WIN, new StateWin());

        SwitchState(States.IDLE);
        Invoke(nameof(StartGame), timeToStartGame);
    }

    private void StartGame()
    {
        SwitchState(States.RUNNING);
    }

    public void SwitchState(States state)
    {
        if (_currentState != null)
        {
            _currentState.OnStateExit();
        }

        _currentState = dictonaryState[state];
        _currentState.OnStateEnter(player);

        if (state == States.DEAD)
        {
            gameUIManager.ShowGameOver();
        }
        else if (state == States.WIN)
        {
            gameUIManager.ShowYouWin();
        }
    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.OnStateStay();
        }
    }
}
