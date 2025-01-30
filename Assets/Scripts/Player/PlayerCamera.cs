using System.Collections;
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
        transform.position = Vector3.MoveTowards(transform.position, TrackingObject.transform.position, (TrackingSpeed * _distance)* Time.deltaTime);
    }

    public void CameraShake(float duration, float shakeAmount)
    {
        StartCoroutine(Shake(duration, shakeAmount));
    }

    IEnumerator Shake(float duration, float shakeAmount)
    {
        float time = 0.0f;
        Vector3 originalPos = transform.localPosition;
        while (time <= duration)
        {
            transform.position += Random.insideUnitSphere * shakeAmount;
            yield return new WaitForSeconds(0.016f);
            time += 0.016f;
        }
    }
}
