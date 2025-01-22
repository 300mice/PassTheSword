using UnityEngine;

public class Sword : MonoBehaviour
{
    private Vector3 startPos;
    private float dragMagnitude;
    private Vector3 dragDirection;
    
    [SerializeField]
    private float ThrowForce = 10f;

    private Rigidbody rb;

    private bool bHeld;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            bHeld = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            bHeld = false;
            if (dragMagnitude > 100.0f)
            {
                Throw();
            }
        }

        if (bHeld)
        {
            dragMagnitude = (Input.mousePosition - startPos).magnitude;
            Vector3 _normalizedMouse = (startPos - Input.mousePosition).normalized;
            dragDirection = new Vector3(_normalizedMouse.x, 0, _normalizedMouse.y);
        }
        
    }

    void Throw()
    {
        rb.AddForce(dragDirection * dragMagnitude * ThrowForce, ForceMode.Impulse);
    }

    
}
