using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{

    public bool countdownOnStart = true;
    public float time = 1;
    private float t_time = 0;
    private bool countdown = true;


    // Start is called before the first frame update
    void Start()
    {
        countdown = countdownOnStart;
        t_time = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown)
        {
            t_time -= Time.deltaTime;
            if(t_time <= 0)
                Destroy(this.gameObject);
        }
    }


}
