using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject Player;

    public float offsetY = 45f;
    public float offsetZ = -40f;
    Vector3 cameraPosition;
    private void LateUpdate()
    {
        cameraPosition.x = Mathf.Clamp(Player.transform.position.x,-2f,2f);
        cameraPosition.y = Player.transform.position.y+offsetY;
        cameraPosition.z = Player.transform.position.z+offsetZ;

        transform.position = cameraPosition;
    }
}
