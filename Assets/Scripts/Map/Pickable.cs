using Unity.VisualScripting;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField] private string playerTag   = "Player"; // Le tag du joueur
    [SerializeField] private string TrophyName   = "Trophy"; // Le tag du Trophée
    [SerializeField] private string KeyName      = "Key";    // Le tag de la clé

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet entrant dans le trigger a le tag "Player"
        if (other.CompareTag(playerTag))
        {
            // cas ou on récupère le trophée
            if (gameObject.name == TrophyName)
                GameManager.Instance.TrophyCollected();

            // cas ou on récupère la clé
            else if (gameObject.name == KeyName)
                GameManager.Instance.KeyCollected();

            // cas ou on récupère une pièce
            else
                // Ajoute une pièce au compteur global via le GameManager
                GameManager.Instance.AddCoin();

            // Détruit cet objet (celui auquel ce script est attaché)
            Destroy(gameObject);
        }
    }
}
