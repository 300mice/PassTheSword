using UnityEngine;

public class GuyPointer : MonoBehaviour
{

    public AIBrain target;
    public GameObject ui;

    private HealthBar healthBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetHealth(target.GetComponent<HealthComponent>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Change(bool b)
    {

    }
}
