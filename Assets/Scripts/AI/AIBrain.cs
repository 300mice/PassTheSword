using UnityEngine;
using UnityEngine.AI;
using System.Collections;
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

    public float Damage = 5;

    private bool bAlive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        healthComponent = GetComponent<HealthComponent>();
        StartCoroutine(BrainLoop(1.0f));
        healthComponent.HasDied.AddListener(StopBrain);
    }

    private GameObject GetTarget()
    {
        GameObject chosenTarget = null;
        for (int i = 0; i < TargetPriorities.Length; i++)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(TargetPriorities[i]);
            foreach (GameObject enemy in enemies)
            {
                AIBrain brain = enemy.GetComponent<AIBrain>();
                if (!brain)
                {
                    break;
                }
                if (brain.bAlive == false)
                {
                    break;
                }
                if (!chosenTarget || (chosenTarget.transform.position - transform.position).magnitude >
                    (enemy.transform.position - transform.position).magnitude)
                {
                    chosenTarget = enemy;
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

    void StopBrain()
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
        if (!TargetBrain)
        {
            return;
        }
        if (!TargetBrain.healthComponent)
        {
            return;
        }
        if (!TargetBrain.bAlive)
        {
            return;
        }
        TargetBrain.healthComponent.UpdateHealth(-Damage);
    }
}
