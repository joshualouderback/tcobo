using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelVoteManager : VoteManager {

	public void OnEnable()
	{
		EventManager.Instance.Connect<AddVoteForLevelEvent>(OnAddVote, null);
		EventManager.Instance.Connect<RemoveVoteForLevelEvent>(OnRemoveVote, null);
	}

	public void OnDisable()
	{
		EventManager.Instance.Disconnect<AddVoteForLevelEvent>(OnAddVote, null);
		EventManager.Instance.Disconnect<RemoveVoteForLevelEvent>(OnRemoveVote, null);
	}

	// Simple tally tracking delegates for vote events
	public void OnAddVote(AddVoteForLevelEvent addVoteEvent)
	{
		++VoteCount[(int)addVoteEvent.VoteToAdd];
	}
	public void OnRemoveVote(RemoveVoteForLevelEvent removeVoteEvent)
	{
		--VoteCount[(int)removeVoteEvent.VoteToRemove];
	}

	// Override base class implementation to size array properly
	public override int GetSize()
	{
		return (int)Levels.Enum.COUNT;
	}

	// Override base class implementation to store voted choice in the GameManager
	public override void SaveVotedChoice(int index)
	{
		Singleton<GameManager>.Instance.LevelEnum = (Levels.Enum)index;
	}

	// Override base class implementation to load the level stored in property
	public override void LoadNextLevel()
	{
		SceneManager.LoadScene(Singleton<GameManager>.Instance.LevelEnum.ToString());
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
