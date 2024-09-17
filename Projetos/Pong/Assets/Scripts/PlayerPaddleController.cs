using UnityEngine;

public class PlayerPaddleController : MonoBehaviour
{
    [SerializeField] GameSettings settings;

    private void Update()
    {
        MovePaddle();
    }

    private void MovePaddle()
    {
        float moveInput = Input.GetAxis("Vertical");
        Vector3 newPosition = transform.position + Vector3.up * moveInput * settings.playerSpeed * Time.deltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, -4.5f, 4.5f);
        transform.position = newPosition;
    }
}
