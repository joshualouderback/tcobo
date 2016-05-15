using UnityEngine;
using System.Collections;

public class Throw : MonoBehaviour {

	public float throwSpeed = 1.0f;		// How much force applied when thrown
	public float throwFactor = 1.0f;	// Ratio of how far in throw direction to spawn it

	private Coroutine mThrowUpdate_ = null; // Managers the players throw coroutine
	private Vector3 mAimDirection_; // Direction the player is aiming
	private PlayerData mPlayer_;

	// Enable coroutine if it is not already active
	public void EnableThrowing()
	{
		if(mThrowUpdate_ == null)
			mThrowUpdate_ = StartCoroutine(TriggerUpdate());
	}

	// Disable coroutine if it is active
	public void DisableThrowing()
	{
		if(mThrowUpdate_ != null)
			StopCoroutine(mThrowUpdate_);
	}

	// Coroutine for updating when triggered
	IEnumerator TriggerUpdate () 
	{
		// While throw is not down, yield
		//while(!InputManager.AnyInput(mPlayer_.mPlayerID))
			yield return null;

		// Otherwise throw button was hit...

		// Get normalized direction
		//mAimDirection_ =  new Vector3(InputManager.GetAxis("Right Stick Horizontal", mPlayer_.mPlayerID),
		//S							 -InputManager.GetAxis("Right Stick Vertical", mPlayer_.mPlayerID), 0).normalized;
		// Calculate the position where we would create if thrown
		Vector3 createDistance = this.transform.position + mAimDirection_ * throwFactor;

		// Check if at that position, we would hit something
		Collider2D collider = Physics2D.OverlapCircle(createDistance, 
		                                              Singleton<ShurikenManager>.Instance.GetShurikenSize());
		// If we don't hit anything, spawn shuriken
		if(collider == null)
		{
			Singleton<ShurikenManager>.Instance.CreateInstance(createDistance, mAimDirection_, throwSpeed);
		}
	}
}
