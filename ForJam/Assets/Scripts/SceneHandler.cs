using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    private List<AsyncOperation> _scenesToLoad = new List<AsyncOperation>();
    
    public void LoadMainScene()
    {
        _scenesToLoad.Add(SceneManager.LoadSceneAsync(1));
        _scenesToLoad.Add(SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
