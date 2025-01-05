using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damage = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Le projectile touche le joueur
            Killable player = other.GetComponent<Killable>();
            if (player != null)
            {
                Debug.Log("Projectile a touché le joueur.");
                player.TakeDamage(damage); // Infliger des dégâts au joueur
            }
            
            // Détruire le projectile
            Destroy(gameObject);
        }
    }
}