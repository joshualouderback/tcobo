using UnityEngine;
using System.Collections;
using UnityEngine.InputNew;

public class Movement : MonoBehaviour {

	PlayerInput playerInput;
	GameControls controls;

	public float mMoveSpeed = 100.0f; // Move speed of our player

	private Rigidbody2D mBody_;		// Reference to rigidbody, for easy update
	private PlayerData mPlayer_;	// Reference to our player data, for ID
	private Coroutine moveRoutine_; // Reference to move routine, so we don't start it multiple times

	// Connecting to space wide join and leave events
	void OnEnable()
	{
		EventManager.Instance.Connect<SetInputEvent>(OnSetInputEvent, this.gameObject);
	}
	// Disconnecting to space wide join and leave events
	void OnDisable()
	{
		EventManager.Instance.Disconnect<SetInputEvent>(OnSetInputEvent, this.gameObject);
	}

	void OnSetInputEvent(SetInputEvent inputEvent)
	{
		playerInput = inputEvent.Input;
		controls = playerInput.GetActions<GameControls>();
		Init();
	}

	void Init()
	{
		if(mBody_ == null)
			mBody_ = this.gameObject.GetComponent<Rigidbody2D>();
		if(mPlayer_ == null)
			mPlayer_ = this.gameObject.GetComponent<PlayerData>();
		if(moveRoutine_ == null)
			moveRoutine_ = StartCoroutine(MoveUpdate());
	}

	// Grab our instance of rigidbody
	void Start()
	{
		moveRoutine_ = null;
		Init();
	}

	// Update our movement
	IEnumerator MoveUpdate()
	{
		while(true)
		{
			// Set velocity based on our normalized input and move speed
			mBody_.velocity = new Vector3(controls.move.vector2.x, controls.move.vector2.y, 0).normalized 
										 * mMoveSpeed * Time.deltaTime;

			yield return null;
		}
	}

}