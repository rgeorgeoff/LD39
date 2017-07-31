using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AutoJoin : MonoBehaviour {

	NetworkManagerHUD nmh;
	// Use this for initialization
	void Start () {
		nmh = this.GetComponent<NetworkManagerHUD> ();
		if (!nmh.showGUI) {
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
