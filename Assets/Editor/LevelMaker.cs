using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using game.levels;

[CustomEditor(typeof(LevelGenerator))]
public class LevelMaker : Editor
{
    SerializedProperty levelContent;
    SerializedProperty levelsAttrsGenerator;
    SerializedProperty pathTextLevelData;
    SerializedProperty objectScale;
    SerializedProperty generateOn;

    // Start is called before the first frame update
    private void OnEnable ()
    {
        levelContent = serializedObject.FindProperty("levelContent");
        levelsAttrsGenerator = serializedObject.FindProperty("levelsAttrsGenerator");
        pathTextLevelData = serializedObject.FindProperty("pathTextLevelData");
        objectScale = serializedObject.FindProperty("objectScale");
        generateOn = serializedObject.FindProperty("generateOn");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Level Settings");

        serializedObject.Update();
        LevelGenerator _buildingStruct = (LevelGenerator)target;

        if (_buildingStruct.levelContent == null)
            EditorGUILayout.HelpBox("Please add the levelContent", MessageType.Error);

        EditorGUILayout.PropertyField(levelContent);

        if(_buildingStruct.levelsAttrsGenerator == null)
            EditorGUILayout.HelpBox("Please add the LevelsAttrs", MessageType.Error);

        EditorGUILayout.PropertyField(levelsAttrsGenerator);

        EditorGUILayout.HelpBox("You need give the path of your generating data!", MessageType.Info);
        if (!File.Exists(_buildingStruct.pathTextLevelData))
            EditorGUILayout.HelpBox("The path not right please give the right path!", MessageType.Error);

        EditorGUILayout.PropertyField(pathTextLevelData);
        EditorGUILayout.PropertyField(objectScale);
        EditorGUILayout.PropertyField(generateOn);

        if (GUILayout.Button("Make Floors", GUILayout.Height(50)))
        {
            _buildingStruct.MakeLevel();
        }

        if (GUILayout.Button("Delete Floors", GUILayout.Height(20)))
        {
            _buildingStruct.DeleteLevel();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
