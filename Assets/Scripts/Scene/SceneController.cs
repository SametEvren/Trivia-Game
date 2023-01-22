using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private const string GameSceneName = "Game";
    
    public void UploadGameScene()
    {
        SceneManager.LoadScene(GameSceneName);
    }
}
