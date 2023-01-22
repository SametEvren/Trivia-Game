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
        private const string uri = "https://magegamessite.web.app/case1/questions.json";
        public static QuestionData questionData;

        
        public static APIStatus Status
        {
            get
            {
                if (questionData == null)
                    return APIStatus.Empty;
                if (questionData.questions == null)
                    return APIStatus.Error;

                return APIStatus.Ready;
            }
        }
        
        private void Start()
        {
            StartCoroutine(GetRequest());
        }

        
        private IEnumerator GetRequest()
        {
            QuestionData tempData = null;
            QuestionData receivedData = null;
            
            do
            {
                UnityWebRequest webRequest = UnityWebRequest.Get(uri);
                yield return webRequest.SendWebRequest();
                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                        Debug.LogError("ConnectionError");
                        PlaceEmptyData();
                        yield break;
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError("DataProcessingError");
                        PlaceEmptyData();
                        yield break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError("ProtocolError");
                        PlaceEmptyData();
                        yield break;
                    case UnityWebRequest.Result.Success:
                        tempData = JsonConvert.DeserializeObject<QuestionData>(webRequest.downloadHandler.text);
                        if (receivedData == null)
                        {
                            receivedData = tempData;
                        }
                        else
                        {
                            receivedData.questions.AddRange(tempData.questions);
                        }
                        break;
                    default:
                        
                        throw new ArgumentOutOfRangeException();
                }
            } while (tempData == null);
    
            questionData = receivedData;
            QuestionController.Instance.questionData = receivedData;
        }
        private void PlaceEmptyData()
        {
            questionData = new QuestionData
            {
                questions = null
            };
        }
    }
}