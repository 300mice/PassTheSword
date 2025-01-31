using UnityEngine;

public class SwordMatSwitch : MonoBehaviour
{
    public bool swinging = false;

    public SpriteRenderer sprite;
    private AIBrain brain;

    public Material idle;
    public Material slash;

    private Sword sword;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
        brain = GetComponent<AIBrain>();
        if (brain != null)
            brain.OnStateChange.AddListener(MatChange);
        sword = GetComponent<Sword>();
        sword.wielderChange.AddListener(UpdateBrain);*/
    }

    // Update is called once per frame
    void Update()
    {
        if(swinging)
            sprite.material = slash;
        else
            sprite.material = idle;
    }
    /*
    void MatChange(BrainState state)
    {
        if(state == BrainState.Idle)
            sprite.material = idle;
        else if (state == BrainState.Attacking)
            sprite.material = slash;
    }

    void UpdateBrain(AIBrain b)
    {
        if (brain != null)
            brain.OnStateChange.RemoveListener(MatChange);
        brain = b;
        if (brain != null)
        {
            brain.OnStateChange.AddListener(MatChange);
            MatChange(b.CurrentState);
        }

        else Debug.Log("null");
    }*/
}
