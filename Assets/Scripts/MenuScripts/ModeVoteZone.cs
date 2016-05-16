using UnityEngine;
using System.Collections;

public class AddVoteForModeEvent : CustomEvent
{
	public AddVoteForModeEvent(GameModes.Type type)
	{
		VoteToAdd = type;
	}

	public GameModes.Type VoteToAdd;
}

public class RemoveVoteForModeEvent : CustomEvent
{
	public RemoveVoteForModeEvent(GameModes.Type type)
	{
		VoteToRemove = type;
	}

	public GameModes.Type VoteToRemove;
}

public class ModeVoteZone : MonoBehaviour {

	public GameModes.Type ModeRepresented;
	[HideInInspector]
	public int votes;

	public void AddVote()
	{
		++votes;
		EventManager.Instance.SendEvent<AddVoteForModeEvent>(new AddVoteForModeEvent(ModeRepresented), null);
	}

	public void RemoveVote()
	{
		--votes;
		EventManager.Instance.SendEvent<RemoveVoteForModeEvent>(new RemoveVoteForModeEvent(ModeRepresented), null);
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
