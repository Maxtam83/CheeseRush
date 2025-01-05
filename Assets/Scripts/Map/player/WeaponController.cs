using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject shurikenPrefab; // Le prefab du shuriken
    [SerializeField] private Transform throwPoint;      // Le point d'où le shuriken sera lancé
    [SerializeField] private float throwForce = 10f;    // La force avec laquelle le shuriken sera lancé

    private bool hasShuriken = false;                   // Si le joueur a collecté le shuriken
    private GameObject currentShuriken;                 // Référence au shuriken collecté

    private void Update()
    {
        // Vérifie si le joueur a collecté l'arme et appuie sur "X" ou tout autre mécanisme d'entrée
        if (hasShuriken && Input.GetKeyDown(KeyCode.X)) 
        {
             Debug.Log("Touche X");
            LaunchShuriken();
        }
    }

    // Cette méthode sera appelée par GameManager lorsque le joueur collecte l'arme
    public void CollectShuriken()
    {
        if (!hasShuriken) // Si le joueur n'a pas déjà collecté un shuriken
        {
            hasShuriken = true;

            // Crée une instance du shuriken à la position du joueur (throwPoint)
            currentShuriken = Instantiate(shurikenPrefab, throwPoint.position, throwPoint.rotation);

            // Assure-toi que le Shuriken est prêt à être lancé (visible ou non)
            currentShuriken.SetActive(true); // Peut être ajusté selon le besoin
        }
    }

    // Lancer le shuriken
    private void LaunchShuriken()
    {
        if (currentShuriken != null && throwPoint != null)
        {
            Rigidbody rb = currentShuriken.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Applique une force pour lancer le shuriken
                rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
            }

            // Désactive le Shuriken après son lancement et réinitialise la variable
            currentShuriken.SetActive(false);
            hasShuriken = false;  // Le joueur n'a plus de shuriken pour l'instant

            // Réinitialise la référence du shuriken
            currentShuriken = null;
        }
    }
}
