using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player"; // Le tag du joueur
    [SerializeField] private string TrophyName = "Trophy"; // Le nom du Trophée
    [SerializeField] private string KeyName = "Key";    // Le nom de la clé
    [SerializeField] private string ShurikenName = "Shuriken"; // Le nom de l'arme (Shuriken)

    private AudioSource audioSource;

    private void Awake()
    {
        // Vérifie la présence d'un composant AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning($"Aucun AudioSource trouvé sur {gameObject.name}. Aucun son ne sera joué.");
        }
    }

    private void OnTriggerEnter(Collider other)
{
    // Vérifie si l'objet entrant dans le trigger a le tag "Player"
    if (other.CompareTag(playerTag))
    {
        // Cas où on récupère le trophée
        if (gameObject.name == TrophyName)
            GameManager.Instance.TrophyCollected();

        // Cas où on récupère la clé
        else if (gameObject.name == KeyName)
            GameManager.Instance.KeyCollected();

        // Cas où on récupère un shuriken (arme)
        else if (gameObject.name == ShurikenName)
            GameManager.Instance.WeaponCollected();

        // Cas où on récupère une pièce
        else
            GameManager.Instance.AddCoin();

        // Cache le Renderer de l'objet (pour le rendre invisible)
        Renderer renderer = GetComponentInChildren<MeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = false;  // Rendre l'objet invisible
        }

        // Désactive le collider pour empêcher d'interagir à nouveau avec l'objet
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;  // Désactive la détection de collision
        }

        // Ne détruit pas l'objet, mais le rend inactif pour que le joueur puisse le récupérer
        gameObject.SetActive(false);  // Désactive l'objet, mais il reste dans la scène
    }
}


    private System.Collections.IEnumerator PlaySoundAndDestroy()
    {
        // Si un AudioSource est défini
        if (audioSource != null)
        {
            audioSource.Play();                // Joue le son attaché au composant

            // Attend la durée du clip audio actuellement configuré
            yield return new WaitForSeconds(audioSource.clip.length);
        }
        else
        {
            Debug.LogWarning($"Aucun AudioSource trouvé sur {gameObject.name}. Aucun son ne sera joué.");
        }

        // Détruire l'objet après la lecture du son (ou immédiatement si aucun son n'est joué)
        Destroy(gameObject);
    }
}
