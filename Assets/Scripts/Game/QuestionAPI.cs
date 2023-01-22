using Newtonsoft.Json;
using UnityEngine;

namespace Game
{
    public class QuestionAPI : MonoBehaviour
    {
        public static QuestionData questionData;

        private void Awake()
        {
            ReadDataFromJSON();
        }

        public static void ReadDataFromJSON()
        {
            TextAsset textJSON = (TextAsset)Resources.Load("questions", typeof(TextAsset));
            string jsonString = textJSON.text.Replace("\n","");
            questionData = JsonConvert.DeserializeObject<QuestionData>(jsonString);
        }
    }
    
}