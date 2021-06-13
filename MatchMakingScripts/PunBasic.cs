using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.Collections;

public class PunBasic : MonoBehaviourPunCallbacks
{
    public Text Log;
    public GameObject BattleBtn;
    public GameObject ClickButton;
    public bool Playerisfull;
    public GameObject[] Counting;
   
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        
       
    }
   

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server ");
        PhotonNetwork.AutomaticallySyncScene = true;
      
    }
    public void BattleStartBtn()
    {
        PhotonNetwork.JoinRandomRoom();// this will be in battel button
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }
    private void Update()
    {
        Log.text ="Room Name: " + PhotonNetwork.CurrentRoom.Name.ToString() + " No of Player in room: " + PhotonNetwork.PlayerList.Length;

        Debug.Log(PhotonNetwork.PlayerList.Length + " Number of Player in room");
        if (PhotonNetwork.PlayerList.Length == 2)
        {
           ClickButton.SetActive(true);
            foreach (var item in Counting)
            {
                item.SetActive(true);
            }

        }
        else
        {
           
          ClickButton.SetActive(false);
            foreach (var item in Counting)
            {
                item.SetActive(false);
            }
        }
       
    }
    
    public void CreateRoom()
    {
        int RandomRoom = Random.Range(1, 5);
        RoomOptions roomOptions = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 2
        
        };
        PhotonNetwork.CreateRoom("_RoomName" + RandomRoom, roomOptions);

    }
    public override void OnCreatedRoom()
    {
       
    }


    public override void OnJoinedRoom()
    {
        BattleBtn.SetActive(false);
        Debug.Log(PhotonNetwork.CurrentRoom.Name + " im in room");

      
    }
   



}/////////////////////////////////////
