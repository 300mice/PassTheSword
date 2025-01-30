using UnityEngine;

public class CheckOnScreen : MonoBehaviour
{
    public Renderer r;
    public GameObject guyPointer;

    private bool visLastFrame = false;
    private GameObject pointer;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pointer = Instantiate(guyPointer, transform.position, Quaternion.identity);
        pointer.GetComponent<GuyPointer>().target = GetComponent<AIBrain>();
    }

    // Update is called once per frame
    void Update()
    {


        if(visLastFrame != r.isVisible)
        {
            Debug.Log("change");
            pointer.SendMessage("Change", r.isVisible);
            visLastFrame = r.isVisible;
        }

    }


}
