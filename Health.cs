using UnityEngine;

namespace EclipseProtocol
{
    public class Health : MonoBehaviour
    {
        public float maxHealth = 100f;
        public float currentHealth;

        void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            currentHealth -= amount;
            if (currentHealth <= 0f)
                Die();
        }

        void Die()
        {
            Debug.Log(gameObject.name + " died.");
            // For now, just disable object
            gameObject.SetActive(false);
        }
    }
}
