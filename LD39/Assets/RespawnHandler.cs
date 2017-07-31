using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnHandler : MonoBehaviour {

	// Use this for initialization
	PlayerControl playerControl;
	SpriteRenderer bodySR;
	PlayerHealth ph;
	DeathCanvas dcdc;
	public GameObject deadCanvas;
	public GameObject fireObj;
	GameObject dc;

	private bool isDead = false;
		
	public bool IsDead()
	{
		return isDead;
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void spawnDC()
	{
		dc = Instantiate (deadCanvas);
		dcdc = dc.GetComponent<DeathCanvas> ();
		dc.SetActive (false);
	}

	public void Kill(int deathCause, PlayerControl _playerControl, SpriteRenderer _bodySR, PlayerHealth _ph, string reason, string subtext)
	{
		if (!isDead) {
			isDead = true;
			if (dc == null)
				spawnDC ();
			dc.SetActive (true);
			dcdc.mainTextChange (reason);
			dcdc.subTextChange (subtext);
			playerControl = _playerControl;
			bodySR = _bodySR;
			ph = _ph;
			ph.health = 0;
			playerControl.dead = true;
			playerControl.maxSpeed = 0;
			playerControl.GetComponent<BoxCollider2D> ().enabled = false;
			bodySR.enabled = false;
			playerControl.GetComponent<CircleCollider2D> ().enabled = false;
			playerControl.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
			playerControl.GetComponent<Rigidbody2D> ().gravityScale = 0;
			dcdc.isMakingFire = false;

			switch (deathCause) {
			case 0: //out of energy
			//spawn the fire Object
				//GameObject g = Instantiate (fireObj, this.transform.position, Quaternion.identity);
				dcdc.message.gameObject.SetActive (true);
				dcdc.isMakingFire = true;
			//message popup!
				break;
			case 1: //sacrifice
			//send remove event!
				dcdc.message.gameObject.SetActive (false);
				break;
			}
		} else {
			Debug.Log ("killing when already dead");
		}
	}

	public void Respawn()
	{
		if (isDead) {
			isDead = false;
			ph.health = ph.maxHealth;
			playerControl.dead = false;
			if (dc == null)
				spawnDC();
			dc.SetActive (false);
			Vector3 v3 = GetSpawnPoint ();
			playerControl.gameObject.transform.position = v3;
			Camera.main.transform.position = v3 - new Vector3 (0, 0, 10);
			playerControl.ResetMaxSpeed ();
			playerControl.GetComponent<BoxCollider2D> ().enabled = true;
			bodySR.enabled = true;
			playerControl.GetComponent<CircleCollider2D> ().enabled = true;
			playerControl.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
			playerControl.GetComponent<Rigidbody2D> ().gravityScale = 1;

			//respawn fires
			GameObject.FindGameObjectWithTag("levelSpawner").GetComponent<LevelSpawner>().DownloadAndSpawnFires();
		}
	}

	public Vector3 GetSpawnPoint()
	{
		return new Vector3 ();
	}
}
