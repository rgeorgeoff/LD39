using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using UnityEngine.Experimental.UIElements;

public class ToNextSceneButtonScript : MonoBehaviour {

	public UnityEngine.UI.Button myButton;

	public GameObject tf;
	public GameObject enterNameHint;
	public UnityEngine.UI.Toggle blue;
	public UnityEngine.UI.Toggle red;
	public GameObject colorHint;

	void Start()
	{
		UnityEngine.UI.Button btn = myButton.GetComponent<UnityEngine.UI.Button>();
		btn.onClick.AddListener(OnFirstButtonClick);
	}

	public void OnFirstButtonClick(){
		if(tf.GetComponent<InputField>().text.Length == 0 || tf.GetComponent<InputField>().text == "Enter Name")
		{
			enterNameHint.SetActive(true);
		}
		else if((blue.isOn && red.isOn) || (!blue.isOn && !red.isOn))
		{
			colorHint.SetActive(true);
		}
		else
		{
			StaticVarScript.changeName(tf.GetComponent<InputField>().text);
			if (red.isOn)
				StaticVarScript.changeTeam (1);
			SceneManager.LoadScene(1);
		}
	}
}