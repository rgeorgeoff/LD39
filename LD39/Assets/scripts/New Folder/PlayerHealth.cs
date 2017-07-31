using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{	
	public float maxHealth = 100f;
	public float health = 100f;					// The player's health.
	private float damagePerSec = 8f;		// How frequently the player can be damaged.s
	public AudioClip[] ouchClips;				// Array of clips to play when the player is damaged.
	public float hurtForce = 10f;				// The force with which the player is pushed when hurt.
	public float damageAmount = 10f;			// The amount of damage to take when enemies touch the player

	private float lastHitTime;					// The time at which the player was last hit.
	private Vector3 healthScale;				// The local scale of the health bar initially (with full health).
	private PlayerControl playerControl;		// Reference to the PlayerControl script.
	private Animator anim;						// Reference to the Animator on the player

	public Light myLight;
	private float maxLightIntensity = 2.3f;
	private float maxLightRange = 20f;

	private float deadLightIntensity = 0f;
	private float deadLightRange = 0f;

	private bool died = false;

	public SpriteRenderer bodySR;
	public RespawnHandler rspnH;

	void Awake ()
	{
		// Setting up references.
		playerControl = GetComponent<PlayerControl>();
		anim = GetComponent<Animator>();
		//rspnH = GameObject.FindGameObjectWithTag ("networkManager").GetComponent<RespawnHandler> ();
	}

	void Update ()
	{
		
		//slowly kill the player, and dim light accordingly IFFFF ... blah blah blah
		health -= Time.deltaTime * damagePerSec;
		myLight.intensity = Mathf.Lerp (maxLightIntensity, deadLightIntensity, 1-(health / maxHealth));
		myLight.range = Mathf.Lerp (maxLightRange, deadLightRange, 1-(health / maxHealth));
		if (rspnH == null) {
			GameObject rspnHGO = GameObject.FindGameObjectWithTag ("neededStuff");
			if (rspnHGO != null) {
				rspnH = rspnHGO.GetComponent<RespawnHandler> ();
			}
		}

		if (rspnH != null && health < 0 && !rspnH.IsDead()) {
			string[] hints = new string[]{ "Recharge at your teams bonfires to not die!", "Your Death is not in vein!, you became a bonfire beacon for your team!"};
			killPlayer (0, "No More Energy!", hints[Random.Range(0,hints.Length)]);
		}
	}


	void OnCollisionEnter2D (Collision2D col)
	{
		// If the colliding gameobject is an Enemy...
		if(col.gameObject.tag == "Enemy")
		{
			// ... and if the time exceeds the time of the last hit plus the time between hits...
			if (Time.time > lastHitTime + damagePerSec) 
			{
				// ... and if the player still has health...
				if(health > 0f)
				{
					// ... take damage and reset the lastHitTime.
					TakeDamage(col.transform); 
					lastHitTime = Time.time; 
				}
				// If the player doesn't have health, do some stuff, let him fall into the river to reload the level.
				else
				{
					// Find all of the colliders on the gameobject and set them all to be triggers.
					Collider2D[] cols = GetComponents<Collider2D>();
					foreach(Collider2D c in cols)
					{
						c.isTrigger = true;
					}

					// Move all sprite parts of the player to the front
					SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
					foreach(SpriteRenderer s in spr)
					{
						s.sortingLayerName = "UI";
					}

					// ... disable user Player Control script
					GetComponent<PlayerControl>().enabled = false;

					// ... disable the Gun script to stop a dead guy shooting a nonexistant bazooka
					GetComponentInChildren<Gun>().enabled = false;

					// ... Trigger the 'Die' animation state
					anim.SetTrigger("Die");
				}
			}
		}
	}


	void TakeDamage (Transform enemy)
	{
		// Make sure the player can't jump.
		playerControl.jump = false;

		// Create a vector that's from the enemy to the player with an upwards boost.
		Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;

		// Add a force to the player in the direction of the vector and multiply by the hurtForce.
		GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);

		// Reduce the player's health by 10.
		health -= damageAmount;

		// Play a random clip of the player getting hurt.
		int i = Random.Range (0, ouchClips.Length);
		AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
	}

	public bool dead;

	public void killPlayer(int deathCause, string reason, string hint){
		rspnH.Kill (deathCause, playerControl, bodySR, this, reason, hint);

	}


}
