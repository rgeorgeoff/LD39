using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

	// Use this for initialization
	public GameObject nameText;

	void Awake()
	{
		nameText.GetComponent<TextMesh> ().text = "";
	}


	public void InitMe (DeathLocation dl) {
		nameText.GetComponent<TextMesh> ().text = dl.name + "\n" + dl.message;
	}

}
