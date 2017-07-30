using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pixelChopper : MonoBehaviour {

	// Use this for initialization
	public Texture2D mTexture;
	void Start () {
		float h = GetComponent<Image> ().sprite.rect.height;
		float w = GetComponent<Image> ().sprite.rect.width;
		Debug.Log(w + "  " + h);
		Color[] pixels = GetComponent<Image>().sprite.texture.GetPixels();
		int x = 0;
		foreach(Color c in pixels)
		{
			if(Random.value > .5f)
				pixels [x] = Color.black;
			x++;
		}
		Debug.Log (pixels.Length);
		mTexture = new Texture2D((int) w, (int) h);
		mTexture.SetPixels (pixels);
		Sprite s = Sprite.Create(mTexture, GetComponent<Image>().sprite.rect, GetComponent<Image>().sprite.pivot);
		GetComponent<Image> ().sprite = s;
		//Color[] c = mTexture.GetPixels (0, 0, 200, 200);
		//Texture2D m2Texture = new Texture2D (200, 200);
		//m2Texture.SetPixels (c);
		//m2Texture.Apply ();
		//game
	}

	// Update is called once per frame
	void Update () {
		
	}
}
