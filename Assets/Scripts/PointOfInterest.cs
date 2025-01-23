using Unity.VisualScripting;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    private AIBrain Guardian;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void RequestGuardian()
    {
        Guardian = GameManager.Instance.GetClosestBrainWithTag(transform, "PartyMember");
        Guardian.AddToQueue(ActionType.GoToPointOfInterest, gameObject);
    }

    public void AssignGuardian(AIBrain guardian)
    {
        Guardian = guardian;
    }
}
