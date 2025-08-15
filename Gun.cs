using UnityEngine;
using System.Collections;

namespace EclipseProtocol
{
    public class Gun : MonoBehaviour
    {
        [Header("Gun Settings")]
        public int maxAmmo = 30;
        public int currentAmmo;
        public float fireRate = 0.15f;
        public float reloadTime = 1.5f;
        public float damage = 25f;
        public float range = 100f;

        [Header("References")]
        public Transform firePoint;
        public ParticleSystem muzzleFlash;
        public Camera fpsCamera;

        private bool isReloading = false;
        private float nextTimeToFire = 0f;

        void Start()
        {
            currentAmmo = maxAmmo;
        }

        void Update()
        {
            if (isReloading) return;

            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }

            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + fireRate;
                Shoot();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Reload());
            }
        }

        void Shoot()
        {
            currentAmmo--;

            if (muzzleFlash != null) muzzleFlash.Play();

            RaycastHit hit;
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
            {
                // You can add enemy damage logic here
                Debug.Log("Hit: " + hit.collider.name);
            }
        }

        IEnumerator Reload()
        {
            isReloading = true;
            Debug.Log("Reloading...");
            yield return new WaitForSeconds(reloadTime);
            currentAmmo = maxAmmo;
            isReloading = false;
        }
    }
}
