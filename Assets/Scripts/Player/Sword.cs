using UnityEngine;

public class Sword : MonoBehaviour
{
    private Vector3 startPos;
    private float dragMagnitude;
    private Vector3 dragDirection;

    [SerializeField] private float UnEquippedMagnitude = 100;
    
    [SerializeField]
    private float ThrowForce = 10f;

    private Rigidbody rb;

    private bool bMouseHeld;

    public bool bEquipped { get; private set; } = false;
    
    public AIBrain Wielder {get; private set;}
    
    public DamageComponent DamageComponent {get; private set;}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        DamageComponent = GetComponent<DamageComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            bMouseHeld = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            bMouseHeld = false;
            if (dragMagnitude > 40.0f)
            {
                Throw();
            }
        }

        if (bMouseHeld)
        {
            dragMagnitude = bEquipped? (Input.mousePosition - startPos).magnitude : UnEquippedMagnitude;
            Vector3 _normalizedMouse = (startPos - Input.mousePosition).normalized;
            dragDirection = new Vector3(_normalizedMouse.x, 0, _normalizedMouse.y);
        }
        
    }

    void Throw()
    {
        rb.ResetInertiaTensor();
        Unequip();
        Vector3 targetDirection = Quaternion.Inverse(transform.rotation) * dragDirection;
        rb.AddForce(targetDirection * dragMagnitude * ThrowForce, ForceMode.Impulse);
        
    }
    
    public void Equip(AIBrain NewWielder)
    {
        Wielder = NewWielder;
        transform.position = Wielder.transform.position;
        transform.parent = Wielder.transform;
        bEquipped = true;
    }

    public void Unequip()
    {
        Wielder = null;
        transform.SetParent(null);
        bEquipped = false;
    }

    
}
