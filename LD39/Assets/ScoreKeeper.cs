using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class ScoreKeeper : NetworkBehaviour {

	public GUIText blueT;
	public GUIText redT;

	NetworkClient myClient;

	public int bluepoints = 1;
	public int redpoints = 1;

	NetworkClient m_client;
	/*
	public void SendPixelGot(int myId)
	{
		var msg = new StringMessage(myId);
		m_client.Send(msg);
	}

	public void Init(NetworkClient client)
	{
		m_client = client;
		NetworkServer.RegisterHandler(OnScoreBlue);
	}

	void OnScoreBlue(NetworkMessage netMsg)
	{
		var beginMessage = netMsg.ReadMessage<IntegerMessage>();
		Debug.Log("received OnServerReadyToBeginMessage ");
	}
	*/
}
