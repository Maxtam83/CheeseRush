using UnityEngine;
using UnityEngine.UI;

public class Killable : MonoBehaviour
{
    public float maxHealth = 100f; 
    private float currentHealth;

    public UnityEngine.Events.UnityEvent OnDie;
    public Slider healthBar; 

    private void Start()
    {
        // Initialisation de la santé
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        // Vérification si le personnage meurt
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }

    public void Die()
    {
        Debug.Log("Player has died!");
        if (OnDie != null)
        {
            OnDie.Invoke();
        }

        Destroy(gameObject);
    }

    // Utilisation de OnTriggerEnter pour gérer la détection des projectiles
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyProjectile"))
        {
            // Si l'objet touché est un projectile d'ennemi, prendre des dégâts
            float damage = 10f; // Ajuste la valeur des dégâts ici
            TakeDamage(damage);
        }
    }
}