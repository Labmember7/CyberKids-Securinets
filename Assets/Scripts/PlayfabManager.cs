using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Login();
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnFailure);

    }
    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful Login "+ result.PlayFabId);
    }
    void OnFailure(PlayFabError error)
    {
        Debug.Log("Error while loggin in Check network");
        Debug.Log(error.GenerateErrorReport());

    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Score",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnFailure);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successful : Leaderboard Updated");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest{
            StatisticName = "Score",
            StartPosition =0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboard, OnFailure);

    }

    void OnGetLeaderboard(GetLeaderboardResult result)
    {
        Debug.Log("Successful : Leaderboard received");
        foreach(var item in result.Leaderboard)
        {
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);

        }

    }
}
