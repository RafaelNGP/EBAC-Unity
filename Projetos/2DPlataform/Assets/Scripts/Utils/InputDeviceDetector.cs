using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InputDeviceDetector : MonoBehaviour
{
    [SerializeField] private TMP_Text controlsText;

    private string lastDevice = "";

    void Update()
    {
        if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame)
        {
            if (lastDevice != "Gamepad")
            {
                lastDevice = "Gamepad";
                UpdateTextForGamepad();
            }
        }
        else if (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (lastDevice != "Keyboard")
            {
                lastDevice = "Keyboard";
                UpdateTextForKeyboard();
            }
        }
    }

    void UpdateTextForGamepad()
    {
        controlsText.text = "Press \"X\" to attack\nPress \"A\" to jump";
    }

    void UpdateTextForKeyboard()
    {
        controlsText.text = "Press \"F\" to attack\nPress Space to jump";
    }
}
