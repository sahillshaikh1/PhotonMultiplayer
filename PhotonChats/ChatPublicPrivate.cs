using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using ExitGames.Client.Photon;

public class ChatTest : MonoBehaviour, IChatClientListener {

	private ChatClient chatClient;
	//private string userName;
	private string currentChannelName = "Channel 001";

    public InputField inputField;
	public Text outputText;
    public InputField NameToSend;
    public InputField privatmsg;
    public Text showPrivateMsg;

    // Use this for initialization
    void Start () {
		
		Application.runInBackground = true;

	//	userName = System.Environment.UserName;
		

		chatClient = new ChatClient(this);
		chatClient.Connect(Photon.Pun.PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat
            , Photon.Pun.PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion
            , new AuthenticationValues(ClothesUIManager.Instance.ProfleName.text));

		//AddLine(string.Format("연결시도", userName));
	}
	
	public void AddLine(string lineString)
	{
		outputText.text += lineString + "\r\n";
     

    }
    public void PrivateAddLine(string lineString)
    {
       
        showPrivateMsg.text += lineString + "\r\n";

    }

    public void OnApplicationQuit ()
	{
		if (chatClient != null)
		{
			chatClient.Disconnect();
		}
	}

	public void DebugReturn (ExitGames.Client.Photon.DebugLevel level, string message)
	{
		if (level == ExitGames.Client.Photon.DebugLevel.ERROR)
		{
			Debug.LogError(message);
		}
		else if (level == ExitGames.Client.Photon.DebugLevel.WARNING)
		{
			Debug.LogWarning(message);
		}
		else
		{
			Debug.Log(message);
		}
	}

	public void OnConnected ()
	{
		AddLine ("Connected.");

		chatClient.Subscribe(new string[]{currentChannelName}, 10);
        print("Connected");
	}

	public void OnDisconnected ()
	{
		AddLine ("서버에 연결이 끊어졌습니다.");
	}

	public void OnChatStateChange (ChatState state)
	{
		//Debug.Log("OnChatStateChange = " + state);
	}

	public void OnSubscribed(string[] channels, bool[] results)
	{
	//	AddLine (string.Format("Player ({0})", string.Join(",",channels)));
	}

	public void OnUnsubscribed(string[] channels)
	{
        //AddLine (string.Format("Player ({0})", string.Join(",",channels)));
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
	{
		for (int i = 0; i < messages.Length; i++)
		{
			AddLine (string.Format("{0} : {1}", senders[i], messages[i].ToString()));
		}
        //ChatChannel chatChannel = this.chatClient.PrivateChannels[ClothesUIManager.Instance.ProfleName.text];
        //foreach (object msg in chatChannel.Messages)
        //{
        //    Debug.Log(msg);
        //}

    }

	public void OnPrivateMessage(string sender, object message, string channelName)
	{
		//Debug.Log("sender name : " + sender+"Meassange"+ message + channelName);
       // showPrivateMsg.text = sender + ":" + message;
        PrivateAddLine(string.Format("{0} : {1}",sender,message));
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
	{
		//Debug.Log("status : " + string.Format("{0} is {1}, Msg : {2} ", user, status, message)); 
	}

	void Update ()
	{
		chatClient.Service();
	}

	public void Input_OnEndEdit (string text)
	{
		if (chatClient.State == ChatState.ConnectedToFrontEnd)
		{
			//chatClient.PublishMessage(currentChannelName, text);
			chatClient.PublishMessage(currentChannelName, inputField.text);

			inputField.text = "";
		}
        //var mas = outputText.text.Split(new[] { ':' });
        //if (mas.Length == 2)
        //{
        //    chatClient.SendPrivateMessage(mas[0], mas[1]);
        //    return;
        //}
    }
    public void privatemsg()
    {
        chatClient.SendPrivateMessage(NameToSend.text, privatmsg.text);
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }
}
