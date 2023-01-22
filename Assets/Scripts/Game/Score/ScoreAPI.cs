using Game.Questions;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Score
{
    public class ScoreAPI : MonoBehaviour
    {
        public static ScoreConfig scoreConfig;

        private void Awake()
        {
            ReadDataFromJSON();
        }

        public static void ReadDataFromJSON()
        {
            TextAsset textJSON = (TextAsset)Resources.Load("scoreConfig", typeof(TextAsset));
            string jsonString = textJSON.text.Replace("\n","");
            scoreConfig = JsonConvert.DeserializeObject<ScoreConfig>(jsonString);
        }
    }
    
}