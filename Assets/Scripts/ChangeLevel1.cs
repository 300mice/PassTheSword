using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel1 : MonoBehaviour
{
    public string LevelName;

    // Update is called once per frame
    public void ChangeLevel()
    {
     Invoke(nameof(Change), 0.5f);   
    }

    void Change()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(LevelName);
    }
}
