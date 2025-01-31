using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text PartyRemainingText;
    public GameObject retry;
    public TextMeshProUGUI scoreText;
    private bool bStopped;
    [SerializeField] private Image XPBar;
    
    [SerializeField] private GameObject Upgrades;
    
    [SerializeField] private Upgrade[] AvailableUpgrades;
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
}
