using UnityEngine;

public class DeckClickHandler : MonoBehaviour
{
    public GameManager gameManager;

    void OnMouseDown()
    {
        if (gameManager.IsDeckClickable())
        {
            gameManager.ToggleRadialMenu();
        }
    }
}
