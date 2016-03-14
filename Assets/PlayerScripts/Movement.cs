using UnityEngine;
using System.Collections;
using TeamUtility.IO;

public class Movement : MonoBehaviour {

	public float mMoveSpeed = 100.0f; // Move speed of our player

	private Rigidbody2D mBody_;		// Reference to rigidbody, for easy update
	private PlayerData mPlayer_;	// Reference to our player data, for ID

	// Grab our instance of rigidbody
	void Start()
	{
		mBody_ = this.gameObject.GetComponent<Rigidbody2D>();
		mPlayer_ = this.gameObject.GetComponent<PlayerData>();
	}

	// Update our movement
	void Update()
	{
		// Set velocity based on our normalized input and move speed
		mBody_.velocity = new Vector3(InputManager.GetAxis("Left Stick Horizontal", mPlayer_.mPlayerID),
									 -InputManager.GetAxis("Left Stick Vertical", mPlayer_.mPlayerID), 0).normalized 
									 * mMoveSpeed * Time.deltaTime;
	}
}
