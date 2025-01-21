using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    HealthComponent healthComponent;
    Slider healthSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthComponent = GetComponentInParent<HealthComponent>();
        healthSlider = GetComponent<Slider>();
        healthComponent.OnHealthChanged.AddListener(OnHealthChanged);
    }

    void OnHealthChanged()
    {
        healthSlider.value = healthComponent.health / healthComponent.GetMaxHealth();
    }
}
