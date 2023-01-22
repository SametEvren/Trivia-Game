using System;
using System.Collections;
using System.Collections.Generic;
using Leaderboard.LeaderboardEntry;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Utility;

public class LeaderboardAPI : Instancable<LeaderboardAPI>
{
    
    public static LeaderboardData leaderboardData;
    
    private const string UriBeginning = "https://magegamessite.web.app/case1/leaderboard_page_";
    private const string UriEnding = ".json";

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


