using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class HealthComponent : MonoBehaviour
{
    public float health { get; private set; }= 0;
    
    [SerializeField]
    private float maxHealth = 100;

    public HealthComponentEvent HasDied = new HealthComponentEvent();
    public UnityEvent OnHealthChanged = new UnityEvent();
    public UnityEvent OnShieldChanged = new UnityEvent();
    public float shield;
    public bool bAlive = true;
    public GameObject damageEffect;
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
        float newDelta = delta;
        if (!bAlive)
        {
            return 0;
        } 

        if(delta < 0)
        {
            Instantiate(damageEffect, transform.position+new Vector3(0,3,0), Quaternion.identity);
            if (shield > 0)
            {
                newDelta = UpdateShield(delta);
                if (delta > 0)
                {
                    return health;
                }
            }
        }

        if (CompareTag("PartyMember"))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/PartyHurt", transform.position);
        }

        health = Mathf.Clamp(health += newDelta, 0, maxHealth);
        OnHealthChanged.Invoke();
        IsDead();
        
        return health;
    }

    public float UpdateShield(float delta)
    {
        float originalShield = shield;
        shield = Mathf.Clamp(shield += delta, 0, maxHealth);
        OnShieldChanged.Invoke();
        return originalShield + delta;
    }
    

    public bool IsDead()
    {
        if (!bAlive)
        {
            return true;
        }
        if (health <= 0)
        {
            HasDied.Invoke(this);
            bAlive = false;
            return true;
        }

        return false;
    }
}

[System.Serializable]
public class HealthComponentEvent : UnityEvent<HealthComponent> {}
