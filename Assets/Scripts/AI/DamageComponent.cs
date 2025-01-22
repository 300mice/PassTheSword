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
    
    public void DealDamage(HealthComponent AttackedComponent)
    {
        if (!AttackedComponent)
        {
            return;
        }
        if (Time.unscaledTime - lastAttacked < AttackSpeed)
        {
            return;
        }
        lastAttacked = Time.unscaledTime;
        AttackedComponent.UpdateHealth(-Damage);
    }
}
