using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

public class BattelStart : MonoBehaviourPunCallbacks, IPunObservable
{
    float totalTime = 60f; //2 minutes
    public Text timer;
    public PhotonView pv;
    int minutes;
    int seconds;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(totalTime);
        }
        else
        {
            totalTime = (float)stream.ReceiveNext();
        }
    }

    private void Update()
    {
        try
        {

            if (PhotonNetwork.PlayerList.Length == 2)
            {
                //  pv.RPC("TimeRpc", RpcTarget.All);
                TimeRpc();
            }
        }
        catch(Exception e)
        {
            Debug.LogError("TimeSync not working");
        }
        if(totalTime == 0)
        {
            PhotonNetwork.LeaveRoom();

        }
       

       
       
    }
   // [PunRPC]
   public void TimeRpc()
    {
        timer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        totalTime -= Time.deltaTime;
        UpdateLevelTimer(totalTime);
    }
   
   
    public void UpdateLevelTimer(float totalSeconds)
    {
         minutes = Mathf.FloorToInt(totalSeconds / 60f);
         seconds = Mathf.RoundToInt(totalSeconds % 60f);

        string formatedSeconds = seconds.ToString();

        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }

      

      
    }

    
}///////////////////////////////////////////////
