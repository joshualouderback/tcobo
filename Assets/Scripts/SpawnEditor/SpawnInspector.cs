using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(SpawnManager))]
public class SpawnInspector : Editor {
	
	// Need specialized inspector in order to build and destroy slot objects during editor time
	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector();

		SpawnManager spawner = (SpawnManager)target;
		if(GUILayout.Button("Add Spawn Point"))
		{
			spawner.CreateSpawnPoint();
		}
	}
}
#endif 
