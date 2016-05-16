using UnityEngine;
using System.Collections;

public class AddVoteForLevelEvent : CustomEvent
{
	public AddVoteForLevelEvent(Levels.Enum type)
	{
		VoteToAdd = type;
	}

	public Levels.Enum VoteToAdd;
}

public class RemoveVoteForLevelEvent : CustomEvent
{
	public RemoveVoteForLevelEvent(Levels.Enum type)
	{
		VoteToRemove = type;
	}

	public Levels.Enum VoteToRemove;
}

public class LevelVoteZone : MonoBehaviour {

	public Levels.Enum LevelRepresented;
	[HideInInspector]
	public int votes;

	public void AddVote()
	{
		++votes;
		EventManager.Instance.SendEvent<AddVoteForLevelEvent>(new AddVoteForLevelEvent(LevelRepresented), null);
	}

	public void RemoveVote()
	{
		--votes;
		EventManager.Instance.SendEvent<RemoveVoteForLevelEvent>(new RemoveVoteForLevelEvent(LevelRepresented), null);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Player")
			AddVote();
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.tag == "Player")
			RemoveVote();
	}
}

