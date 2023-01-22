using System;
using System.Collections;
using System.Collections.Generic;
using Leaderboard.LeaderboardEntry;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Utility;

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