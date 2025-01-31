using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel1 : MonoBehaviour
{
    public string LevelName;

    // Update is called once per frame
    public void ChangeLevel()
    {
        Time.timeScale = 1;
        Invoke(nameof(Change), 0.5f);   
    }

    void Change()
    {
        SceneManager.LoadScene(LevelName);
    }
}
