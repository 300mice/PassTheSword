using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    HealthComponent healthComponent;
    public Image healthFill;
    public Color healthFillColor;

    public GameObject targetObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(targetObject != null)
            healthComponent = targetObject.GetComponent<HealthComponent>();
        else
            healthComponent = GetComponentInParent<HealthComponent>();
    }
    
    void Start()
    {
        healthComponent.OnHealthChanged.AddListener(OnHealthChanged);
        healthFill.color = healthFillColor;
    }

    void OnHealthChanged()
    {
        healthFill.fillAmount = healthComponent.health / healthComponent.GetMaxHealth();
    }

    public void SetHealth(HealthComponent h)
    {
        healthComponent = h;
    }
}
