using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;
public class AnimalInputEnum : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] List<Sprite> listSprites;
    [SerializeField] TextMeshProUGUI numberTyped;
    [SerializeField] TextMeshProUGUI nameAnimal;
    [SerializeField] UnityEvent eventDebug;
    enum EnumAnimais
    {
        None,
        Crow,
        Octorok,
        Moblin
    };

    private EnumAnimais animalSelecionado;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animalSelecionado = EnumAnimais.Octorok;
            eventDebug?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animalSelecionado = EnumAnimais.Crow;
            eventDebug?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            animalSelecionado = EnumAnimais.Moblin;
            eventDebug?.Invoke();
        }

        switch (animalSelecionado)
        {
            case EnumAnimais.Octorok:
                ChangeMob("1", "Octorok", 0);
                break;
            case EnumAnimais.Crow:
                ChangeMob("2", "Crow", 1);
                break;
            case EnumAnimais.Moblin:
                ChangeMob("3", "Moblin", 2);
                break;
        }
    }

    public void ChangeMob(string number, string  name, int spriteNumber)
    {
        numberTyped.text = number;
        nameAnimal.text = name;
        image.sprite = listSprites[spriteNumber];
    }

    public void DebugAnimalSelecionado()
    {
        Debug.Log(animalSelecionado);
    }
}
