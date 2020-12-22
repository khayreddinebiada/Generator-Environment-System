using System.IO;
using UnityEditor;
using UnityEngine;

namespace game.levels
{
    [ExecuteInEditMode]
    public class LevelGenerator : MonoBehaviour
    {
        public enum GenerateOn { OnY, OnZ }

        public LevelContent levelContent;
        public LevelsAttrsGenerator levelsAttrsGenerator;
        public string pathTextLevelData;

        public GenerateOn generateOn;
        public Vector2 objectScale;
#if UNITY_EDITOR
        public void MakeLevel()
        {
            if (File.Exists(pathTextLevelData))
            {
                string[] line = File.ReadAllLines(pathTextLevelData);
                for (int i = 0; i < line.Length; i++)
                {
                    GameObject lineObj = new GameObject("Line " + (i + 1));
                    lineObj.transform.SetParent(levelContent.generatingPlace);
                    lineObj.transform.position = levelContent.generatingPlace.position;
                    lineObj.transform.rotation = levelContent.generatingPlace.rotation;

                    for (int j = 0; j < line[i].Length; j++)
                    {
                        for(int k = 0; k < levelsAttrsGenerator.attributes.Length; k++)
                        {
                            if (line[i][j] == levelsAttrsGenerator.attributes[k].character)
                            {
                                if(generateOn == GenerateOn.OnZ)
                                    InstaintiateAsPrefab(levelsAttrsGenerator.attributes[k].piece, new Vector3(j * objectScale.x, 0, i * objectScale.y), Quaternion.identity, new Vector3(objectScale.x, objectScale.x, objectScale.x), lineObj.transform);
                                else
                                    InstaintiateAsPrefab(levelsAttrsGenerator.attributes[k].piece, new Vector3(j * objectScale.x, i * objectScale.y, 0), Quaternion.identity, new Vector3(objectScale.x, objectScale.x, objectScale.x), lineObj.transform);
                            }
                        }
                    }
                }
            }
        }

        public void DeleteLevel()
        {
            DestroyAllChildren(levelContent.generatingPlace);
        }

        public static void DestroyAllChildren(Transform content)
        {
            while(content.childCount != 0)
            {
                DestroyImmediate(content.GetChild(0).gameObject);
            }
        }

        public GameObject InstaintiateAsPrefab(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 localScale, Transform parent)
        {
            GameObject clone = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            clone.transform.SetParent(parent);
            clone.transform.localPosition = position;
            clone.transform.localRotation = rotation;
            clone.transform.localScale = localScale;

            return clone;
        }
#endif
    }
}