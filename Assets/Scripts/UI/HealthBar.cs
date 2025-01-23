using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    HealthComponent healthComponent;
    public Image healthFill;
    public Color healthFillColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthComponent = GetComponentInParent<HealthComponent>();
        healthComponent.OnHealthChanged.AddListener(OnHealthChanged);
        healthFill.color = healthFillColor;
    }

    void OnHealthChanged()
    {
        healthFill.fillAmount = healthComponent.health / healthComponent.GetMaxHealth();
    }
}
