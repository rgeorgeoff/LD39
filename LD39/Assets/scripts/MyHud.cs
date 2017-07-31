#if ENABLE_UNET
using System.Collections;

namespace UnityEngine.Networking
{
	[AddComponentMenu("Network/NetworkManagerHUD")]
	[RequireComponent(typeof(NetworkManager))]
	[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
	public class MyHud : MonoBehaviour
	{
		public NetworkManager manager;
		[SerializeField] public bool showGUI = true;
		[SerializeField] public int offsetX;
		[SerializeField] public int offsetY;

		// Runtime variable
		public bool ready = false;

		public bool beServer = false;

//		string myIP = "67.188.9.36";

		void Awake()
		{
			StartCoroutine ("connect");
		}


		IEnumerator connect()
		{
			manager = GetComponent<NetworkManager> ();
			manager.StartMatchMaker ();
			while ((manager.matches == null || manager.matches.Count == 0) && !beServer) {
				manager.matchMaker.ListMatches (0, 2, "", false, 0, 0, manager.OnMatchList);
				yield return new WaitForEndOfFrame();
			}
			
			ready = true;
		}

		public void HostMatch()
		{
			manager.matchMaker.CreateMatch(manager.matchName, manager.matchSize, true,"","", "",0,0, manager.OnMatchCreate);
		}

		public void ConnectToMatch()
		{
			if (beServer) {
				HostMatch ();
			} else {
				if (ready)
					manager.matchMaker.JoinMatch (manager.matches [0].networkId, "", "", "", 0, 0, manager.OnMatchJoined);
			}
		}
	}
};
#endif //ENABLE_UNET
