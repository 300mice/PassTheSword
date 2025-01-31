using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel1 : MonoBehaviour
{
    public string LevelName;

    // Update is called once per frame
    public void ChangeLevel()
    {
        SceneManager.LoadScene(LevelName);
        Time.timeScale = 1;
    }
}
