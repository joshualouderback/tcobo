using UnityEngine;
using System.Collections;

public class Throw : MonoBehaviour {

	public float throwSpeed = 1.0f;		// How much force applied when thrown
	public float throwFactor = 1.0f;	// Ratio of how far in throw direction to spawn it
	
	private Vector3 aimDirection; // Direction the player is aiming
	
	// Coroutine for updating when triggered
	IEnumerator TriggerUpdate () 
	{
		// While throw is not down, yield
		while(!Input.GetKeyDown(KeyCode.Space))
			yield return null;

		// Otherwise throw button was hit...

		// Get direction
		aimDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		// Normalize it
		aimDirection.Normalize();
		// Calculate the position where we would create if thrown
		Vector3 createDistance = this.transform.position + aimDirection * throwFactor;

		// Check if at that position, we would hit something
		Collider2D collider = Physics2D.OverlapCircle(createDistance, 
		                                              Singleton<ShurikenManager>.Instance.GetShurikenSize());
		// If we don't hit anything, spawn shuriken
		if(collider == null)
		{
			Singleton<ShurikenManager>.Instance.CreateInstance(createDistance, aimDirection, throwSpeed);
		}
	}
}
