using Unity.VisualScripting;
using UnityEngine;

public class SpawnEffectOnPickup : MonoBehaviour
{

    private AIBrain brain;
    public GameObject effect;

    public SpriteRenderer sprite;
    // this is going to manage the outline as well

    private Material mat;
    private bool wielder = false;
    void Start()
    {
        mat = sprite.material;
        brain = GetComponent<AIBrain>();
        if (brain != null)
            brain.onPickup.AddListener(OnPickup);
    }


    void Update()
    {
        if (wielder)
            mat.SetInt("_Outline",1);
        else
            mat.SetInt("_Outline", 0);
    }

    void OnPickup()
    {
        wielder = true;
        effect.SetActive(true);
        effect.GetComponentInChildren<UnitAnimationController>().PlayAnimation("spin", 0);
        
    }

    void OnDrop()
    {
        wielder = false;
        effect.SetActive(false);
    }
}
