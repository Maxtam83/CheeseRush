using System.Collections;
using UnityEngine;

public class BarrelMana : MonoBehaviour
{
    private Animator barrelAnimator;
    private AudioSource audioSource;
    private CapsuleCollider capsuleCollider;

    private void Awake()
    {
        // Vérifie la présence d'un composant AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning($"Aucun AudioSource trouvé sur {gameObject.name}. Aucun son ne sera joué.");
        }

    }

    private void Start()
    {
        barrelAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet entrant est le joueur
        if (other.CompareTag("Player"))
        {
            //  condition voir si le joueur à recup la clé
            if (GameManager.Instance.isKeyIsCollected())
                // ouvrir le tonneau
                BarrilOpening();
            else
                // afficher messageInfo
                GameManager.Instance.SetActiveTextBarrelInfo(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Vérifie si l'objet entrant est le joueur
        if (other.CompareTag("Player"))
        {
            //  desactivé messageInfo
            GameManager.Instance.SetActiveTextBarrelInfo(false);
        }
    }

    private void BarrilOpening()
    {
        // Récupère le CapsuleCollider attaché au GameObject
        capsuleCollider = GetComponent<CapsuleCollider>();

        // désactive le Capsule Collider pour eviter de rejouer l'animation
        if (capsuleCollider != null)
        {
            // Désactive le CapsuleCollider
            capsuleCollider.enabled = false;
        }
        else
        {
            Debug.LogWarning("Aucun CapsuleCollider trouvé sur ce GameObject.");
        }

        // lance animation
        barrelAnimator.SetTrigger("BarrelAnimation");

        // permet d'éviter que ça boucle
        ResetTriggerAfterFrame("BarrelAnimation");

        // lance le son ouverture coffre
        StartCoroutine(PlaySound());

    }

    private System.Collections.IEnumerator ResetTriggerAfterFrame(string triggerName)
    {
        yield return null; // Attend une frame
        barrelAnimator.ResetTrigger(triggerName);
    }

    private IEnumerator PlaySound()
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
    }

    // détruit le tonneau via les evenements de l'animation à la fin de celle ci
    public void DestroyBarrel()
    {
        Destroy(gameObject);
    }
}
