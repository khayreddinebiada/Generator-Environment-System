using UnityEngine;

namespace game.levels
{
    public class LevelContent : MonoBehaviour
    {
        [SerializeField]
        private Transform _generatingPlace;
        public Transform generatingPlace
        {
            get { return _generatingPlace; }
        }
    }
}
