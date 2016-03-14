using UnityEngine;
using System.Collections;

public class Catch : MonoBehaviour 
{
	public float catchTime = 0.25f;    // Time to be in catching state
	public float cooldownTime = 0.25f; // Time till they can catch again

	private bool canCatch;      // Bool to notify they can trigger catch state
	private bool isCatching;	// Bool to notify if we are catching
	private ActionSequence seq; // Sequence for catch states

	// Settors for catch sequence
	private void StartCatching() { isCatching = true; }
	private void StopCatching() { isCatching = false; }
	private void CantCatch() { canCatch = false; }
	private void CanCatch() { canCatch = true; }

	// On initialization...
	void Start()
	{
		// Create instance of sequence
		seq = new ActionSequence(this.gameObject);
		// Set player being able to catch, but not in catch state
		canCatch = true;
		isCatching = false;
	}

	private void CatchActionSequence()
	{
		// Clear sequence
		seq.Cancel();
		// Stop them from catching again and start their catch state now
		CantCatch();
		StartCatching();
		// Now trigger catching frames for duration, then end it onces times up
		new ActionDelay(seq, catchTime);
		new ActionCall(seq, StopCatching);
		// Delay until cooldown is over, to allow them to catch again
		new ActionDelay(seq, cooldownTime);
		new ActionCall(seq, CanCatch);
	}

	// Gettor for asking if player is catching
	public bool IsCatching()
	{
		return isCatching;
	}

	// Update is called once per frame
	public IEnumerator TriggerUpdate () 
	{
		// Infinite loop, a manager will start and end us
		while(true)
		{
			// While they can't catch, wait until they can
			while(!canCatch)
				yield return null;
			// Then wait while they are not pressing "catch button", yield
			while(!Input.GetKeyDown(KeyCode.Space))
				yield return null;

			// They pressed "catch button" and can catch, so start sequence
			CatchActionSequence();
		}
	}
}
