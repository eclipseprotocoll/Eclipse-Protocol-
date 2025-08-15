using UnityEngine;

namespace EclipseProtocol
{
    public enum PickupType { Health, Ammo }

    public class Pickup : MonoBehaviour
    {
        public PickupType type;
        public float amount = 20f;

        void OnTriggerEnter(Collider other)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player == null) return;

            switch (type)
            {
                case PickupType.Health:
                    Health health = player.GetComponent<Health>();
                    if (health != null)
                    {
                        health.TakeDamage(-amount); // heals
                        Destroy(gameObject);
                    }
                    break;
                case PickupType.Ammo:
                    if (player.gun != null)
                    {
                        player.gun.currentAmmo += (int)amount;
                        if (player.gun.currentAmmo > player.gun.maxAmmo)
                            player.gun.currentAmmo = player.gun.maxAmmo;
                        Destroy(gameObject);
                    }
                    break;
            }
        }
    }
}
