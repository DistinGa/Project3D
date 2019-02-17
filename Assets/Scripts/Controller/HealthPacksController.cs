using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Geekbrains
{
    public class HealthPacksController
    {
        private int _packsAmount;
        private float _HPamount;
        private HealthPack[] ListHP;

        public HealthPacksController(int packsAmount, float HPamount, GameObject prefab)
        {
            _packsAmount = packsAmount;
            _HPamount = HPamount;
            ListHP = new HealthPack[packsAmount];

            InitGeneration(prefab);
        }

        private void InitGeneration(GameObject prefab)
        {
            for (int i = 0; i < _packsAmount; i++)
            {
                HealthPack _healthPack = GameObject.Instantiate(prefab).GetComponent< HealthPack>();
                _healthPack.Init(_HPamount, RespawnHealthPack);
                RespawnHealthPack(_healthPack);
                ListHP[i] = _healthPack;
            }
        }

        private void RespawnHealthPack(HealthPack healthPack)
        {
            Vector3 _hpPosition;
            NavMeshHit _NavMeshHit;

            _hpPosition = Random.insideUnitSphere * 100;
            float maxDistance = 5f;
            while (!NavMesh.SamplePosition(_hpPosition, out _NavMeshHit, maxDistance, NavMesh.AllAreas))
            {
                maxDistance *= 2f;
            }

            _hpPosition = _NavMeshHit.position;
            healthPack.Transform.position = _hpPosition;
        }

        /// <summary>
        /// Ближайшая аптечка.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Vector3 GetNearestHealthPack(Vector3 pos)
        {
            float _tmpDist, _minDist = float.MaxValue;
            int _minInd = 0;
            NavMeshPath _path = new NavMeshPath();
            Vector3 _prevCorner;
            for (int i = 0; i < ListHP.Length; i++)
            {
                _tmpDist = 0;
                var item = ListHP[i];
                if (NavMesh.CalculatePath(pos, item.Transform.position, NavMesh.AllAreas, _path))
                {
                    _prevCorner = _path.corners[0];
                    foreach (var corner in _path.corners)
                    {
                        _tmpDist += (corner - _prevCorner).sqrMagnitude;
                        _prevCorner = corner;
                    }

                    if (_tmpDist < _minDist)
                    {
                        _minInd = i;
                        _minDist = _tmpDist;
                    }
                }
            }

            return ListHP[_minInd].Transform.position;
        }
    }
}
