using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
    /// <summary>
    /// Базовый класс для действий, выполняемых ботами.
    /// </summary>
    public abstract class BaseBotRoutine : MonoBehaviour
    {
        public int Priority;    // Приоритет действия.
        public float Delay; // Пауза в выполнении данного действия.
        protected float _restDelayTime = 0;

        public abstract bool Act();
        public abstract void Init();

        public void SetDelay()
        {
            _restDelayTime = Delay;
        }
    }
}
