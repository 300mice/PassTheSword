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

    public bool CanEquipSword = false;

    private bool bAlive = true;

    public Transform swordPos;

    private UnitAnimationController animator;
    // minimum movement speed to play run animation
    public float minRunAnimSpeed = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        healthComponent = GetComponent<HealthComponent>();
        damageComponent = GetComponent<DamageComponent>();
        StartCoroutine(BrainLoop(0.33f));
        healthComponent.HasDied.AddListener(StopBrain);
        animator = GetComponent<UnitAnimationController>();
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
            if (PickUpSword())
            {
                target = GameManager.Instance.Sword.gameObject;
            }
            else
            {
                target = GetTarget();
            }
            agent.destination = target.transform.position;
            if ((agent.destination - transform.position).magnitude <= agent.stoppingDistance)
            {
                Action(target);
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

    void StopBrain(HealthComponent HealthComponent)
    {
        bAlive = false;
        if (GameManager.Instance.Sword.Wielder == this)
        {
            GameManager.Instance.Sword.Unequip();
        }
        Destroy(gameObject, 3);
    }

    void Action(GameObject Target)
    {
        if (agent.velocity.magnitude > minRunAnimSpeed)
        {
            animator.PlayAnimation("run", 0);
        }
        else
        {
            //animator.PlayAnimation("idle", 0);
        }

        AIBrain TargetBrain = Target.GetComponent<AIBrain>();
        if (!bAlive)
        {
            return;
        }
        if (!TargetBrain)
        {
            Sword sword = Target.gameObject.GetComponent<Sword>();
            if (sword)
            {
                if (!PickUpSword())
                {
                    target = null;
                    return;
                }
                sword.Equip(this);
            }
            return;
        }

        if (!TargetBrain.bAlive)
        {
            return;
        }
        if (GameManager.Instance.Sword.Wielder == this)
        {
            if (GameManager.Instance.Sword.DamageComponent.DealDamage(TargetBrain.healthComponent))
            {
                animator.PlayAnimation("attack", 0);
            }

        }



        if (damageComponent.DealDamage(TargetBrain.healthComponent))
        {
            animator.PlayAnimation("attack", 0);
        }
        
    }

    bool PickUpSword()
    {
        if (!CanEquipSword)
        {
            return false;
        }

        return !GameManager.Instance.Sword.bEquipped;
    }

    public GameObject ReturnTarget()
    {
        return target;
    }


    
}
