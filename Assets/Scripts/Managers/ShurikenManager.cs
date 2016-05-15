using UnityEngine;
using System.Collections;

public class ShurikenManager : MonoBehaviour
{
	private static GameObject Prefab;	// Prefab we load
	private GameObject Shuriken;		// Actual instantiation of shuriken, ensures only one
	private int whoHasShuriken;			// ID of player who has the shuriken

	// Default Constructor
	ShurikenManager()
	{	
		// Grab the resource and load it
		Prefab = Resources.Load ("Shuriken") as GameObject;
	}

	public void GiveShuriken(int playerID)
	{
		whoHasShuriken = playerID;
	}

	// Ask if we have the shuriken
	public bool HasShuriken(int playerID)
	{
		return (whoHasShuriken == playerID);
	}

	// Get the size / radius of the shuriken
	public float GetShurikenSize()
	{
		return Prefab.transform.localScale.x * 0.5f;
	}

	// Creates shuriken instance and applies velocity in throw direction
	public void CreateInstance(Vector2 position, Vector2 direction, float throwSpeed)
	{
		// Only create if a valid player has shuriken
		if(whoHasShuriken != -1)
		{
			// Remove whoever has the shuriken
			whoHasShuriken = -1;
			// Then create it
			Shuriken = Instantiate(Prefab);
			Shuriken.transform.position = position;
			Shuriken.GetComponent<Rigidbody2D>().velocity = direction * throwSpeed;
		}
	}
}
