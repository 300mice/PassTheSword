using UnityEngine;
using UnityEngine.AI;

public class AddSpeed : MonoBehaviour
{
    public float addSpeed = 2.5f;
    private AIBrain focusBrain;
    Sword sword;
    void Awake()
    {
        sword = GetComponent<Sword>();
        sword.wielderChange.AddListener(OnEquip);
    }
    
    void OnEquip(AIBrain brain)
    {
        if (!brain && focusBrain)
        {
            brain.GetComponent<NavMeshAgent>().speed = 2.5f;
        }
        
        if (brain)
        {
            brain.GetComponent<NavMeshAgent>().speed = addSpeed;
        }
    }
}
