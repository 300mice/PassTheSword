using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class FlipXManager : MonoBehaviour
{

    private AIBrain brain;
    public SpriteRenderer sprite;
    private Material material;
    public GameObject swordPos;


    // used to track facing for sprite and sword positions
    private int currentFacing = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        brain = GetComponent<AIBrain>();
        material = sprite.material;
        /*material.EnableKeyword("_NORMALMAP");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        material.EnableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_EMISSION");
        material.EnableKeyword("_PARALLAXMAP");
        material.EnableKeyword("_DETAIL_MULX2");
        material.EnableKeyword("_METALLICGLOSSMAP");
        material.EnableKeyword("_SPECGLOSSMAP");*/
        
        
        
        
        sprite.material.SetFloat("_IsFacingLeft", currentFacing);
        //int asd123 = material.GetInteger("_IsFacingLeft");
        
    }

    // Update is called once per frame
    void Update()
    {
        // check facing
        if (!brain.CurrentAction.Target)
        {
            return;
        }
        
        if (Mathf.Sign(brain.CurrentAction.Target.transform.position.x - transform.position.x) != currentFacing)
        {
            currentFacing = (int)Mathf.Sign(brain.CurrentAction.Target.transform.position.x - transform.position.x);
            FlipX();
        }

    }

    // this is to flip the sprite and sword positions on the x axis
    void FlipX()
    {

        //swordPos.transform.localScale = new Vector3(-currentFacing, 1, 1);
        sprite.material.SetFloat("_IsFacingLeft", currentFacing > 0? 0 : 1);
        sprite.flipX = currentFacing < 0;
    }
}
