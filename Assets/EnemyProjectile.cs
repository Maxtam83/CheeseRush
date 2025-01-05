using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Le projectile touche le joueur
            Killable player = other.GetComponent<Killable>();
            if (player != null)
            {
                player.TakeDamage(damage); // Infliger des dégâts au joueur
            }
            
            // Détruire le projectile
            Destroy(gameObject);
        }
    }
}
