using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCameraValues", menuName = "PlayerCameraSO")]
public class PlayerCameraVariables : ScriptableObject
{
    // Start is called before the first frame update

    public float cameraDistance = 2;
    public float minDistance = 0;
    public float maxDistance = -3;
}

