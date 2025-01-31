using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public int score { get; private set; }= 0;
    public TextMeshProUGUI scoreText;
    private static GameManager instance;
    public int EnemiesRemaining = 0;
    public int PartyRemaining = 0;

    public AIBrain[] EnemyTypes;
    public AIBrain PartyType;
    public UnityEvent onEnemyDeath;
    public UnityEvent onPartyDeath;
    public UnityEvent onGameOver;
    public AddScoreEvent onAddScore;
    public UnityEvent onLevelUp;
    public List<AIBrain> CurrentBrains { get; private set; } = new List<AIBrain>();
    public int requiredXP = 750;
    public int level = 0;
    private int xp = 0;
    
    
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
        StartCoroutine(SpawnWave(8, 8, 0, 0.25f));
    }

    void SpawnEnemy(AIBrain Enemy)
    {
        AIBrain NewEnemy = Instantiate(Enemy, new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f)),
            Quaternion.identity);
        NewEnemy.GetComponent<HealthComponent>().HasDied.AddListener(EnemyDied);
        CurrentBrains.Add(NewEnemy);
        EnemiesRemaining++;
    }

    void EnemyDied(HealthComponent HealthComponent)
    {
        AIBrain deadAI = HealthComponent.GetComponent<AIBrain>();
        RemoveDeadBrain(deadAI);
        EnemiesRemaining--;
        onEnemyDeath.Invoke();
    }

    IEnumerator SpawnWave(int numEnemies, float delay, int enemyIndex, float spawnDelay)
    {
        int wave = 0;
        while (true)
        {
            for (int i = 0; i < numEnemies + Mathf.Clamp(wave, 0, 6); i++)
            {
                SpawnEnemy(EnemyTypes[Mathf.Clamp(enemyIndex, 0, EnemyTypes.Length - 1)]);
                yield return new WaitForSeconds(spawnDelay);

            }

            if (wave == 4)
            {
                StartCoroutine(SpawnWave(1, Mathf.Clamp(delay + 7, 2, 20), enemyIndex + 1, 1.0f));
            }
            
            yield return new WaitForSeconds(delay);
            delay = Mathf.Clamp(delay - 0.20f, 4, 20);
            wave++;
        }

    }
    
    

    public void SpawnParty()
    {
        for (int i = 0; i < 4; i++)
        {
            SpawnPartyMember();
        }
    }

    public void SpawnPartyMember()
    {
        AIBrain NewPartyMember = Instantiate(PartyType,
            new Vector3(Random.Range(-15f, 15f), 0f, Random.Range(-15f, 15f)), Quaternion.identity);
        NewPartyMember.GetComponent<HealthComponent>().HasDied.AddListener(PartyDied);
        CurrentBrains.Add(NewPartyMember);
        PartyRemaining++;
    }

    void PartyDied(HealthComponent HealthComponent)
    {
        AIBrain deadAI = HealthComponent.GetComponent<AIBrain>();
        RemoveDeadBrain(deadAI);
        PartyRemaining--;
        onPartyDeath.Invoke();
        if (PartyRemaining <= 0)
        {
            onGameOver.Invoke();
        }
    }

    void RemoveDeadBrain(AIBrain deadAI)
    {
        if (deadAI && CurrentBrains.Contains(deadAI))
        {
            CurrentBrains.Remove(deadAI);
        }
    }

    public AIBrain GetClosestBrainWithTag(Transform inTransform, string tag)
    {
        AIBrain closestBrain = null;
        foreach (AIBrain PartyMember in GameManager.Instance.CurrentBrains)
        {
            if (!PartyMember.CompareTag(tag))
            {
                continue;
            }

            if (!PartyMember.bAlive)
            {
                continue;
            }

            if (!closestBrain || Vector3.Distance(closestBrain.transform.position, inTransform.position) >
                Vector3.Distance(PartyMember.transform.position, inTransform.position))
            {
                closestBrain = PartyMember;
            }


        }
        return closestBrain;
    }

    public void AddScore(int s)
    {
        score += s;
        xp += s;
        if(scoreText  != null)
        {
            scoreText.text = score.ToString();
        }
        LevelUp();
        onAddScore.Invoke(xp);
    }

    public bool LevelUp()
    {
        if (xp <= requiredXP)
        {
            return false;
        }
        onLevelUp.Invoke();
        requiredXP += 250;
        level++;
        xp = 0;
        return true;
    }

}

[System.Serializable]
public class AddScoreEvent : UnityEvent<int> {}
