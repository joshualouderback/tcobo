using UnityEngine;
using System.Collections;

// Event To Inform What Level Type This Level Is
public class LevelTypeEvent : CustomEvent
{
	public LevelTypes.Type type;

	public LevelTypeEvent(LevelTypes.Type _type)
	{ 
		type = _type;
	}
}

public class LevelType : MonoBehaviour 
{
	// Every Level Is Required To Have This Component
	public LevelTypes.Type typeOfLevel;

	void Start()
	{
		// When level is loaded, we need to inform the space what level we are
		EventManager.Instance.SendEvent<LevelTypeEvent>(new LevelTypeEvent(typeOfLevel), null);
	}
}
