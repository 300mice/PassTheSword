using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PointOfInterest : MonoBehaviour
{
    private SphereCollider _sphereCollider;
    private AIBrain Guardian;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
    }


    void Start()
    {
        RequestGuardian();
    }

    void RequestGuardian()
    {
        AIBrain RequestedPlayer = GameManager.Instance.GetClosestBrainWithTag(transform, "PartyMember");
        if (!RequestedPlayer)
        {
            Invoke(nameof(RequestGuardian), 3);
            return;
        }
        RequestedPlayer.AddToQueue(ActionType.GoToPointOfInterest, gameObject);
        RequestedPlayer.GetComponent<HealthComponent>().HasDied.AddListener(RemoveGuardian);
        
    }

    public void AssignGuardian(AIBrain guardian)
    {
        Guardian = guardian;
    }

    private void RemoveGuardian(HealthComponent healthComponent)
    {
        Guardian = null;
        RequestGuardian();
    }

    

    private void OnTriggerExit(Collider other)
    {
        RemoveGuardian(null);
    }
}
