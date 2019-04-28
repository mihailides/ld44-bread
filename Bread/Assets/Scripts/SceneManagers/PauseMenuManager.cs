using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ContinueGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
