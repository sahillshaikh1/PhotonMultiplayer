using EasyUI.Toast;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class InternetConnectionCheck : MonoBehaviourPunCallbacks
{
    bool isOnline, flag;
    public GameObject OffilinePanel;
    void Start()
    {
        flag = true;
        OffilinePanel.SetActive(false);
    }

    void Update()
    {
        CheckInternetConnection();
        if (isOnline && flag)
        {
            Toast.Show("ONLINE", 2, Color.green);
            OffilinePanel.SetActive(false);
            flag = false;
        }
        if (!isOnline && !flag)
        {            
            OffilinePanel.SetActive(true);
            if (SceneManager.GetActiveScene().name == "TicTacToe")
            {
               // ChangeRoom
            }
            flag = true;
        }
    }

    public void CheckInternetConnection()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            // text1.text = "Unity comes with its own judgment, no internet connection";
            Debug.Log("Unity comes with judgment, no networking");
            isOnline = false;
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            Debug.Log("Unity comes with, connected mobile network");
            isOnline = true;
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            Debug.Log("Unity comes with, connected wifi");
            isOnline = true;
        }
        else
        {
            // text1.text = "Unity comes with its own judgment and is connected to the Internet";
            Debug.Log("Unity comes with judgment and is connected to the Internet");
            isOnline = true;
        }
    }
}
