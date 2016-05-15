using UnityEngine;
using UnityEngine.InputNew;
using System.Collections;

public class SelectControls : MonoBehaviour {

	public float WaitTime = 0.25f;				// Delay time between switching characters, for smoother control
	[HideInInspector]
	public int PlayerID;						// The index in player manager of this player

	// References to components
	public SpriteRenderer portraitSprite;		// To modify portrait
	public TextMesh textMesh;					// To modify hud text
	private PlayerInput playerInput;			// To get player input actions
	private GameControls controls;	// So we can read this players control input 
	// ---------------------------- // 
	private int portraitIndex_;					// The index of the portrait we are currently on
	private Coroutine scrollRoutine_;			// Used to keep track of routine to let player scroll through portraits	
	// Use this for initialization
	void Start () 
	{
		playerInput = this.GetComponent<PlayerInput>();
		controls = playerInput.GetActions<GameControls>();
		portraitIndex_ = Singleton<CharacterManager>.Instance.FindNextAvailableOnLeft(
			Random.Range(0, Singleton<CharacterManager>.Instance.NumberOfCharacters - 1), portraitSprite);
		StartCoroutine(WaitingForSelection());
		scrollRoutine_ = StartCoroutine(ScrollingUpdate());
	}

	IEnumerator WaitingForDropOut () 
	{
		while(true)
		{
			// If unconfirm was pressed, player wants to drop out
			if(controls.goBack.wasJustPressed)
			{
				// Despawn the character
				EventManager.Instance.SendEvent<LeaveGameEvent>(new LeaveGameEvent(PlayerID), null);
				// Free that character selection, start selection coroutine and end this one
				Singleton<CharacterManager>.Instance.UntakeSelection(portraitIndex_);
				textMesh.text = "Press Start To Select";
				StartCoroutine(WaitingForSelection());
				scrollRoutine_ = StartCoroutine(ScrollingUpdate());
				break;
			}

			// Otherwise no input this frame, return next one
			yield return null;
		}
	}
		
	IEnumerator ScrollingUpdate()
	{		
		while(true)
		{
			// If scroll left
			if(controls.move.vector2.x < 0.0f)
			{
				// Change portrait and yield for short period of time, so they don't fast scroll
				portraitIndex_ = Singleton<CharacterManager>.Instance.FindNextAvailableOnLeft(portraitIndex_, portraitSprite);
				yield return new WaitForSeconds(WaitTime);
			}
			// Else if scroll right
			else if(controls.move.vector2.x > 0.0f)
			{
				// Change portrait and yield for short period of time, so they don't fast scroll
				portraitIndex_ = Singleton<CharacterManager>.Instance.FindNextAvailableOnRight(portraitIndex_, portraitSprite);
				yield return new WaitForSeconds(WaitTime);
			}

			// Otherwise no input this frame, return next frame
			yield return null;
		}
	}

	IEnumerator WaitingForSelection () 
	{
		while(true)
		{
			// If at any point they make a selection 
			if(controls.start.wasJustPressed)
			{
				// And the selection is still valid (just in case someone chose them slighltly before them)
				if(Singleton<CharacterManager>.Instance.TakeSelection(portraitIndex_))
				{
					// Spawning the character
					EventManager.Instance.SendEvent<JoinGameEvent>(new JoinGameEvent(PlayerID, portraitIndex_), null);
					// Clear our the HUD text, start coroutine to listen for drop out and end this routine
					textMesh.text = "";
					StartCoroutine(WaitingForDropOut());
					StopCoroutine(scrollRoutine_);
					break;
				}
			}
			// Else if, player wants to drop out for good
			else if(controls.goBack.wasJustPressed)
			{
				// TODO: Whatever it means to drop out for good
			}

			// Otherwise no input this frame, return next frame
			yield return null;
		}
	}
}
