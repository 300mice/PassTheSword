using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    public float Damage;
    public float AttackSpeed;

    private float lastAttacked;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    public bool DealDamage(HealthComponent AttackedComponent)
    {
        if (!AttackedComponent)
        {
            return false;
        }
        if (Time.unscaledTime - lastAttacked < AttackSpeed)
        {
            return false;
        }
        lastAttacked = Time.unscaledTime;
        AttackedComponent.UpdateHealth(-Damage);
        return true;
    }
}
