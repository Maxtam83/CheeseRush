using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    public GameObject shurikenPrefab; // Préfab du shuriken
    public Transform launchPoint;    // Point d'où le shuriken sera lancé
    public float rotationSpeed = 500f; // Vitesse de rotation du shuriken autour du joueur
    public float rotationDistance = 1f; // Distance de rotation du shuriken autour du joueur

    private PlayerInputActions playerInputActions; // Référence à l'Action Map
    private bool hasShuriken = false;             // Indique si le joueur possède un shuriken
    private GameObject currentShuriken;           // Référence au shuriken instancié

    private void Awake()
    {
        // Initialiser le système d'entrée
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        // Activer l'action de lancement
        playerInputActions.Player.Launch.performed += OnLaunch;
        playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        // Désactiver l'action de lancement
        playerInputActions.Player.Launch.performed -= OnLaunch;
        playerInputActions.Player.Disable();
    }

    // Méthode appelée lorsqu'un shuriken est récupéré
    public void CollectShuriken()
    {
        if (!hasShuriken)
        {
            hasShuriken = true;
            Debug.Log("Shuriken récupéré !");
            // Instancier le shuriken à une position plus proche du joueur
            Vector3 initialPosition = transform.position + transform.forward * rotationDistance;
            currentShuriken = Instantiate(shurikenPrefab, initialPosition, Quaternion.identity);
            currentShuriken.transform.SetParent(transform); // Attacher le shuriken au joueur
        }
    }

    private void Update()
    {
        if (hasShuriken && currentShuriken != null)
        {
            // Faire tourner le shuriken autour du joueur à une distance spécifiée
            currentShuriken.transform.RotateAround(transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    // Méthode appelée lorsqu'on appuie sur la touche de lancement
    private void OnLaunch(InputAction.CallbackContext context)
    {
        // Vous pouvez ajouter une fonctionnalité supplémentaire ici si nécessaire
    }
}