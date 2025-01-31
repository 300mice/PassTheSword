using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Upgrade : MonoBehaviour
{
    public UnityEvent OnUpgrade;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void AddPartyMember()
    {
        GameManager.Instance.SpawnPartyMember();
        OnUpgrade.Invoke();
    }

    public void AddShield()
    {
        FindAnyObjectByType<AddShieldOnEquip>().shieldAmount += 50;
        OnUpgrade.Invoke();
    }

    public void AddSpeed()
    {
        FindAnyObjectByType<AddSpeed>().addSpeed += 2.75f;
        OnUpgrade.Invoke();
    }

    public void AddRes()
    {
        FindAnyObjectByType<NecroBlade>().target += 3;
        OnUpgrade.Invoke();
    }
}
