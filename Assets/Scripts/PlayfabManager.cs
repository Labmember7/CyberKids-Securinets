using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{
    public Avatar avatar;
    public bool responseReceived = false;
    public GameObject avatarObject;
    public GameObject loadingPanel;
    public GameObject errorPanel;
    public GameObject username;

    // Start is called before the first frame update
    void Start()
    {
        Login();

        //Reference the avatar data
        avatar = avatarObject.GetComponent<AvatarManager>().avatar;
    }
    public void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnFailure);
        StartCoroutine(WaitForResponse());


    }
    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful Login "+ result.PlayFabId);
        GetAvatar();
        //responseReceived = true;

    }
    void OnFailure(PlayFabError error)
    {
        Debug.Log("Error while loggin in Check network");
        Debug.Log(error.GenerateErrorReport());
        responseReceived = true;
        //Show error on screen => activation error panel
        errorPanel.SetActive(true);

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
    public void GetAvatar()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnFailure);
        StartCoroutine(WaitForResponse());

    }

    void OnDataReceived(GetUserDataResult result)
    {
        Debug.Log("Reveived User Data");
        if(result.Data != null && result.Data["username"].Value!=null)
        {
            avatar.bgColor = result.Data["bgColor"].Value;
            avatar.hair = result.Data["hair"].Value;
            avatar.face = result.Data["face"].Value;
            avatar.body = result.Data["body"].Value;
            avatar.kit = result.Data["kit"].Value;
            avatarObject.GetComponent<AvatarManager>().avatar = avatar ;
            avatarObject.GetComponent<AvatarManager>().GetAvatar() ;
            username.GetComponent<InputField>().text = result.Data["username"].Value;
            /*Debug.LogWarning(avatarObject.GetComponent<AvatarManager>().avatar.bgColor);
            Debug.LogWarning(avatar.hair);
            Debug.LogWarning(avatar.face);
            Debug.LogWarning(avatar.body);
            Debug.LogWarning(avatar.kit);*/
        }
        else
        {
            SetAvatar();
        }
        responseReceived = true;
    }


   public void SetAvatar()
    {
        avatar.username = username.GetComponent<InputField>().text;
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"face" , avatar.face },
                { "hair",avatar.hair },
                { "body",avatar.body },
                { "kit",avatar.kit },
                { "bgColor",avatar.bgColor },
                { "username",avatar.username }

            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSent, OnFailure);
        StartCoroutine(WaitForResponse());

    }
    IEnumerator WaitForResponse()
    {
        loadingPanel.SetActive(true);
        
        while (!responseReceived)
        {
            //Debug.Log(Time.time);
            yield return null;
        }
        responseReceived = false;
        loadingPanel.SetActive(false);

    }
    void OnDataSent(UpdateUserDataResult result)
    {
        Debug.Log("Successful User Data Sent!");
        responseReceived = true;
    } 
}
public class Avatar
{
    public string face="0";
    public string hair="0";
    public string body="0";
    public string kit="0";
    public string bgColor="0";
    public string username="Username...";
    
}
