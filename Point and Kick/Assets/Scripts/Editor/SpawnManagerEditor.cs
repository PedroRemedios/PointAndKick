using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnManager))]
public class SpawnManagerEditor : Editor {
    public override void OnInspectorGUI() {
        serializedObject.Update();
        SpawnManager spawnManager = (SpawnManager) target;
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("combatRounds"));

        if(spawnManager.combatRounds.Count == 0) {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("infiniteEnemy"));
            spawnManager.simultaneouslyAlive = EditorGUILayout.IntField("Number Alive", spawnManager.simultaneouslyAlive);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
    
