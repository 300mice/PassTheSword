using UnityEngine;

public class CorpseScript : MonoBehaviour
{

    public float despawnTime = 4;
    public AnimationCurve curve;

    private float timer = 0;
    private Vector3 initialPos;
    private int anim = 0;
    private UnitAnimationController animator;


    void Start()
    {
        initialPos = transform.position;
        timer = 0;
        anim = Random.Range(1, 3);
        animator = GetComponent<UnitAnimationController>();
        if (animator != null)
            animator.PlayAnimation(anim.ToString(), 0);
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        transform.position = initialPos - new Vector3(0, curve.Evaluate(timer / despawnTime), 0);

        if(timer > despawnTime)
        {
            Destroy(this.gameObject);
        }
    }

    public void Setup(int facing)
    {
        Debug.Log(facing);
        if(facing == -1)
        {
            GetComponent<FlipXManager>().FlipX(-facing);
        }
    }


}
