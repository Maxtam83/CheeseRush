using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform teleportOut; // Position de sortie
    [SerializeField] private float teleportCooldown = 2f; // Temps avant de pouvoir retéléporter

    private bool isOnCooldown = false; // Empêche la téléportation en boucle
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
        // Vérifie si l'objet entrant est le joueur
        if (other.CompareTag("Player") && teleportOut != null && !isOnCooldown)
        {
            // Téléporte le joueur
            TeleportPlayer(other.GetComponent<CharacterController>());
        }
    }

    private void TeleportPlayer(CharacterController playerController)
    {
        if (playerController == null)
        {
            Debug.LogWarning("Le joueur n'a pas de CharacterController !");
            return;
        }
        StartCoroutine(PlaySound());

        // Désactive temporairement le CharacterController pour éviter des conflits
        playerController.enabled = false;

        // lance le son

        // Téléporte le joueur
        playerController.transform.position = teleportOut.position + Vector3.up;

        // Réactive le CharacterController après le déplacement
        playerController.enabled = true;

        // Lance le cooldown
        StartCoroutine(TeleportCooldown());
    }

    private System.Collections.IEnumerator PlaySound()
    {
        // Si un AudioSource est défini
        if (audioSource != null)
        {
            audioSource.Play(); // Joue le son attaché au composant

            // Attend la durée du clip audio actuellement configuré
            yield return new WaitForSeconds(audioSource.clip.length);
        }
        else
        {
            Debug.LogWarning($"Aucun AudioSource trouvé sur {gameObject.name}. Aucun son ne sera joué.");
        }
    }

    private System.Collections.IEnumerator TeleportCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(teleportCooldown);
        isOnCooldown = false;
    }

    private void OnDrawGizmos()
    {
        // Dessine une ligne pour visualiser la connexion entre les téléporteurs
        if (teleportOut != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, teleportOut.position);
        }
    }
}
