using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayerPaddleController : MonoBehaviour
{
    [SerializeField] GameSettings gameSettings;

    // Update is called once per frame
    void Update()
    {
        MovePaddle();
    }
    private void MovePaddle()
    {
        float moveInput = 0f;

        if (Input.GetKey(KeyCode.PageUp))
        {
            moveInput = 1f;
        }
        else if (Input.GetKey(KeyCode.PageDown))
        {
            moveInput = -1f;
        }

        Vector3 newPosition = transform.position + Vector3.up * moveInput * gameSettings.playerSpeed * Time.deltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, -4.5f, 4.5f);
        transform.position = newPosition;
    }

}
