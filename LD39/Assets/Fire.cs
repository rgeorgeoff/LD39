using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

	// Use this for initialization
	public GameObject nameText;
	public SpriteRenderer bonesSR;
	public SpriteRenderer fireSR;
	public HealthPickup hp;

	void Awake()
	{
		nameText.GetComponent<TextMesh> ().text = "";
	}


	public void InitMe (DeathLocation dl) {
		nameText.GetComponent<TextMesh> ().text = dl.name + "\n" + dl.message;
		if (dl.team == 0) {//blue
			bonesSR.color = Color.cyan;
			fireSR.color = Color.cyan;
		} else if (dl.team == 1) {
			bonesSR.color = Color.red;
			fireSR.color = Color.red;
		}

		hp.setDL(dl);
	}

}
