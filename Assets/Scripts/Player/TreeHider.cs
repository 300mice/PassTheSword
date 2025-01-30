using UnityEngine;

public class TreeHider : MonoBehaviour
{
    public SpriteRenderer sprite;
    private Material mat;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mat = sprite.material;
        mat.SetInt("_Outline", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Tree")
        {
            other.gameObject.SendMessage("Fade", true);
            mat.SetInt("_Outline", 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Tree")
        {
            other.gameObject.SendMessage("Fade", false);
            mat.SetInt("_Outline", 0);
        }
    }
}
