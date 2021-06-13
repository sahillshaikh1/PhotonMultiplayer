using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

using Photon.Pun.UtilityScripts;

public class ScoreManager : MonoBehaviourPunCallbacks//,IPunObservable
{
    public static ScoreManager Instance { get; set; }
    public int Score =0;
    public Text UiScore;

    public int OpponentScore =0;
   public Text OpponentUiScore;

    public GameObject scoreInsta;
    public PhotonView PhotonView;
    public string Name;
    public PhotonView Pv;
   
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(Score);
    //    }
    //    else
    //    {
    //        Score = (int)stream.ReceiveNext();
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Instance == null)
        {
            Instance = this;
        }


        UiScore.text = Name + Score.ToString();
        OpponentUiScore.text = Name + OpponentScore.ToString();
       
    }
    public void ScoreClick()
    {
        if (Pv.IsMine)
        {
          
            Pv.RPC("Click", RpcTarget.All);
            
        }
        else
        {
           
            Pv.RPC("OppoClick", RpcTarget.All);
           
        }

    }
    [PunRPC]
    void Click()
    {

       Score++;
       

    }
    [PunRPC]
    void OppoClick()
    {
       
        OpponentScore++;


    }




}///////////////
