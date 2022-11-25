using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;       
    }
}
