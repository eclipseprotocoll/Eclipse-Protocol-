using UnityEngine;

namespace EclipseProtocol
{
    public class SafeZone : MonoBehaviour
    {
        public float shrinkRate = 0.5f; // units per second
        public float minSize = 5f;

        void Update()
        {
            Vector3 size = transform.localScale;
            size -= Vector3.one * shrinkRate * Time.deltaTime;

            if (size.x < minSize) size = Vector3.one * minSize;

            transform.localScale = size;
        }

        void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            PlayerController player = other.GetComponent<PlayerController>();
            if (player == null) return;

            // Player outside safe zone takes damage
            Health health = player.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(10f * Time.deltaTime);
            }
        }
    }
}
