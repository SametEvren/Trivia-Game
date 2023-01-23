using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class SceneController : MonoBehaviour
    {
        #region Private Properties
        private const string GameSceneName = "Game";
        #endregion
    
        public void UploadGameScene()
        {
            SceneManager.LoadScene(GameSceneName);
        }
    }
}
