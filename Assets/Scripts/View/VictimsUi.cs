using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Geekbrains
{
    public class VictimsUi: MonoBehaviour
    {
        [SerializeField] private Text txtScore;
        private int _victimsInitCount, _victimsCount;

        private int VictimsCount
        {
            get
            {
                return _victimsCount;
            }
            set
            {
                _victimsCount = value;
                ShowScore();
            }
        }

        private void Awake()
        {
            if (txtScore == null)
                txtScore = GetComponentInChildren<Text>();

            var _victims = FindObjectsOfType<Victim>();
            foreach (var item in _victims)
            {
                item.Init(EliminateVictim);
            }
            _victimsInitCount = _victims.Length;
            VictimsCount = 0;
        }

        private void EliminateVictim()
        {
            VictimsCount++;
        }

        public void ShowScore()
        {
            txtScore.text = $"Score: {VictimsCount} / {_victimsInitCount}";
        }
    }
}
