using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text EnemiesRemainingText;
    [SerializeField] private TMP_Text PartyRemainingText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemiesRemainingText.text = "Enemies Remaining: " + GameManager.Instance.EnemiesRemaining.ToString();
        PartyRemainingText.text = "Party Members Remaining: " + GameManager.Instance.PartyRemaining.ToString();
    }
}
