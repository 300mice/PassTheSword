using System;
using UnityEngine;
using UnityEngine.Events;

public class Sword : MonoBehaviour
{
    private Vector3 startPos;
    private float dragMagnitude;
    private Vector3 dragDirection;

    [SerializeField] private float UnEquippedMagnitude = 100;

    private float MaxMagnitude = 450f;
    
    [SerializeField]
    private float ThrowForce = 10f;

    private Rigidbody rb;

    private bool bMouseHeld;

    private float lastEquippedTime;

    public bool bEquipped { get; private set; } = false;
    
    public AIBrain Wielder {get; private set;}
    
    public DamageComponent DamageComponent {get; private set;}

    private SwordMove mover;

    public WielderChangeEvent wielderChange = new WielderChangeEvent();

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        DamageComponent = GetComponent<DamageComponent>();
        mover = GetComponent<SwordMove>();
    }

    private void Start()
    {
        RequestPickup();
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
            dragMagnitude = bEquipped? Mathf.Clamp((Input.mousePosition - startPos).magnitude, UnEquippedMagnitude, MaxMagnitude) : UnEquippedMagnitude;
            Vector3 _normalizedMouse = (startPos - Input.mousePosition).normalized;
            dragDirection = new Vector3(_normalizedMouse.x, 0, _normalizedMouse.y);
        }

        if(Wielder != null)
        {
            
        }
        
    }

    void Throw()
    {
        rb.ResetInertiaTensor();
        //Vector3 targetDirection = Quaternion.Inverse(transform.rotation) * dragDirection;
        rb.AddForce(dragDirection * dragMagnitude * ThrowForce, ForceMode.Impulse);
        Unequip();
        Invoke(nameof(RequestPickup), 0.5f);
    }
    
    void RequestPickup()
    {
        AIBrain closestmember = GameManager.Instance.GetClosestBrainWithTag(transform, "PartyMember");
        if (closestmember)
        {
            closestmember.AddToQueue(ActionType.PickupSword, gameObject);
        }
    }
    
    public void Equip(AIBrain NewWielder)
    {
        if (lastEquippedTime + 1 > Time.time)
        {
            return;
        }
        Wielder = NewWielder;
        //transform.position = Wielder.transform.position;
        //transform.parent = Wielder.transform;
        rb.linearVelocity = Vector3.zero;
        bEquipped = true;
        wielderChange.Invoke(Wielder);
        mover.currentTarget = Wielder.swordPos;


    }

    public void Unequip()
    {
        Wielder = null;
        //transform.SetParent(null);
        bEquipped = false;
        lastEquippedTime = Time.time;
        mover.currentTarget = null;
        wielderChange.Invoke(Wielder);
    }

    

    

    
}

[System.Serializable]
public class WielderChangeEvent : UnityEvent<AIBrain> { }

