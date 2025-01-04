using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player"; // Le tag du joueur
    [SerializeField] private string TrophyName = "Trophy"; // Le nom du Trophée
    [SerializeField] private string KeyName = "Key";    // Le nom de la clé

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

            // Cas où on récupère une pièce
            else
                GameManager.Instance.AddCoin();

            // Cache le premier Renderer trouvé dans les enfants du gameObject (pour le rendre invisible)
            Renderer renderer = GetComponentInChildren<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }


            // Joue le son avant de détruire l'objet
            StartCoroutine(PlaySoundAndDestroy());
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
