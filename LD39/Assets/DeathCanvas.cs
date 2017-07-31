using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class DeathCanvas : MonoBehaviour {

	// Use this for initialization
	public InputField message;
	public Button myButton;
	public GameObject enterNameHint;
	private GameObject player;
	public Text mainText;
	public Text mainTextB;
	public Text subText;
	public Text subTextB;
	public bool isMakingFire = false;

	void Start () {
		message.Select();
		message.ActivateInputField();
		UnityEngine.UI.Button btn = myButton.GetComponent<UnityEngine.UI.Button>();
		btn.onClick.AddListener(OnFirstButtonClick);
		player = GameObject.FindGameObjectWithTag("Player") as GameObject;
	}


	void Update(){
		message.Select();
		message.ActivateInputField();
	}

	public void OnFirstButtonClick(){
		//send to server and restart!
		if (isMakingFire) {
			StartCoroutine ("sendToDB");
		} else {
			GameObject.FindGameObjectWithTag("neededStuff").GetComponent<RespawnHandler>().Respawn();
		}
	}


	IEnumerator sendToDB(){
		string endingString = "?x=" + player.transform.position.x + "&y=" + player.transform.position.y + "&n=" + StaticVarScript.name + "&i=" + StaticVarScript.icon + "&m=" + message.text + "&t=" + StaticVarScript.team;
		WWW www = new WWW (StaticVarScript.url + "death.php" + endingString);
		yield return www;
		//respawn Manager
		GameObject.FindGameObjectWithTag("neededStuff").GetComponent<RespawnHandler>().Respawn();
	}

	public void mainTextChange(string text)
	{
		mainText.text = text;
		mainTextB.text = text;
	}

	public void subTextChange(string text)
	{
		subText.text = text;
		subTextB.text = text;
	}
}
