using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private string gameSceneName = "Game";
    
    public void UploadGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
