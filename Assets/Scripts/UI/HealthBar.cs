using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    HealthComponent healthComponent;
    public Image healthFill;
    public Color healthFillColor;

    public GameObject targetObject;
    public GameObject john;

    private bool bFlashing;

    private Material _material;
    
    private float oldHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(targetObject != null)
            healthComponent = targetObject.GetComponent<HealthComponent>();
        else
            healthComponent = GetComponentInParent<HealthComponent>();
        SpriteRenderer sprite = john.GetComponent<SpriteRenderer>();
        if (sprite)
        {
            sprite.material = new Material(sprite.material);
            sprite.material.SetColor("_Outline_Color", healthFillColor);
        }
    }
    
    void Start()
    {
        healthComponent.OnHealthChanged.AddListener(OnHealthChanged);
        _material = new Material(healthFill.material);
        healthFill.material = _material;
        _material.SetColor("_Colour", healthFillColor);
        oldHealth = healthComponent.health;
    }

    void OnHealthChanged()
    {
        healthFill.fillAmount = healthComponent.health / healthComponent.GetMaxHealth();
        
        if (!bFlashing)
        {
            bFlashing = true;
            if (healthComponent.health - oldHealth > 0)
            {
                StartCoroutine(HealthbarFlash(Color.green));
            }
            else
            {
                StartCoroutine(HealthbarFlash(Color.white));
            }
            
        }
        oldHealth = healthComponent.health;
        
    }

    public void SetHealth(HealthComponent h)
    {
        healthComponent = h;
    }

    IEnumerator HealthbarFlash(Color newColor)
    {
        _material.SetColor("_Colour",newColor);
        yield return new WaitForSeconds(0.1f);
        _material.SetColor("_Colour", healthFillColor);
        bFlashing = false;
    }
}
