using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class VoteManager : MonoBehaviour {

	// -- Public
	public TextMesh timeText;				// Reference to time text we will modify
	public float BaseTimerLength = 15.0f;	// Base timer length
	public float AddedTimePerPlayer = 5.0f;	// Value added to time per player

	// -- Protected
	protected int[] VoteCount; 				// Array containing the votes
	protected int HighestVote = 0;			// The number of votes that is the highest
	protected List<int> MajorityVotes;		// Array of tied votes, indexs to enum

	// -- Private
	private float timer_;					// Timer to keep track of countdown

	// Function that must be override for proper array sizing
	public abstract int GetSize();
	// Function that must be overrided to store voted choice in the GameManager
	public abstract void SaveVotedChoice(int index);
	// Function that must be overried, so we can load the correct level based on what manager we are
	public abstract void LoadNextLevel();

	// Initializing our manager
	public void Start () 
	{
		// Create our list of majority votes
		MajorityVotes = new List<int>();

		// Create the array to size of number of modes and init all to zero
		VoteCount = new int[GetSize()];
		for(int i = 0; i < GetSize(); ++i)
			VoteCount[i] = 0;

		// Set the timer using formula
		timer_ = BaseTimerLength + AddedTimePerPlayer * Singleton<PlayerManager>.Instance.NumberOfPlayers;
	}

	// Update is called once per frame
	public void Update () 
	{
		// If no majority
		if(HighestVote == 0)
		{
			// Make sure there are no votes
			MajorityVotes.Clear();
			// Reduce timer at normal rate
			timer_ -= Time.deltaTime;

			// If we are out of time
			if(timer_ <= 0)
			{
				// Select a random from range
				int index = Random.Range(0, GetSize());

				// Now save the data
				SaveVotedChoice(index);
				// And load the next scene
				LoadNextLevel();
			}
		}
		else // Otherwise we have a majority
		{
			// Remove all the votes less than our current majority
			MajorityVotes.RemoveAll(x => VoteCount[x] < HighestVote);
			// And reduce timer the highest vote rate
			timer_ -= Time.deltaTime * HighestVote;

			// If we are out of time
			if(timer_ <= 0)
			{
				// Select random if there is a tie, if there is only one then this still gives that majority
				int index = Random.Range(0, MajorityVotes.Count - 1);

				// Now save the data
				SaveVotedChoice(index);
				// And load the next scene
				LoadNextLevel();
			}
		}

		// Also set the timer text, formatted to single decimal
		timeText.text = timer_.ToString("0.0");
	}
}
