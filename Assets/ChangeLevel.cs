using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    public string levelName;
    

    public void OpenLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelName);
    }
}
