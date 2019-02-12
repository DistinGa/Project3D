using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Geekbrains
{
    public class ScoreUi: MonoBehaviour
    {
        [SerializeField] private Text txtScore;
        private int _points;

        private int Points
        {
            get
            {
                return _points;
            }
            set
            {
                _points = value;
                ShowScore();
            }
        }

        private void Awake()
        {
            if (txtScore == null)
                txtScore = GetComponentInChildren<Text>();

            Points = 0;
        }

        public void AddPoints()
        {
            AddPoints(1);
        }

        public void AddPoints(int amount)
        {
            Points += amount;
        }

        public void ShowScore()
        {
            txtScore.text = $"Score: {Points}";
        }
    }
}
