using UnityEngine;
using System.Collections;

public class Shuriken : MonoBehaviour {
	
	// We want to grab reference to our rigidbody at start
	void Start () 
	{
	}

	// When we hit anything, destroy ourselves
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			// If the player is catching, destroy shuriken

				Destroy(this.gameObject);
				// Set the player to be holding it

				// Stop catching coroutine

				// Start throwing coroutine

			// Otherwise kill them
		}
		else
		{
			this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
		}
	}
}
