using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    [Header("Gameplay")]
    public int MAX_score;
    public float playerSpeed;
    public float difficultyLevel; // 1 = Easy, 2 = Medium, 3 = Hard

    public const int MAX_COLOR = 3;
    
    [Header("Colors")]
    public Color playerPaddleColor;
    public Color enemyPaddleColor;
    public Color ballColor;

    [Header("Counters")]
    public int counterEnemy = 0;
    public int counterPlayer = 0;
    public int counterBall = 0;
    private int colorOption = 0;

    // misc - total match score
    public int legacyScorePlayer = 0;
    public int legacyScoreEnemy = 0;

    public Color CycleOptionsColor(int i)
    {
        colorOption = i;

        if (colorOption < 0) colorOption = 3;
        if (colorOption > 3) colorOption = 0;

        switch (colorOption)
        {
            case 0:
                return Color.white;
            case 1:
                return Color.red;
            case 2:
                return Color.green;            
            case 3:
                return Color.blue;
            default:
                colorOption = 0;
                return Color.white;
        }
    }
    public int VerifyCycle(int i)
    {
        switch (i)
        {
            case 0:
                if (counterEnemy < 0) counterEnemy = MAX_COLOR;
                if (counterEnemy > MAX_COLOR) counterEnemy = 0;
                return counterEnemy;
            case 1:
                if (counterPlayer < 0) counterPlayer = MAX_COLOR;
                if (counterPlayer > MAX_COLOR) counterPlayer = 0;
                return counterPlayer;
            case 2:
                if (counterBall < 0) counterBall = MAX_COLOR;
                if (counterBall > MAX_COLOR) counterBall = 0;
                return counterBall;
            default:
                return i;
        }
    }
}