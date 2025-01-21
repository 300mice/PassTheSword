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

    private bool bCanNavigate = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(BrainLoop(1.0f));
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
        
            yield return new WaitForSeconds(waitTime);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
