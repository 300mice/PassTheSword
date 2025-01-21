using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    protected int health;
    protected int maxHealth;

    public UnityEvent HasDied = new UnityEvent();
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public int UpdateHealth(int delta)
    {
        health = Mathf.Clamp(health += delta, 0, maxHealth);
        return health;
    }
    
    public int GetHealth()
    {
        return health;
    }

    public bool IsDead()
    {
        if (health <= 0)
        {
            HasDied.Invoke();
            return true;
        }

        return false;
    }
}
