using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PixelPickup : NetworkBehaviour
{
	public AudioClip pickupClip;		// Sound for when the bomb crate is picked up.

//	private Animator anim;				// Reference to the animator component.

	public Color[] colors;

	public SpriteRenderer sr;
	private ServerPixelHandler SPH;

	[SyncVar]
	public int posx;

	[SyncVar]
	public int posy;

	[SyncVar]
	public int team = 2;

	private int lastteam = 2;

	public int GetCurrentTeam()
	{
		return team;
	}

	public GameObject pixelEffect;
	void Awake()
	{
		// Setting up the reference.
		//anim = transform.root.GetComponent<Animator>();

	}



	void Start()
	{
		
	}

	void Update()
	{
		
		if(SPH == null)
		{
			GameObject SPHGO = GameObject.FindGameObjectWithTag("neededStuff");
			if (SPHGO) {
				SPH = SPHGO.GetComponent<ServerPixelHandler>();
				SPH.RegisterPixelWithManager (this);
			}
		}
		//switch color based on the

		if(lastteam != team)
		{
			ChangeColorToNewTeam(team);
			lastteam = team;
		}

	}

	void ChangeColorToNewTeam(int team)
	{
		sr.color = colors [team];
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		// If your player enters the trigger zone...
		if(other.tag == "Player")
		{
			// ... play the pickup sound effect.
			//AudioSource.PlayClipAtPoint(pickupClip, transform.position);

			// Increase the number of bombs the player has.
			//other.GetComponent<LayBombs>().bombCount++;

			//createEffect if its not your color:
			if (StaticVarScript.team != this.team) {
				Instantiate (pixelEffect, this.transform.position, Quaternion.identity);
				// Destroy the crate.
				//Destroy(transform.root.gameObject);
				//this.team = StaticVarScript.team;
				Debug.Log("switching teams");
				other.GetComponent<PlayerControl>().CmdSetColor(StaticVarScript.team, new Vector2(this.posx, this.posy));
			}
		}
	}
}
