using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{
    public Avatar avatar = new Avatar();
    public bool responseReceived = false;
    public GameObject avatarObject;
    public GameObject loadingPanel;
    public GameObject errorPanel;

    [Header("Login UI Components")]
    public Button registerButton;
    public Button loginButton;
    public Text message;
    public InputField username;
    public InputField usernameRegister;
    public InputField email;
    public InputField password;
    public Text score;

    [Header("UI Canvas")]
    public GameObject LoginUI;
    public GameObject ProfileUI;
    public GameObject backgroundImage;
    public GameObject homeScreen;

    [Header("LeaderBoard")]
    public GameObject playerRankPrefab;
    public Transform allRanksHolder;
    // Start is called before the first frame update

    void Start()
    {
        //Init PlayerRank text Color 
        ColorUtility.TryParseHtmlString("#9800FF", out color);

        if (PlayerPrefs.HasKey("email") && PlayerPrefs.HasKey("password"))
        {
            email.text = PlayerPrefs.GetString("email");
            password.text = PlayerPrefs.GetString("password");
            if (email.text != "" && password.text != "")
            {
                Login();
            }
        }
    }
    void RememberMe()
    {
        //Debug.Log("User Credentials Saved");
        PlayerPrefs.SetString("email", email.text);
        PlayerPrefs.SetString("password", password.text);
    }

    #region Authentification
    public void Register()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Username = usernameRegister.text,
            Email = email.text,
            Password = password.text

        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFail);
        //Loading Screen
        StartCoroutine(WaitForResponse());

    }
    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        message.text = "Register Successful";
        message.color = Color.green;
        message.gameObject.SetActive(true);
        usernameRegister.gameObject.SetActive(false);
        loginButton.gameObject.SetActive(true);
        registerButton.gameObject.SetActive(false);
        responseReceived = true;
        score.text = "0";
    }
    void OnRegisterFail(PlayFabError error)
    {
        message.text = error.ErrorMessage;
        message.gameObject.SetActive(true);
        responseReceived = true;
    }
    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email.text,
            Password = password.text,

        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFail);
        //Loading Screen
        StartCoroutine(WaitForResponse());


    }

    void OnLoginSuccess(LoginResult result)
    {
        //Transition to Profile :
        LoginUI.SetActive(false);
        backgroundImage.SetActive(false);
        ProfileUI.SetActive(true);
        GetPlayerInfo();
        //Remember me 
        RememberMe();
        //Recover Avatar if the user have created one
        GetAvatar();

    }
    void OnLoginFail(PlayFabError error)
    {
        message.text = error.ErrorMessage;
        message.gameObject.SetActive(true);
        responseReceived = true;

    }

    void OnFailure(PlayFabError error)
    {
        Debug.Log("Error while loggin in Check network");
        Debug.Log(error.GenerateErrorReport());
        responseReceived = true;
        //Show error on screen => activation error panel
        errorPanel.SetActive(true);

    }
    #endregion
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
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Score",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboard, OnFailure);

    }
    Color color = new Color();

    void OnGetLeaderboard(GetLeaderboardResult result)
    {

        foreach (Transform item in allRanksHolder)
        {
            Destroy(item.gameObject);
        }
        //Debug.Log("Successful : Leaderboard received");
        foreach (var item in result.Leaderboard)
        {
            GameObject newRankObj = Instantiate(playerRankPrefab, allRanksHolder);
            Text[] texts = newRankObj.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            if(item.Profile.DisplayName == username.text)
            {
                texts[0].color = color;
                texts[1].color = color;
                texts[2].color = color;
            }
            texts[1].text = item.Profile.DisplayName;
            texts[2].text = item.StatValue.ToString();

            //Debug.Log(item.Position + " " +" Profile : " + item.Profile.DisplayName + " " + item.StatValue);
        }

    }

    public void GetAvatar()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnFailure);
        StartCoroutine(WaitForResponse());

    }

    void OnDataReceived(GetUserDataResult result)
    {
        //Debug.Log("Reveived User Data");            
        if (result.Data != null && result.Data.ContainsKey("bgColor"))
        {
            avatar.bgColor = result.Data["bgColor"].Value;
            avatar.hair = result.Data["hair"].Value;
            avatar.face = result.Data["face"].Value;
            avatar.body = result.Data["body"].Value;
            avatar.kit = result.Data["kit"].Value;
            avatarObject.GetComponent<AvatarManager>().SetAvatar(avatar);
            homeScreen.SetActive(true);
            responseReceived = true;

        }
        else
        {
            //Initialise the avatar variables
            SetAvatar();
        }
    }


    public void SetAvatar()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"face" , avatar.face },
                { "hair",avatar.hair },
                { "body",avatar.body },
                { "kit",avatar.kit },
                { "bgColor",avatar.bgColor },
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSent, OnFailure);
        StartCoroutine(WaitForResponse());

    }

    void OnDataSent(UpdateUserDataResult result)
    {
        //Debug.Log("Successful User Data Sent!");
        GetAvatar();
    }
    IEnumerator WaitForResponse()
    {
        loadingPanel.SetActive(true);

        while (!responseReceived)
        {
            yield return null;
        }
        responseReceived = false;
        loadingPanel.SetActive(false);

    }

    public void Logout()
    {
        PlayFabDataAPI.ForgetAllCredentials();
    }
    public void SetDisplayName()
    {

        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = username.text
        }, result =>
        {
            //Debug.Log("The player's display name is now: " + result.DisplayName);
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }
    public void GetPlayerInfo()
    {
        var request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, success, OnFailure);
        PlayFabClientAPI.GetPlayerStatistics(new GetPlayerStatisticsRequest()
        {
            StatisticNames = new List<string> { "Score"// <- My Leaderboard name
}
        }, result => {
            /*Debug.Log("Complete " + result.ToString());
            Debug.Log("result.Statistics.Count " + result.Statistics.Count);*/
            try
            {
                if (result.Statistics.Count > 0)
                {
                    //Debug.Log("Score = " + result.Statistics[0].Value);
                    score.text = result.Statistics[0].Value + "";
                    PlayerPrefs.SetInt("score", result.Statistics[0].Value);
                }
                else
                {
                    SendLeaderboard(0);
                    PlayerPrefs.SetInt("score", 0);
                }
            }
            catch (PlayFabException e)
            {
                Debug.Log(e);
            }

        },
        error => Debug.Log(error.GenerateErrorReport()));

    }

    public void success(GetAccountInfoResult result)
    {
        if (result.AccountInfo.TitleInfo.DisplayName == null)
        {
            username.text = result.AccountInfo.Username;
            //Set The DisplayName to Username
            SetDisplayName();
        }
        else
        {
            username.text = result.AccountInfo.TitleInfo.DisplayName;
        }

        //Debug.Log(result.AccountInfo.TitleInfo.DisplayName + "  "+ this.gameObject.tag);
    }


}
public class Avatar
{
    public string face = "0";
    public string hair = "0";
    public string body = "0";
    public string kit = "0";
    public string bgColor = "0";
}
