using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerPixelHandler : MonoBehaviour {

	//this will be an event reciever:
	Dictionary<Vector2, PixelPickup> pixels = new Dictionary<Vector2, PixelPickup>();

	public GUIText blueT;
	public GUIText redT;

	int blue = 0;
	int red = 0;
	int n = 0;
		
	public void RegisterPixelWithManager(PixelPickup p)
	{
		if(!pixels.ContainsKey(new Vector2(p.posx, p.posy)))
			pixels.Add(new Vector2(p.posx, p.posy), p);
	}


	void Update()
	{
		CalculatePoints ();
	}

	void CalculatePoints()
	{
		blue = 0;
		red = 0;
		n = 0;
		foreach(PixelPickup p in pixels.Values)
		{
			int t = p.GetCurrentTeam ();
			if (t == 0)
				blue++;
			if (t == 1)
				red++;
			if (t == 2)
				n++;
		}

		UpdateScore ();
	}

	private void UpdateScore()
	{
		blueT.text = "Blue:" + blue.ToString();
		redT.text = "Red:"+ red.ToString();
		//Debug.Log (blue + "  " + red + "  " + n);
	}

	public void SetColorFor(Vector2 v, int team)
	{
		pixels [v].team = team;
	}

}
