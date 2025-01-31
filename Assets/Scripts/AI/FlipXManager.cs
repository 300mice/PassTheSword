using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class FlipXManager : MonoBehaviour
{

    private AIBrain brain;
    public SpriteRenderer sprite;
    private Material material;


    // used to track facing for sprite and sword positions
    public int currentFacing = 0;

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
        if(brain != null)
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
        

    }

    // this is to flip the sprite and sword positions on the x axis
    public void FlipX()
    {

        sprite.material.SetFloat("_IsFacingLeft", currentFacing > 0? 0 : 1);
        //sprite.flipX = currentFacing < 0;
    }

    public void FlipX(int facing)
    {

        sprite.material.SetFloat("_IsFacingLeft", facing > 0 ? 0 : 1);
        //sprite.flipX = facing < 0;
    }
}
