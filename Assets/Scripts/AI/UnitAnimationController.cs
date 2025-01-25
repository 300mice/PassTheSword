using System.Linq;
using UnityEngine;

public class UnitAnimationController : MonoBehaviour
{

    public string[] names;
    public AnimationClip[] animations;
    private Animator animator;
    public SpriteRenderer sprite;
    private AIBrain brain;
    private Sword sword;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = sprite.gameObject.GetComponent<Animator>();
        brain = GetComponent<AIBrain>();
        if(brain != null )
            brain.OnStateChange.AddListener(AnimationChange);
        sword = GetComponent<Sword>();

        if( sword != null)
        {
            sword.wielderChange.AddListener(UpdateBrain);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayAnimation(string name, float frame)
    {
        AnimationClip anim = GetAnimation(name);

        if (anim != null && animator.GetCurrentAnimatorClipInfo(0)[0].clip != anim)
        {
            animator.Play(anim.name, 0, frame);
            //t_currentAnimTime = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        }

    }

    private AnimationClip GetAnimation(string name)
    {
        for(int i = 0; i< animations.Length; i++)
        {
            if (names[i] == name)
                return animations[i];
        }

        return null;
    }


    void AnimationChange(BrainState state)
    {

        switch (state)
        {
            case BrainState.Idle:
                if (names.Contains("idle"))
                    PlayAnimation("idle", 0);
                break;
            case BrainState.Running:
                if (names.Contains("run"))
                    PlayAnimation("run", 0);
                break;
            case BrainState.Attacking:
                if (names.Contains("attack"))
                    PlayAnimation("attack", 0);
                break;
        }
    }


    // if needed, update the brain we're referencing. Used by the sword
    void UpdateBrain(AIBrain b)
    {
        brain = b;
        if (brain != null)
        {
            Debug.Log(b.transform.name);
            AnimationChange(b.CurrentState);
        }
            
        else Debug.Log("null");
    }




}
