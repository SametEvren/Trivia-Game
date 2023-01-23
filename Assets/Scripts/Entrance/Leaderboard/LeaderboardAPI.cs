using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Utility;

namespace Entrance.Leaderboard
{
    public class LeaderboardAPI : Instancable<LeaderboardAPI>
    {
        #region Public Properties
        public static LeaderboardData leaderboardData;
        #endregion
        
        #region Private Properties
        private const string UriBeginning = "https://magegamessite.web.app/case1/leaderboard_page_";
        private const string UriEnding = ".json";
        #endregion

        public static APIStatus Status
        {
            get
            {
                if (leaderboardData == null)
                    return APIStatus.Empty;
                if (leaderboardData.data == null)
                    return APIStatus.Error;

                return APIStatus.Ready;
            }
        }
        public void UpdateLeaderboardData()
        {
            leaderboardData = null;
            StartCoroutine(GetRequest());
        }
    
        private IEnumerator GetRequest()
        {
            LeaderboardData tempData = null;
            LeaderboardData receivedData = null;
        
            int pageIndex = 0;
        
            do
            {
                var uri = UriBeginning + pageIndex + UriEnding;
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
                        tempData = JsonConvert.DeserializeObject<LeaderboardData>(webRequest.downloadHandler.text);
                        pageIndex++;
                        if (receivedData == null)
                        {
                            receivedData = tempData;
                        }
                        else
                        {
                            receivedData.data.AddRange(tempData.data);
                        }
                        break;
                    default:
                        PlaceEmptyData();
                        throw new ArgumentOutOfRangeException();
                }
            } while (tempData is not { is_last: true });

            leaderboardData = receivedData;
        }
    
        private void PlaceEmptyData()
        {
            leaderboardData = new LeaderboardData
            {
                data = null
            };
        }
    }
}


