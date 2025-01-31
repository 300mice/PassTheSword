using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text EnemiesRemainingText;
    [SerializeField] private TMP_Text PartyRemainingText;
    public GameObject retry;
    public TextMeshProUGUI scoreText;
    private bool bStopped;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.onGameOver.AddListener(StopGame);
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
        Time.timeScale = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemiesRemainingText.text = "Enemies Remaining: " + GameManager.Instance.EnemiesRemaining.ToString();
        PartyRemainingText.text = "Party Members Remaining: " + GameManager.Instance.PartyRemaining.ToString();
    }
}
