using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player"; // Le tag du joueur

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet entrant dans le trigger a le tag "Player"
        if (other.CompareTag(playerTag))
        {
            // Ajoute une pièce au compteur global via le GameManager
            GameManager.Instance.AddCoin();

            // Détruit cet objet (celui auquel ce script est attaché)
            Destroy(gameObject);
        }
    }
}
