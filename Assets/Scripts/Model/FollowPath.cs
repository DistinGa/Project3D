using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
    public class FollowPath : MonoBehaviour
    {
        public List<Transform> Path;

        private int _pathNode;
        private UnityStandardAssets.Characters.ThirdPerson.AICharacterControl _AICharacterControl;

        public int PathNode
        {
            get { return _pathNode; }

            set
            {
                _pathNode = value;
                if (_pathNode == Path.Count)
                    _pathNode = 0;

                _AICharacterControl.target = Path[PathNode];
            }
        }

        void Start()
        {
            _AICharacterControl = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
            PathNode = 0;
        }

        void Update()
        {
            if ((transform.position - Path[PathNode].position).sqrMagnitude < 1)
            {
                ++PathNode;
            }
        }

        public void SetPath(List<Transform> newPath)
        {
            Path = newPath;
            PathNode = 0;
        }
    }
}
