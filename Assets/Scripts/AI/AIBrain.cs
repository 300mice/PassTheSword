using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.Events;

// Defines logic for the behaviour of Party Members
[RequireComponent(typeof(HealthComponent))]
public class AIBrain : MonoBehaviour
{

    [TagSelector] 
    public string[] TargetPriorities;
    private NavMeshAgent agent;
    private HealthComponent healthComponent;
    private DamageComponent damageComponent;

    public bool CanEquipSword = false;

    private bool bAlive = true;

    public Transform swordPos;

    private UnitAnimationController animator;
    // minimum movement speed to play run animation
    public float minRunAnimSpeed = 0.5f;
    
    private List<Action> QueuedActions = new List<Action>();
    public Action CurrentAction = new Action();

    public BrainState CurrentState;
    public StateChangeEvent OnStateChange = new StateChangeEvent();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentAction.Priority = 999;
        agent = GetComponent<NavMeshAgent>();
        healthComponent = GetComponent<HealthComponent>();
        damageComponent = GetComponent<DamageComponent>();
        healthComponent.HasDied.AddListener(StopBrain);
        animator = GetComponent<UnitAnimationController>();
        AddToQueue(ActionType.AttackEnemy, null);
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

    /*IEnumerator BrainLoop(float waitTime)
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
*/
    void StopBrain(HealthComponent HealthComponent)
    {
        bAlive = false;
        if (GameManager.Instance.Sword.Wielder == this)
        {
            GameManager.Instance.Sword.Unequip();
        }
        Destroy(gameObject, 3);
    }

    /*void Action(GameObject Target)
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
        



        if (damageComponent.DealDamage(TargetBrain.healthComponent))
        {
            animator.PlayAnimation("attack", 0);
        }
        
    }*/

    bool PickUpSword()
    {
        if (!CanEquipSword)
        {
            return false;
        }

        return !GameManager.Instance.Sword.bEquipped;
    }

    public void AddToQueue(ActionType action, GameObject target)
    {
        Action newAction = new Action();
        newAction.ActionType = action;
        newAction.Target = target;
        newAction.Priority = 99;
        switch (action)
        {
            case ActionType.AttackEnemy:
                newAction.Priority = 0;
                break;
            case ActionType.PickupSword:
                newAction.Priority = 1;
                break;
            case ActionType.GoToPointOfInterest:
                newAction.Priority = 2;
                break;
        }
        QueuedActions.Add(newAction);
        if (newAction.Priority < CurrentAction.Priority)
        {
            QueuedActions.Add(CurrentAction);
            StopCurrentAction();
            MoveThroughQueue();
        }
    }

    void MoveThroughQueue()
    {
        Action ChosenAction = GetActionInQueue();
        CurrentAction = ChosenAction;
        switch (CurrentAction.ActionType)
        {
            case ActionType.AttackEnemy:
                StartCoroutine(AttackAction());
            break;
            case ActionType.PickupSword:
                StartCoroutine(SwordAction());
            break;
            case ActionType.GoToPointOfInterest:
                StartCoroutine(MoveToInterestAction());
            break;
        }
    }

    void StopCurrentAction()
    {
        switch (CurrentAction.ActionType)
        {
            case ActionType.AttackEnemy:
                StopCoroutine(AttackAction());
                break;
            case ActionType.PickupSword:
                StopCoroutine(SwordAction());
                break;
            case ActionType.GoToPointOfInterest:
                StopCoroutine(MoveToInterestAction());
                break;
        }
    }

    Action GetActionInQueue()
    {
        int HighestPrioAction = 999;
        Action ChosenAction = new Action();
        ChosenAction.ActionType = ActionType.AttackEnemy;
        ChosenAction.Priority = 4;
        foreach (Action a in QueuedActions)
        {
            if (HighestPrioAction > a.Priority)
            {
                HighestPrioAction = a.Priority;
                ChosenAction = a;
            }
        }
        QueuedActions.Remove(ChosenAction);
        return ChosenAction;
    }

    IEnumerator SwordAction()
    {
        agent.destination = CurrentAction.Target.transform.position;
        while ((agent.destination - transform.position).magnitude > agent.stoppingDistance)
        {
            UpdateState(BrainState.Running);
            yield return new WaitForSeconds(0.25f);
        }
        Sword sword = CurrentAction.Target.gameObject.GetComponent<Sword>();
        sword.Equip(this);
        CurrentAction = new Action();
        UpdateState(BrainState.Idle);
        MoveThroughQueue();
    }

    IEnumerator MoveToInterestAction()
    {
        agent.destination = CurrentAction.Target.transform.position;
        while ((agent.destination - transform.position).magnitude > agent.stoppingDistance)
        {
            UpdateState(BrainState.Running);
            yield return new WaitForSeconds(0.25f);
        }
        PointOfInterest pointOfInterest = CurrentAction.Target.GetComponent<PointOfInterest>();
        pointOfInterest.AssignGuardian(this);
        CurrentAction = new Action();
        UpdateState(BrainState.Idle);
        MoveThroughQueue();
    }

    IEnumerator AttackAction()
    {
        while (true)
        {
            GameObject target = GetTarget();
            CurrentAction.Target = target;
            if (!CurrentAction.Target)
            {
                yield return new WaitForSeconds(0.25f);
                continue;
            }
            agent.destination = target.transform.position;
            if ((agent.destination - transform.position).magnitude > agent.stoppingDistance)
            {
                UpdateState(BrainState.Attacking);
            }
            else
            {
                UpdateState(BrainState.Running);
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    bool UpdateState(BrainState newState)
    {
        if (newState == CurrentState)
        {
            return false;
        }
        CurrentState = newState;
        OnStateChange.Invoke(CurrentState);
        return true;
    }

    /*void Attack()
    {
        if (GameManager.Instance.Sword.Wielder == this)
        {
            GameManager.Instance.Sword.DamageComponent.DealDamage(CurrentAction.Target);
            return;
        }
        damageComponent.DealDamage(CurrentAction.Target);
    }*/
}



public enum ActionType
{
    PickupSword,
    GoToPointOfInterest,
    AttackEnemy
}

public struct Action
{
    public ActionType ActionType;
    public int Priority;
    public GameObject Target;
}

public enum BrainState
{
    Idle,
    Running,
    Attacking
}


[System.Serializable]
public class StateChangeEvent : UnityEvent<BrainState> {}
