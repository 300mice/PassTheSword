using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class HealthComponent : MonoBehaviour
{
    public float health { get; private set; }= 0;
    
    [SerializeField]
    private float maxHealth = 100;

    public UnityEvent HasDied = new UnityEvent();
    public UnityEvent OnHealthChanged = new UnityEvent();

    public bool bAlive = true;
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateHealth(maxHealth);
    }

    public float UpdateHealth(float delta)
    {
        if (!bAlive)
        {
            return 0;
        } 
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
            bAlive = false;
            return true;
        }

        return false;
    }
}
