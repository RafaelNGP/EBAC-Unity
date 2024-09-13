using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleColors : MonoBehaviour
{
    [SerializeField] private Light light;
    [SerializeField] private MeshRenderer cubeMesh;
    [SerializeField] private Color[] color;

    public int colorIndex = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            light.color = color[colorIndex];
            cubeMesh.material.color = color[colorIndex];
            colorIndex++;
            if (colorIndex >= 5)
            {
                colorIndex = 0;
            }
        }   
    }
}
