using UnityEngine;

public class UIFaceCamera : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position, Camera.main.transform.rotation * Vector3.up);
        
    }
}
