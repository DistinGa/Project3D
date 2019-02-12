using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Geekbrains
{
    public class EnemyController : BaseController
    {
        public GameObject EnemyPrefab;
        public ScoreUi ScoreUi;

        List<IEnemy> Enemies;

        public EnemyController()
        {
            Enemies = new List<IEnemy>();
            if (ScoreUi == null)
                ScoreUi = GameObject.FindObjectOfType<ScoreUi>();

            foreach (var item in GameObject.FindObjectsOfType<Foe>())
            {
                item.Init(ScoreUi.AddPoints);
                AddEnemyToList(item);
            }

            foreach (var item in GameObject.FindObjectsOfType<Victim>())
            {
                item.Init(ScoreUi.AddPoints);
                AddEnemyToList(item);
            }
        }

        public override void OnUpdate()
        {
            List<IEnemy> _listToDel = new List<IEnemy>();

            foreach (var item in Enemies)
            {
                if (item != null)
                    item.OnUpdate();
                else
                    _listToDel.Add(item);
            }

            foreach (var item in _listToDel)
            {
                Enemies.Remove(item);
            }
        }

        public GameObject CreateFoe(Vector3 Position)
        {
            GameObject newEnemy = GameObject.Instantiate(EnemyPrefab, Position, Quaternion.identity);
            var _foe = newEnemy.GetComponent<Foe>();
            if (_foe != null)
            {
                _foe.Init(ScoreUi.AddPoints);
                AddEnemyToList(_foe);
            }
            return newEnemy;
        }

        void AddEnemyToList(IEnemy enemy)
        {
            Enemies.Add(enemy);
        }
    }
}
