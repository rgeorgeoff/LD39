using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour
{
	float healthBonus = 100;				// How much health the crate gives the player.
	public AudioClip collect;				// The sound of the crate being collected.


	private Animator anim;					// Reference to the animator component.
	private bool landed;					// Whether or not the crate has landed.

	private DeathLocation dl;

	void Awake ()
	{
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		// If the player enters the trigger zone...
		AddHealth (other);
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Player") {
			AddHealth (other);
		}
	}

	void AddHealth (Collider2D other)
	{
		if (other.tag == "Player") {
			// Get a reference to the player health script.
			PlayerHealth playerHealth = other.GetComponent<PlayerHealth> ();

			if (dl.team != StaticVarScript.team) {
				string[] hints = new string[] {
					"Running into the other teams fire will remove them from the field!\nBut You will not get a bonefire.",
					"FOR THE GREATOR GOOD!!!"
				};
				playerHealth.killPlayer (1, "Fire Cleansed By Sacrifice!",hints[Random.Range(0,hints.Length)]);
				StartCoroutine ("RemoveFire");
			}
			// Increasse the player's health by the health bonus but clamp it at 100.
			playerHealth.health += healthBonus;
			playerHealth.health = Mathf.Clamp (playerHealth.health, 0f, 100f);
			// Play the collection sound.
			//AudioSource.PlayClipAtPoint(collect,transform.position);
			//Destroy(transform.root.gameObject);
		}
		// Otherwise if the crate hits the ground...
		else
			if (other.tag == "ground" && !landed) {
				// ... set the Land animator trigger parameter.
				anim.SetTrigger ("Land");
				transform.parent = null;
				gameObject.AddComponent<Rigidbody2D> ();
				landed = true;
			}
	}

	public void setDL(DeathLocation dl)
	{
		this.dl = dl;
	}

	bool sentRemove = false;
	IEnumerator RemoveFire()
	{
		if (!sentRemove) {
			sentRemove = true;
			string endingString = "?x=" + dl.x + "&y=" + dl.y + "&n=" + dl.name; //+ "&i=" + StaticVarScript.icon + "&m=" + message.text + "&t=" + StaticVarScript.team;
			WWW www = new WWW (StaticVarScript.url + "delete.php" + endingString);
			yield return www;
			Destroy (this.gameObject);
		}
	}
}
