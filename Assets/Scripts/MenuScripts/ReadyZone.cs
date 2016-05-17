using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReadyZone : MonoBehaviour {

	// -- Public
	public TextMesh TimerText;				// Text to modify to show time countdown
	public Object SceneToLoad;				// Scene that we load after this one
	public float TimerLength = 5.0f;		// Length of the ready timer

	// -- Private 
	private int playersReady_;				// Count of players that are in ready zone
	private float timer_;					// Timer to countdown when all players are ready

	// Init
	void Start()
	{
		playersReady_ = 0;
		timer_ = TimerLength;
	}

	void Update()
	{
		// When we have at least one player and all of them are ready
		if(playersReady_ != 0 && playersReady_ == Singleton<PlayerManager>.Instance.NumberOfPlayers)
		{
			// Countdown till load scene
			if((timer_ -= Time.deltaTime) < 0)
				SceneManager.LoadScene(SceneToLoad.name);

			// Show the countdown
			TimerText.text = timer_.ToString("0.0");
		}
		else // Else reset the time if someone unreadys
		{
			timer_ = TimerLength;
			TimerText.text = timer_.ToString("0.0");
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Player")
			++playersReady_;
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.tag == "Player")
			--playersReady_;
	}
}
