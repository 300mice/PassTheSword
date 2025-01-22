using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public GameObject TrackingObject;

    public float TrackingSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float _distance = Mathf.Clamp((Vector3.Distance(transform.position, TrackingObject.transform.position) / 10.0f), 0.05f, 1.0f);
        Debug.Log(_distance);
        transform.position = Vector3.MoveTowards(transform.position, TrackingObject.transform.position, (TrackingSpeed * _distance)* Time.deltaTime);
    }
}
