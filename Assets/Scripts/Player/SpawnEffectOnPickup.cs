using UnityEngine;

public class SpawnEffectOnPickup : MonoBehaviour
{

    private AIBrain brain;
    public GameObject effect;

    void Start()
    {
        brain = GetComponent<AIBrain>();
        if (brain != null)
            brain.onPickup.AddListener(OnPickup);
    }


    void Update()
    {
        
    }

    void OnPickup()
    {
        if(effect != null)
            Instantiate(effect, transform.position, Quaternion.identity);
    }
}
