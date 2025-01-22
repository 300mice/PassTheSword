using UnityEngine;

public class SwordMove : MonoBehaviour
{
    public float speed = 0.8f;
    public float minDist = 0.2f;
    public float maxDist = 5;
    public Transform currentTarget;
    public float rotSpeed = 1;

    public AnimationCurve speedDistCurve;


    
    // this script is for the sword to move between positions

    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        // move toward target

        if(currentTarget != null)
        {


            // translate position 
            Vector3 dir = (currentTarget.position - transform.position).normalized;
            float dist = Vector3.Distance(currentTarget.position, transform.position);
            float s = speed * speedDistCurve.Evaluate( Mathf.InverseLerp(minDist, maxDist, dist) );


            if (dist > minDist)
            {
                transform.Translate(dir * s * Time.deltaTime);
            }
            else
                transform.position = currentTarget.position;



            
        }

    }

}
