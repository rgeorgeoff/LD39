using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour
{
	float healthBonus = 100;				// How much health the crate gives the player.
	public AudioClip collect;				// The sound of the crate being collected.


	private Animator anim;					// Reference to the animator component.
	private bool landed;					// Whether or not the crate has landed.

	private bool used = false;

	void Awake ()
	{
	}


	void OnTriggerEnter2D (Collider2D other)
	{
		// If the player enters the trigger zone...
		if(other.tag == "Player")
		{
			Debug.Log ("heh?");
			// Get a reference to the player health script.
			PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

			// Increasse the player's health by the health bonus but clamp it at 100.
			playerHealth.health += healthBonus;
			playerHealth.health = Mathf.Clamp(playerHealth.health, 0f, 100f);

			// Play the collection sound.
			//AudioSource.PlayClipAtPoint(collect,transform.position);

			// Set Used
			SetUsed();
			//Destroy(transform.root.gameObject);
		}
		// Otherwise if the crate hits the ground...
		else if(other.tag == "ground" && !landed)
		{
			// ... set the Land animator trigger parameter.
			anim.SetTrigger("Land");

			transform.parent = null;
			gameObject.AddComponent<Rigidbody2D>();
			landed = true;	
		}
	}

	void SetUsed(){
		used = true;
	}
}
