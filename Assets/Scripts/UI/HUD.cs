using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using System.Linq;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text PartyRemainingText;
    public GameObject retry;
    public TextMeshProUGUI scoreText;
    private bool bStopped;
    [SerializeField] private Image XPBar;
    
    [SerializeField] private GameObject Upgrades;
    
    [SerializeField] private List<Upgrade> AvailableUpgrades = new List<Upgrade>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.onGameOver.AddListener(StopGame);
        GameManager.Instance.onAddScore.AddListener(AddXP);
        GameManager.Instance.onLevelUp.AddListener(OpenLevelUpOptions);
        AddXP(0);
    }

    void StopGame()
    {
        if (bStopped)
        {
            return;
        }
        bStopped = true;
        float score = GameManager.Instance.score;
        retry.SetActive(true);
        scoreText.text = score.ToString();
        Time.timeScale = 0.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        PartyRemainingText.text = "Party Members Remaining: " + GameManager.Instance.PartyRemaining.ToString();
    }

    void AddXP(int xp)
    {
        XPBar.fillAmount = (float)xp / GameManager.Instance.requiredXP;
    }

    void OpenLevelUpOptions()
    {
        Time.timeScale = 0f;
        Upgrades.SetActive(true);
        ShuffleUpgrades();
        foreach (Transform child in Upgrades.transform)
        {
            Destroy(child.gameObject);  
        }
        AddUpgrade(0);
        AddUpgrade(1);
        AddUpgrade(2);
    }

    void AddUpgrade(int index)
    {
        Upgrade newUpgrade = Instantiate(AvailableUpgrades[index], Upgrades.transform.position, Quaternion.identity, Upgrades.transform);
        newUpgrade.OnUpgrade.AddListener(FinishUpgrade);
    }

    void FinishUpgrade()
    {
        Upgrades.SetActive(false);
        Time.timeScale = 1.0f;
    }
    
    public void ShuffleUpgrades()
    {
        System.Random rng = new System.Random();
        int n = AvailableUpgrades.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Upgrade temp = AvailableUpgrades[k];
            AvailableUpgrades[k] = AvailableUpgrades[n];
            AvailableUpgrades[n] = temp;
        }
    }
    
    
}
