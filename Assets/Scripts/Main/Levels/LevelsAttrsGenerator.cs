using UnityEngine;

namespace game.levels
{
    [System.Serializable]
    public class Attribute
    {
        [SerializeField]
        private char _character;
        public char character
        {
            get { return _character; }
        }

        [SerializeField]
        private GameObject _piece;
        public GameObject piece
        {
            get { return _piece; }
        }
    }

    [CreateAssetMenu(fileName = "LevelsGenerator", menuName = "Levels/LevelAttrs", order = 1)]
    public class LevelsAttrsGenerator : ScriptableObject
    {
        [SerializeField]
        private Attribute[] _attributes;
        public Attribute[] attributes 
        {
            get { return _attributes; }
        }
    }

}