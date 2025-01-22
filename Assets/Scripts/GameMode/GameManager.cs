using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public int EnemiesRemaining = 0;
    public int PartyRemaining = 0;

    public AIBrain[] EnemyTypes;
    public AIBrain PartyType;

    public List<AIBrain> CurrentBrains { get; private set; } = new List<AIBrain>();
    
    public Sword Sword { get; private set; }

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
        Sword = GameObject.Find("Sword").GetComponent<Sword>();
        SpawnParty();
        SpawnWave();
    }

    void SpawnEnemy(AIBrain Enemy)
    {
        AIBrain NewEnemy = Instantiate(Enemy, new Vector3(Random.Range(-15f, 15f), 0f, Random.Range(-15f, 15f)), Quaternion.identity);
        NewEnemy.GetComponent<HealthComponent>().HasDied.AddListener(EnemyDied);
        CurrentBrains.Add(NewEnemy);
        EnemiesRemaining++;
    }

    void EnemyDied(HealthComponent HealthComponent)
    {
        AIBrain deadAI = HealthComponent.GetComponent<AIBrain>();
        RemoveDeadBrain(deadAI);
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

    void SpawnParty()
    {
        for (int i = 0; i < 4; i++)
        {
            AIBrain NewPartyMember = Instantiate(PartyType, new Vector3(Random.Range(-15f, 15f), 0f, Random.Range(-15f, 15f)), Quaternion.identity);
            NewPartyMember.GetComponent<HealthComponent>().HasDied.AddListener(PartyDied);
            CurrentBrains.Add(NewPartyMember);
            PartyRemaining++; 
        }
    }

    void PartyDied(HealthComponent HealthComponent)
    {
        AIBrain deadAI = HealthComponent.GetComponent<AIBrain>();
        RemoveDeadBrain(deadAI);
        PartyRemaining--;
    }

    void RemoveDeadBrain(AIBrain deadAI)
    {
        if (deadAI && CurrentBrains.Contains(deadAI))
        {
            CurrentBrains.Remove(deadAI);
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
