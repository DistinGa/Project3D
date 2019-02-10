using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
    public class SciFiGunLightAmmo : Ammunition
    {
        [SerializeField] private GameObject _fxGO;
        [SerializeField] private AudioSource _fxAudioSource;

        private void OnCollisionEnter(Collision collision)
        {
            var victim = collision.gameObject.GetComponent<IDamagable>();
            victim?.ApplyDamage(_currentDamage, transform.position, Vector3.zero);
            float _destroyDelay = 0.2f;
            if (_fxGO != null)
            {
                if (_fxAudioSource != null)
                {
                    _fxAudioSource.pitch = Random.Range(0.9f, 1.1f);
                    _destroyDelay = _fxAudioSource.clip.length;
                }

                _fxGO.SetActive(true);
                _fxGO.transform.parent = null;
                Destroy(_fxGO, _destroyDelay);
            }

            Destroy(gameObject);
        }
    }
}
