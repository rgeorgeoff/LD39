using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanUpAndFade : MonoBehaviour {

	TextMesh tm;
	float scrollSpeed = 1;
	public Color startColor;
	public Color endColor;
	public float duration = 2f;
	// Use this for initialization
	private float timeAlive;
	void Start () {
		tm = GetComponent<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update () {
		timeAlive += Time.deltaTime;
		this.transform.Translate (new Vector3 (0, scrollSpeed * Time.deltaTime));
		tm.color = Color.Lerp (startColor, endColor, (timeAlive - .3f) / duration);
	}
}
