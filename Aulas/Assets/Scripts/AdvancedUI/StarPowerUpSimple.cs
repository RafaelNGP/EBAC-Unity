using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StarPowerUpSimple : MonoBehaviour
{
    public AudioSource powerUpMusic;
    public Image btnImage;
    private Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta };
    private bool isPoweredUp = false;

    void Start()
    {
        btnImage.color = Color.white; // Cor inicial
    }

    public void TogglePowerUp()
    {
        if (isPoweredUp)
        {
            DeactivatePowerUp();
        }
        else
        {
            ActivatePowerUp();
        }
    }

    private void ActivatePowerUp()
    {
        isPoweredUp = true;
        powerUpMusic.Play();
        StartCoroutine(ChangeColors());
    }

    private void DeactivatePowerUp()
    {
        isPoweredUp = false;
        powerUpMusic.Stop();
        btnImage.color = Color.white; // Reset para a cor original
    }

    private IEnumerator ChangeColors()
    {
        while (isPoweredUp)
        {
            foreach (Color color in colors)
            {
                btnImage.color = color;
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
