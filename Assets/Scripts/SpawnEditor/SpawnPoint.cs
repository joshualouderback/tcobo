using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	[HideInInspector]
	public SpawnManager Manager;
	[HideInInspector]
	public int Index;

	public void RemoveSpawnPoint()
	{
		// Remove the last element
		DestroyImmediate(Manager.SpawnPoints[Index]);
		Manager.SpawnPoints.RemoveAt(Index);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
