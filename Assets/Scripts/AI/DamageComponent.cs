using System.Collections;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    public float Damage;
    public float AttackSpeed;
    
    private AIBrain brain;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        brain = GetComponent<AIBrain>();
        if (brain)
        {
            brain.OnStateChange.AddListener(OnStateChange);
        }
    }
    
    IEnumerator DealDamage(GameObject AttackedObject)
    {
        while (AttackedObject && brain.bAlive)
        {
            HealthComponent AttackedComponent = AttackedObject.GetComponent<HealthComponent>();
            float DamageDealt = GameManager.Instance.Sword.Wielder == gameObject.GetComponent<AIBrain>()
                ? GameManager.Instance.Sword.DamageComponent.Damage
                : Damage;
            AttackedComponent.UpdateHealth(-DamageDealt);
            yield return new WaitForSeconds(AttackSpeed);
        }
        
    }

    void OnStateChange(BrainState newState)
    {
        if (newState == BrainState.Attacking)
        {
            StartCoroutine(DealDamage(brain.CurrentAction.Target));
        }
        else
        {
            StopAllCoroutines();
        }
    }
}
