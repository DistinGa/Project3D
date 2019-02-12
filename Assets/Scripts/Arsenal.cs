using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
    public class Arsenal : MonoBehaviour
    {
        [SerializeField] private List<Ammunition> _ammunitionList = new List<Ammunition>();
        [SerializeField] private List<Weapons> _weaponsList = new List<Weapons>();
        #region Property    
        public List<Weapons> WeaponsList
        {
            get { return _weaponsList; }
        }

        public List<Ammunition> GetAmmunitionList
        {
            get { return _ammunitionList; }
        }
        #endregion

        public Ammunition GetAmmoByType(AmmoTypes ammoType)
        {
            return _ammunitionList.First(a => a.AmmoType == ammoType);
        }
    }
}
