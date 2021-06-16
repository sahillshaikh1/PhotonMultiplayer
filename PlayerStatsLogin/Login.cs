using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;

public class Login : MonoBehaviour
{
    public static Login Instance { get; set; }
    private string userEmail, userPassword;
    private string username;
    public GameObject LoginPanel;
    private void Update()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void Start()
    {
        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "6ED58"; // Please change this value to your own titleId from PlayFab Game Manager
        }
        if (PlayerPrefs.HasKey("EMAIL"))
        {
            userEmail = PlayerPrefs.GetString("EMAIL");
            userPassword = PlayerPrefs.GetString("PASSWORD");
            var requets = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
            PlayFabClientAPI.LoginWithEmailAddress(requets, OnLoginSuccess, OnLoginFailure);
        }
//        #region AnonymousLogin
//        else
//        {

//#if UNITY_ANDROID
//            var requestAndroid = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = ReturnMobileID(), CreateAccount = true };
//            PlayFabClientAPI.LoginWithAndroidDeviceID(requestAndroid, OnLoginAndroidSuccess, OnLoginAndroidFailure);

//#endif
//        }


    }
    //public static string ReturnMobileID()
    //{
    //    string deviceID = SystemInfo.deviceUniqueIdentifier;
    //    return deviceID;
    //}
    //private void OnLoginAndroidSuccess(LoginResult result)
    //{
    //    Debug.Log("Congratulations, you made your first successful API call!");
    //    LoginPanel.SetActive(false);
    //    //RememberMe(); //torememberme
    //}
    //private void OnLoginAndroidFailure(PlayFabError error)
    //{
    //    Debug.Log(error.GenerateErrorReport());
    //}
    //#endregion
    #region LogonEmailAnaPassword
    public void GetUserEmail(string emailIn)
    {
        userEmail = emailIn;
    }
    public void GetUserPassword(string PasswordIn)
    {
        userPassword = PasswordIn;
    }
    public void GetUserName( string usernameIn)
    {
        username = usernameIn;
    }
    public void OnClickLogin()
    {
        var requets = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
        PlayFabClientAPI.LoginWithEmailAddress(requets, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        GetStats();
        Debug.Log("Congratulations, you made your first successful API call!");
        LoginPanel.SetActive(false);
        //RememberMe(); //torememberme
    }
    private void OnLoginFailure(PlayFabError error)
    {
        var RegisterRequest = new RegisterPlayFabUserRequest { Email = userEmail , Password = userPassword,Username = username};
        PlayFabClientAPI.RegisterPlayFabUser(RegisterRequest,OnRigisterSuccess,OnRegisterFailure);
    }
    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
    private void OnRigisterSuccess(RegisterPlayFabUserResult result)
    {
        GetStats();
        LoginPanel.SetActive(false);
        Debug.Log("Congratulations, Register successful ");
        //RememberMe(); //torememberme
    }
    public void RememberMe()
    {
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
    }

    #endregion

    public int Level;
    public int coin;
    public int PlayerHighScore;
    #region Statistic
    //Post and Pull Data
    public void SetStat()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
            //To Update Statistic goto settings Titlessetting > APIFeatures > AllowClientPostPlayerStatistic
            Statistics = new List<StatisticUpdate> {
        new StatisticUpdate { StatisticName = "Levell", Value = Level },
         new StatisticUpdate { StatisticName = "Money", Value = coin },
          new StatisticUpdate { StatisticName = "HighScore", Value = PlayerHighScore }

            }
        },
            result => { Debug.Log("User statistics updated"); },
            error => { Debug.LogError(error.GenerateErrorReport()); });
    }

    void GetStats()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStats,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    void OnGetStats(GetPlayerStatisticsResult result)
    {
        Debug.Log("Received the following Statistics:");
        foreach (var eachStat in result.Statistics)
        {
            Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
            switch(eachStat.StatisticName)
            {
                case "Levell":
                    Level = eachStat.Value;
                    break;
                case "Money":
                    coin = eachStat.Value;
                    break;
                case "HighScore":
                    PlayerHighScore = eachStat.Value;
                    break;

            }
        }
           
    }



    #endregion

}

