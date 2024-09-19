using UnityEngine;

public class PlayerPaddleController : MonoBehaviour
{
    [SerializeField] GameSettings settings;
    [SerializeField] BallController ballController;

    [SerializeField] GameStateManager gameState;

    private void Update()
    {
        MovePaddle();
        CheckButtons();
    }

    private void MovePaddle()
    {
        float moveInput = Input.GetAxis("Vertical");
        Vector3 newPosition = transform.position + Vector3.up * moveInput * settings.playerSpeed * Time.deltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, -4.5f, 4.5f);
        transform.position = newPosition;
    }

    private void CheckButtons()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ballController.ResetBall();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameState.ChangeState(GameState.MainMenu);
        }
    }
}
