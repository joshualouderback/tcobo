using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class StartupManagers : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		// Create all of the managers we will need for the rest of the game
		Assert.IsTrue(Singleton<CharacterManager>.Instance.isActiveAndEnabled,   "Startup failed to create CharacterManager");		
		Assert.IsTrue(Singleton<PlayerManager>.Instance.isActiveAndEnabled,   "Startup failed to create PlayerManager");
		Assert.IsTrue(Singleton<ShurikenManager>.Instance.isActiveAndEnabled, "Startup failed to create ShurikenManager");
		Assert.IsTrue(Singleton<GameManager>.Instance.isActiveAndEnabled,     "Startup failed to create GameManager");
		//Assert.IsTrue(Singleton<InputManager>.Instance.isActiveAndEnabled,     "Startup failed to create InputManager");
	}

}
