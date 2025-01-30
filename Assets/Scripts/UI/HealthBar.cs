using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    HealthComponent healthComponent;
    public Image healthFill;
    public Color healthFillColor;

    public GameObject targetObject;

    private bool bFlashing;

    private Material _material;

    private Color originalColor;
    private Renderer objectRenderer;
    private MaterialPropertyBlock propertyBlock;
    private float oldHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(targetObject != null)
            healthComponent = targetObject.GetComponent<HealthComponent>();
        else
            healthComponent = GetComponentInParent<HealthComponent>();
        
        objectRenderer = healthFill.GetComponent<Renderer>();
        propertyBlock = new MaterialPropertyBlock();
    }
    
    void Start()
    {
        healthComponent.OnHealthChanged.AddListener(OnHealthChanged);
        _material = new Material(healthFill.material);
        healthFill.material = _material;
        originalColor = _material.GetColor("_Colour");
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
        _material.SetColor("_Colour", originalColor);
        bFlashing = false;
    }
}
