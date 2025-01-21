using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEditor;

// Defines logic for the behaviour of Party Members
public class AIBrain : MonoBehaviour
{

    [TagSelector] 
    public string[] TargetPriorities;
    
    private GameObject target;
    private NavMeshAgent agent;
    private HealthComponent healthComponent;

    private bool bCanNavigate = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(BrainLoop(1.0f));
        healthComponent.HasDied.AddListener(StopBrain);
    }

    private GameObject GetTarget()
    {
        for (int i = 0; i < TargetPriorities.Length; i++)
        {
            GameObject enemy = GameObject.FindGameObjectWithTag(TargetPriorities[i]);
            if (enemy != null)
            {
                return enemy.gameObject;
            }
        }

        return null;
    }

    IEnumerator BrainLoop(float waitTime)
    {
        while (bCanNavigate)
        {
            if (target == null)
            {
                target = GetTarget();
            }
            agent.destination = target.transform.position;
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

    void StopBrain()
    {
        bCanNavigate = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
