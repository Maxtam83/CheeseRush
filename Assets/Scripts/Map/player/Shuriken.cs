using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float damage = 10f; // Dégâts infligés à l'ennemi

    private void OnTriggerEnter(Collider other)
    {
        // Si le shuriken touche un ennemi
        if (other.CompareTag("Enemy"))
        {
            EnemyAiTutorial enemy = other.GetComponent<EnemyAiTutorial>();
            if (enemy != null)
            {
                enemy.TakeDamage((int)damage); // Infliger des dégâts à l'ennemi
                Debug.Log("Shuriken a touché un ennemi !");
            }
        }
    }
}