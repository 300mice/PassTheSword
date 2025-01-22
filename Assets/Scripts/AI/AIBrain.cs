using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

// Defines logic for the behaviour of Party Members
[RequireComponent(typeof(HealthComponent))]
public class AIBrain : MonoBehaviour
{

    [TagSelector] 
    public string[] TargetPriorities;
    
    private GameObject target;
    private NavMeshAgent agent;
    private HealthComponent healthComponent;
    private DamageComponent damageComponent;

    public float Damage = 5;

    private bool bAlive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        healthComponent = GetComponent<HealthComponent>();
        damageComponent = GetComponent<DamageComponent>();
        StartCoroutine(BrainLoop(0.33f));
        healthComponent.HasDied.AddListener(StopBrain);
    }

    private GameObject GetTarget()
    {
        GameObject chosenTarget = null;
        for (int i = 0; i < TargetPriorities.Length; i++)
        {
            List<AIBrain> _targets =  GameManager.Instance.CurrentBrains;
            foreach (AIBrain target in _targets)
            {
                if (!target || !target.bAlive)
                {
                    break;
                }
                if (target.CompareTag(TargetPriorities[i]))
                {
                    if (!chosenTarget || (chosenTarget.transform.position - transform.position).magnitude >
                        (target.transform.position - transform.position).magnitude)
                    {
                        chosenTarget = target.gameObject;
                    }
                }
            }
            
        }
        return chosenTarget;
    }

    IEnumerator BrainLoop(float waitTime)
    {
        while (bAlive)
        {
            if (!target)
            {
                target = GetTarget();
            }
            agent.destination = target.transform.position;
            if (agent.remainingDistance <= agent.stoppingDistance && target.GetComponent<AIBrain>())
            {
                Attack(target.GetComponent<AIBrain>());
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

    void StopBrain(HealthComponent HealthComponent)
    {
        bAlive = false;
        Destroy(gameObject, 3);
    }

    void Attack(AIBrain TargetBrain)
    {
        if (!bAlive)
        {
            return;
        }
        if (!TargetBrain || !TargetBrain.bAlive)
        {
            return;
        }
        damageComponent.DealDamage(TargetBrain.healthComponent);
    }
}
