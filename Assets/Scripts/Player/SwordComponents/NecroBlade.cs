using UnityEngine;

public class NecroBlade : MonoBehaviour
{
    Sword sword;
    public int target = 0;
    void Start()
    {
        sword = GetComponent<Sword>();
        sword.DamageComponent.OnHit.AddListener(OnHit);
    }

    void OnHit()
    {
        if (Random.Range(0, 100) < target)
        {
            GameManager.Instance.SpawnZombie();
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
