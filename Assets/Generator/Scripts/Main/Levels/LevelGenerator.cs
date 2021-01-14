using System.IO;
using UnityEditor;
using UnityEngine;

namespace game.levels
{
    [ExecuteInEditMode]
    public class LevelGenerator : MonoBehaviour
    {
        public enum GenerateOn { OnY, OnZ }
        public enum From { DownToUp, UpToDown }

        public LevelContent levelContent;
        public LevelsAttrsGenerator levelsAttrsGenerator;
        public string pathTextLevelData;

        public GenerateOn generateOn = GenerateOn.OnZ;
        public From from = From.DownToUp;

        public Vector2 separateDistance = new Vector2(1, 1);
#if UNITY_EDITOR
        public void MakeLevel()
        {
            if (File.Exists(pathTextLevelData))
            {
                string[] line = File.ReadAllLines(pathTextLevelData);
                for (int i = line.Length - 1; 0 <= i; i--)
                {
                    GameObject lineObj = new GameObject("Line " + (i + 1));
                    lineObj.transform.SetParent(levelContent.generatingPlace);
                    lineObj.transform.position = levelContent.generatingPlace.position;
                    lineObj.transform.rotation = levelContent.generatingPlace.rotation;

                    for (int j = 0; j < line[i].Length; j++)
                    {
                        for (int k = 0; k < levelsAttrsGenerator.attributes.Length; k++)
                        {
                            if (line[i][j] == levelsAttrsGenerator.attributes[k].character)
                            {
                                if (generateOn == GenerateOn.OnZ)
                                    InstaintiateAsPrefab(levelsAttrsGenerator.attributes[k].piece,
                                        new Vector3(j * separateDistance.x, 0, ((from == From.DownToUp)?(line.Length - i - 1) : i) * separateDistance.y),
                                        Quaternion.identity,
                                        lineObj.transform);
                                else
                                    InstaintiateAsPrefab(levelsAttrsGenerator.attributes[k].piece,
                                        new Vector3(j * separateDistance.x, ((from == From.DownToUp) ? (line.Length - i - 1) : i) * separateDistance.y, 0),
                                        Quaternion.identity,
                                        lineObj.transform);
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
            while (content.childCount != 0)
            {
                DestroyImmediate(content.GetChild(0).gameObject);
            }
        }

        public GameObject InstaintiateAsPrefab(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            GameObject clone = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            clone.transform.SetParent(parent);
            clone.transform.localPosition = position;
            clone.transform.localRotation = rotation;

            return clone;
        }
#endif
    }
}