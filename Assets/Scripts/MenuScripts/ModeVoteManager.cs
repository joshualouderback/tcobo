using UnityEngine;
using System.Collections;

public class ModeVoteManager : VoteManager {

	// Connect to the add and remove mode vote events
	public void OnEnable()
	{
		EventManager.Instance.Connect<AddVoteForModeEvent>(OnAddVote, null);
		EventManager.Instance.Connect<RemoveVoteForModeEvent>(OnRemoveVote, null);
	}
	// Disconnect to the add and remove mode vote events
	public void OnDisable()
	{
		EventManager.Instance.Disconnect<AddVoteForModeEvent>(OnAddVote, null);
		EventManager.Instance.Disconnect<RemoveVoteForModeEvent>(OnRemoveVote, null);
	}

	// Simple tally tracking delegates for vote events
	public void OnAddVote(AddVoteForModeEvent addVoteEvent)
	{
		// Increment that vote, and if it's the better than our previous highest set it
		int value = ++VoteCount[(int)addVoteEvent.VoteToAdd];
		if(value >= HighestVote)
		{
			HighestVote = value;
			Debug.Log(HighestVote);
			MajorityVotes.Add((int)addVoteEvent.VoteToAdd);
		}
	}
	public void OnRemoveVote(RemoveVoteForModeEvent removeVoteEvent)
	{
		--VoteCount[(int)removeVoteEvent.VoteToRemove];
		MajorityVotes.RemoveAll(x => x == (int)removeVoteEvent.VoteToRemove);
		HighestVote = MajorityVotes.Find(x => x >= HighestVote);

		Debug.Log(HighestVote);
	}

	// Override base class implementation to size array properly
	public override int GetSize()
	{
		return (int)GameModes.Type.COUNT;
	}

	// Override base class implementation to store voted choice in the GameManager
	public override void SaveVotedChoice(int index)
	{
		Singleton<GameManager>.Instance.ModeType = (GameModes.Type)index;
	}

	// Initializing our manager
	void Start () 
	{
		base.Start();
	}
	
	// Update is called once per frame
	void Update () 
	{
		base.Update();
	}
}
