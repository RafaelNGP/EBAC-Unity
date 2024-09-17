using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameSettings gameSettings;

    [SerializeField] Image enemyColor;
    [SerializeField] Image playerColor;
    [SerializeField] Image ballColor;

    [SerializeField] private UnityEvent applySettings;

    private void Awake()
    {
        //aplicar as cores que estão no GameSettings
        enemyColor.color = gameSettings.enemyPaddleColor;
        playerColor.color = gameSettings.playerPaddleColor;
        ballColor.color = gameSettings.ballColor;
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

}
