using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathCanvas : MonoBehaviour {

	// Use this for initialization
	public InputField message;
	public Button myButton;
	public GameObject enterNameHint;
	public GameObject player;

	void Start () {
		message.Select();
		message.ActivateInputField();
		UnityEngine.UI.Button btn = myButton.GetComponent<UnityEngine.UI.Button>();
		btn.onClick.AddListener(OnFirstButtonClick);
		player = GameObject.FindGameObjectWithTag("Player") as GameObject;
		Debug.Log (player);
	}


	void Update(){
		message.Select();
		message.ActivateInputField();
	}

	public void OnFirstButtonClick(){
		if(message.GetComponent<InputField>().text.Length == 0 || message.GetComponent<InputField>().text == "Enter text")
		{
			enterNameHint.SetActive(true);
		}
		else
		{
			//send to server and restart!
			StartCoroutine("sendToDB");
		}
	}

	bool sent = false;

	IEnumerator sendToDB(){
		if (!sent) {
			sent = true;
			string endingString = "?x=" + player.transform.position.x + "&y=" + player.transform.position.y + "&n=" + StaticVarScript.name + "&i=" + StaticVarScript.icon + "&m=" + message.text + "&t=" + StaticVarScript.team;
			Debug.Log (StaticVarScript.url + "/death.php" + endingString);
			WWW www = new WWW (StaticVarScript.url + "/death.php" + endingString);
			yield return www;
			//load scene
			SceneManager.LoadScene(1);
		}
	}
}
