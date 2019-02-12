using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Geekbrains
{
    public class WeaponUi : MonoBehaviour
    {
        [SerializeField] Text txtAmmo;

        private void Awake()
        {
            if(txtAmmo == null)
                txtAmmo = GetComponentInChildren<Text>();
        }

        public void UpdateUI(string txt)
        {
            txtAmmo.text = txt;
        }

        public void SetActive(bool value)
        {
            txtAmmo.gameObject.SetActive(value);
        }
    }
}