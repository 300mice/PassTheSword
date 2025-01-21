using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class HealthComponent : MonoBehaviour
{
    public float health { get; private set; }= 0;
    public float maxHealth { get; private set; } = 100;

    public UnityEvent HasDied = new UnityEvent();
    public UnityEvent OnHealthChanged = new UnityEvent();
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateHealth(maxHealth);
    }

    public float UpdateHealth(float delta)
    {
        health = Mathf.Clamp(health += delta, 0, maxHealth);
        OnHealthChanged.Invoke();
        IsDead();
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
