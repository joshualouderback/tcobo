using UnityEngine;
using System.Collections;

abstract public class VoteZone : MonoBehaviour {

	public virtual void AddVote() {}
	public virtual void RemoveVote() {}

	void TriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Player")
			AddVote();
	}

	void TriggerExit2D(Collider2D collider)
	{
		if(collider.tag == "Player")
			RemoveVote();
	}
}
