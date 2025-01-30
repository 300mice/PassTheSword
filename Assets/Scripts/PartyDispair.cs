using System;
using System.Collections;
using UnityEngine;

public class PartyDispair : MonoBehaviour
{
    private HealthComponent _healthComponent;
    private bool bDispair;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
    }

    void Start()
    {
        GameManager.Instance.onPartyDeath.AddListener(OnPartyDeath);
    }

    void OnPartyDeath()
    {
        if (GameManager.Instance.PartyRemaining <= 1 && !bDispair)
        {
            bDispair = true;
            StartCoroutine(KillPartyMember());
        }
    }

    IEnumerator KillPartyMember()
    {
        while (_healthComponent)
        {
            float health = _healthComponent.UpdateHealth(-15f);
            yield return new WaitForSeconds(0.25f);
            if (health <= 0)
            {
                break;
            }
        }
    }
}
