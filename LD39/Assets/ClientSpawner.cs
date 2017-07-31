using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ClientSpawner : NetworkManager {

	class MyManager : NetworkManager
	{
		class MBase:MessageBase
		{
			//public uint netId;
			//public NetworkHash128 assetId;
			//public Vector3 position;
			public int team;

			// This method would be generated
			public override void Deserialize(NetworkReader reader)
			{
				//netId = reader.ReadPackedUInt32();
				//assetId = reader.ReadNetworkHash128();
				//position = reader.ReadVector3();
				team = reader.ReadInt32();
			}

			// This method would be generated
			public override void Serialize(NetworkWriter writer)
			{
				//writer.WritePackedUInt32(netId);
				//writer.Write(assetId);
				//writer.Write(position);
				writer.Write(team);
			}
		}
		public override void OnClientConnect(NetworkConnection conn)
		{
			Debug.Log ("here");
			MBase mn = new MBase ();
			mn.team = StaticVarScript.team;
			short t = (short)Random.Range(0, 100000);
			ClientScene.AddPlayer (conn, t, mn);
//			GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
			//NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		}

		public override void OnServerConnect(NetworkConnection conn)
		{
			Debug.Log ("here server");
			Debug.Log ("here");
			MBase mn = new MBase ();
			mn.team = StaticVarScript.team;
			ClientScene.AddPlayer (conn, (short)Random.Range(0, 100000), mn);
			//GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
		}

		public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
		{
			Debug.Log ("HERE?");
		}
	}
}
