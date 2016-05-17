using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(SpawnPoint))]
public class SpawnPointInspector : Editor {

	// Need specialized inspector in order to build and destroy slot objects during editor time
	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector();

		SpawnPoint spawner = (SpawnPoint)target;
		if(GUILayout.Button("Remove Spawn Point"))
		{
			spawner.RemoveSpawnPoint();
		}
	}
}
#endif 
