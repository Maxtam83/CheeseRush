using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player"; // Le tag du joueur
    [SerializeField] private string TrophyName = "Trophy"; // Le nom du Trophée
    [SerializeField] private string KeyName = "Key";    // Le nom de la clé
    [SerializeField] private string ShurikenName = "Shuriken"; // Le nom de l'arme (Shuriken)
    [SerializeField] private GameObject Player;


    private AudioSource audioSource;
    private bool isPickedUp = false; // Flag pour vérifier si l'objet a déjà été ramassé

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
        // Vérifie si l'objet entrant dans le trigger a le tag "Player" et si l'objet n'a pas déjà été ramassé
        if (other.CompareTag(playerTag) && !isPickedUp)
        {
            isPickedUp = true; // Marque l'objet comme ramassé

            // Cas où on récupère le trophée
            if (gameObject.name == TrophyName)
            {
                GameManager.Instance.TrophyCollected();
                Destroy(Player);
            }

            // Cas où on récupère la clé
            else if (gameObject.name == KeyName)
                GameManager.Instance.KeyCollected();

            // Cas où on récupère un shuriken (arme)
            else if (gameObject.name == ShurikenName)
            {
                GameManager.Instance.WeaponCollected();
                WeaponController weaponController = other.GetComponent<WeaponController>();
                if (weaponController != null)
                {
                    weaponController.CollectShuriken();
                }
            }

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

            // Démarre la coroutine pour jouer le son
            StartCoroutine(PlaySoundAndDisable());
        }
    }

    private System.Collections.IEnumerator PlaySoundAndDisable()
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

        // Désactive l'objet après la lecture du son (ou immédiatement si aucun son n'est joué)
        gameObject.SetActive(false);
    }
}