using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Assign this script to the indicator prefabs.
/// </summary>
public class Indicator : MonoBehaviour
{
    [SerializeField] private IndicatorType indicatorType;
    private Image indicatorImage;
    private Text distanceText;
    private HealthComponent healthComponent;
    private Material indicatorMaterial;

    /// <summary>
    /// Gets if the game object is active in hierarchy.
    /// </summary>
    public bool Active
    {
        get
        {
            return transform.gameObject.activeInHierarchy;
        }
    }

    /// <summary>
    /// Gets the indicator type
    /// </summary>
    public IndicatorType Type
    {
        get
        {
            return indicatorType;
        }
    }

    void Awake()
    {
        indicatorImage = transform.GetComponent<Image>();
        distanceText = transform.GetComponentInChildren<Text>();
        indicatorMaterial = new Material(indicatorImage.material);
        indicatorImage.material = indicatorMaterial;
    }

    /// <summary>
    /// Sets the image color for the indicator.
    /// </summary>
    /// <param name="color"></param>
    public void SetImageColor(Color color)
    {
        if (indicatorImage)
        {
            indicatorImage.color = new Color(color.r, color.g, color.b, indicatorImage.color.a);
        }
    }

    /// <summary>
    /// Sets the distance text for the indicator.
    /// </summary>
    /// <param name="value"></param>
    public void SetDistanceText(float value)
    {
        if (distanceText)
        {
            distanceText.text = value >= 0 ? Mathf.Floor(value) + " m" : "";
        }
    }

    /// <summary>
    /// Sets the distance text rotation of the indicator.
    /// </summary>
    /// <param name="rotation"></param>
    public void SetTextRotation(Quaternion rotation)
    {
        if (distanceText)
        {
            distanceText.rectTransform.rotation = rotation;
        }
    }

    public void SetHealthComponent(HealthComponent newhealthComponent)
    {
        if (healthComponent && healthComponent != newhealthComponent)
        {
            healthComponent.OnHealthChanged.RemoveListener(OnHealthChanged);
        }
        else if (healthComponent == newhealthComponent)
        {
            return;
        }
        healthComponent = newhealthComponent;
        if (healthComponent)
        {
            healthComponent.OnHealthChanged.AddListener(OnHealthChanged); 
        }
        OnHealthChanged();
    }

    void OnHealthChanged()
    {
        //indicatorImage.fillAmount = healthComponent.health / healthComponent.GetMaxHealth();
        float newHealth = healthComponent.health / healthComponent.GetMaxHealth() -0.50001f;
        Debug.Log("New health: " + newHealth);
        indicatorMaterial.SetFloat("_Health_Value", newHealth);
    }

    /// <summary>
    /// Sets the indicator as active or inactive.
    /// </summary>
    /// <param name="value"></param>
    public void Activate(bool value)
    {
        transform.gameObject.SetActive(value);
    }
}

public enum IndicatorType
{
    BOX,
    ARROW
}
