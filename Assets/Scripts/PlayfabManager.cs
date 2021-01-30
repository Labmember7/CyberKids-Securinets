using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{
    public Avatar avatar =new Avatar();
    public bool responseReceived = false;
    public GameObject avatarObject;
    public GameObject loadingPanel;
    public GameObject errorPanel;

    [Header("Login UI Components")]
    public Button registerButton;
    public Button loginButton;
    public Text message;
    public InputField username;
    public InputField email;
    public InputField password;
    public Text score;

    [Header("UI Canvas")]
    public GameObject LoginUI;
    public GameObject ProfileUI;
    public GameObject backgroundImage;
    public GameObject homeScreen;
    // Start is called before the first frame update

    void Start()
    {
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
        Debug.Log("User Credentials Saved");
        PlayerPrefs.SetString("email", email.text);
        PlayerPrefs.SetString("password", password.text);
    }

        #region Authentification
        public void Register()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Username = username.text,
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
        username.gameObject.SetActive(false);
        loginButton.gameObject.SetActive(true);
        registerButton.gameObject.SetActive(false);

        responseReceived = true;
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
        if(result.Data != null && result.Data.ContainsKey("bgColor"))
        {
            avatar.bgColor = result.Data["bgColor"].Value;
            avatar.hair = result.Data["hair"].Value;
            avatar.face = result.Data["face"].Value;
            avatar.body = result.Data["body"].Value;
            avatar.kit = result.Data["kit"].Value;
            avatarObject.GetComponent<AvatarManager>().SetAvatar(avatar) ;
            homeScreen.SetActive(true);
            responseReceived = true;

        }
        else
        {
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
        Debug.Log("Successful User Data Sent!");
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
    public void SetPlayerInfo()
    {
        //
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
            Debug.Log("Complete " + result.ToString());
            try
            {
                if (result.Statistics[0] != null)
                    Debug.Log("Score = " + result.Statistics[0].Value);
                score.text = result.Statistics[0].Value + "";
            }
            catch(PlayFabException e)
            {
                Debug.Log(e);
            }
           
        },
        error => Debug.Log(error.GenerateErrorReport()));

    }
    
    public void success(GetAccountInfoResult result)
    {
        Debug.Log(result.AccountInfo.Username);
        username.text = result.AccountInfo.Username;

    }


}
public class Avatar
{
    public string face="0";
    public string hair="0";
    public string body="0";
    public string kit="0";
    public string bgColor = "0";    
}
