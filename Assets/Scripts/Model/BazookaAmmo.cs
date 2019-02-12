using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
    public class BazookaAmmo : Ammunition
    {
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _explosionForce;
        [SerializeField] private UnityStandardAssets.Effects.ExplosionPhysicsForce _explosion;
        [SerializeField] private AudioSource _explosionAudioSource;
        [SerializeField] private GameObject _flameFXGO;

        private void OnTriggerEnter(Collider other)
        {
            Rigidbody.velocity = Vector3.zero;
            SetPhysics(false);
            var audS = GetComponent<AudioSource>();
            if (audS != null)
                audS.Stop();

            Explode();
            foreach (var item in GetComponentsInChildren<ParticleSystem>())
            {
                item.Stop();
            }

            if (_flameFXGO != null)
                _flameFXGO.SetActive(false);

            Destroy(gameObject, 1f);
        }

        private void Explode()
        {
            var colliders = Physics.OverlapSphere(Transform.position, _explosionRadius);
            foreach (var item in colliders)
            {
                var victim = item.gameObject.GetComponentInParent<IDamagable>();
                victim?.ApplyDamage(_damage, item.transform.position, Vector3.zero);
            }

            if (_explosion != null)
            {
                float _destroyDelay = 0.2f;
                _explosion.transform.parent = null;
                if (_explosionAudioSource != null)
                {
                    _explosionAudioSource.pitch = Random.Range(0.9f, 1.1f);
                    _destroyDelay = _explosionAudioSource.clip.length;
                }

                _explosion.explosionForce = _explosionForce;
                _explosion.explosionRadius = _explosionRadius;
                _explosion.gameObject.SetActive(true);
                Destroy(_explosion.gameObject, _destroyDelay);
            }
        }
    }
}
