using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SpawnManager : MonoBehaviour {

	public GameObject SpawnPointPrefab = null;		// Prefab of slot object that gets created when a slot is added
	public List<GameObject> SpawnPoints = new List<GameObject>(); // Manager maintains List to add, remove and allow access to all slots
	
	public void CreateSpawnPoint()
	{
		// Add to the back
		SpawnPoint point = Instantiate(SpawnPointPrefab).GetComponent<SpawnPoint>();
		point.Manager = this;
		point.Index = SpawnPoints.Count;
		SpawnPoints.Add(point.gameObject);
	}


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

