using UnityEngine;

[RequireComponent(typeof(Sword))]
public class AddShieldOnEquip : MonoBehaviour
{
    Sword sword;

    public float shieldAmount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        sword = GetComponent<Sword>();
        sword.wielderChange.AddListener(OnEquip);
    }

    void OnEquip(AIBrain brain)
    {
        if (brain)
        {
            brain.healthComponent.UpdateShield(shieldAmount);
        }
    }

    
}
