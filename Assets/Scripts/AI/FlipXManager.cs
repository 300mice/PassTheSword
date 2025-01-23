using UnityEngine;
using UnityEngine.UIElements;

public class FlipXManager : MonoBehaviour
{

    private AIBrain brain;
    public SpriteRenderer sprite;
    private Material material;
    public GameObject swordPos;


    // used to track facing for sprite and sword positions
    private int currentFacing = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        brain = GetComponent<AIBrain>();
        material = sprite.material;
        material.SetInt("_IsFacingRight?", currentFacing);
    }

    // Update is called once per frame
    void Update()
    {
        // check facing
        if (Mathf.Sign(brain.ReturnTarget().transform.position.x - transform.position.x) != currentFacing)
        {
            currentFacing = (int)Mathf.Sign(brain.ReturnTarget().transform.position.x - transform.position.x);
            FlipX();
        }

    }

    // this is to flip the sprite and sword positions on the x axis
    void FlipX()
    {

        swordPos.transform.localScale = new Vector3(-currentFacing, 1, 1);

        material.SetInt("_IsFacingRight?", currentFacing);
    }
}
