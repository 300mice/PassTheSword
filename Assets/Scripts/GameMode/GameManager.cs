using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    public TextMeshProUGUI scoreText;
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
        StartCoroutine(SpawnWave());
    }

    void SpawnEnemy(AIBrain Enemy)
    {
        AIBrain NewEnemy = Instantiate(Enemy, new Vector3(Random.Range(-15f, 15f), 0f, Random.Range(-15f, 15f)),
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
    }

    IEnumerator SpawnWave()
    {
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                SpawnEnemy(EnemyTypes[Random.Range(0, EnemyTypes.Length)]);
                yield return new WaitForSeconds(0.5f);

            }

            yield return new WaitForSeconds(5.0f);
        }

    }

    void SpawnParty()
    {
        for (int i = 0; i < 4; i++)
        {
            AIBrain NewPartyMember = Instantiate(PartyType,
                new Vector3(Random.Range(-15f, 15f), 0f, Random.Range(-15f, 15f)), Quaternion.identity);
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
        if(scoreText  != null)
        {
            scoreText.text = score.ToString();
        }
    }

}
