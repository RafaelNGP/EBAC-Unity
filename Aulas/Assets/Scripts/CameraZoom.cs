using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private PlayerCameraVariables playerCameraVariables;
    [SerializeField] private List<GameObject> listVirtualCameras;

    private void Start()
    {
        playerCameraVariables.cameraDistance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0)
        {
            if (playerCameraVariables.cameraDistance <= 0)
            {
                playerCameraVariables.cameraDistance += scroll;
                if (playerCameraVariables.cameraDistance > playerCameraVariables.minDistance) playerCameraVariables.cameraDistance = playerCameraVariables.minDistance;
                if (playerCameraVariables.cameraDistance < -2) playerCameraVariables.cameraDistance = (playerCameraVariables.maxDistance * -1);

                ChangeCamera();
            }
        }
    }

    public void ChangeCamera()
    {
        switch (playerCameraVariables.cameraDistance)
        {
            case 0:
                //ativar 1thPerson
                listVirtualCameras[0].SetActive(true);
                listVirtualCameras[1].SetActive(false);
                listVirtualCameras[2].SetActive(false);
                break;
            case -1:
                //ativar 3th versao 1
                listVirtualCameras[0].SetActive(false);
                listVirtualCameras[1].SetActive(true);
                listVirtualCameras[2].SetActive(false);
                break;
            case -2:
                //ativar 3th versao 2
                listVirtualCameras[0].SetActive(false);
                listVirtualCameras[1].SetActive(false);
                listVirtualCameras[2].SetActive(true);
                break;
        }
    }
}
