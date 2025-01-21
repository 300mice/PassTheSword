using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public int EnemiesRemaining = 0;

    public AIBrain[] EnemyTypes;

    public static GameManager Instance
    {
        get
        {
            if (instance is null)
                Debug.LogError("GameManager is null!");
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        SpawnWave();
    }

    void SpawnEnemy(AIBrain Enemy)
    {
        AIBrain NewEnemy = Instantiate(Enemy, new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f)), Quaternion.identity);
        NewEnemy.GetComponent<HealthComponent>().HasDied.AddListener(EnemyDied);
        EnemiesRemaining++;
    }

    void EnemyDied()
    {
        EnemiesRemaining--;
        if (EnemiesRemaining <= 0)
        {
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnEnemy(EnemyTypes[Random.Range(0, EnemyTypes.Length - 1)]);
        }
    }
// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
