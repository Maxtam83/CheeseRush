using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Instance statique pour un accès global
    public static GameManager Instance;

    // Référence au texte dans le Canvas pour l'affichage
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text keyText;
    [SerializeField] private TMP_Text TrophyText;
    [SerializeField] private TMP_Text WeaponText; // Ajout pour l'arme
    [SerializeField] private GameObject BarrelInfoGameObject;
    [SerializeField] private GameObject WinView;
    [SerializeField] private GameObject LoseView;
    [SerializeField] private GameObject MenuView;
    [SerializeField] private GameObject MainMenuButton;

    private TMP_Text BarrelInfoText;
    private int coinsCollected = 0;
    private bool collectedKey = false;
    private bool collectedShuriken = false; // Variable pour savoir si l'arme a été collectée

    private void Awake()
    {
        // Assure qu'il n'y a qu'une seule instance du GameManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        BarrelInfoText = BarrelInfoGameObject.GetComponent<TMP_Text>();
        if (BarrelInfoText == null)
        {
            Debug.LogWarning("Aucun Texte trouvé sur le GameObject " + BarrelInfoGameObject.name);
        }
    }

    // Getteur clé
    public bool isKeyIsCollected()
    {
        return collectedKey;
    }

    // Getteur shuriken
    public bool isShurikenCollected()
    {
        return collectedShuriken;
    }

    // Méthode pour ajouter une pièce
    public void AddCoin()
    {
        coinsCollected++;
        UpdateCoinsText();
    }

    // Méthode pour quand on récupère la clé
    public void KeyCollected()
    {
        collectedKey = true;
        UpdateKeyText();
    }

    // Méthode pour quand on récupère le trophée
    public void TrophyCollected()
    {
        PutWindView(true);
        UpdateTrophyText();
    }

    // Méthode pour quand on récupère un Shuriken (arme)
    public void WeaponCollected()
    {
        collectedShuriken = true;

        // Vérifie si WeaponText est assigné avant d'essayer de l'utiliser
        if (WeaponText != null)
        {
            UpdateWeaponText(); // Met à jour le texte lié à l'arme
        }
        else
        {
            Debug.LogWarning("WeaponText n'est pas assigné dans GameManager.");
        }
    }

    public void BackToMainMenu()
    {
        Debug.Log("retour menu jeux");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Menu");
    }

    public void PutLoseView(bool put)
    {
        LoseView.SetActive(put);
        MainMenuButton.SetActive(put);
    }

    public void PutWindView(bool put)
    {
        WinView.SetActive(put);
        MainMenuButton.SetActive(put);
    }

    public void PutMenuView()
    {
        // TODO lose wiews
    }

    // Met à jour le texte affiché dans le Canvas
    private void UpdateCoinsText()
    {
        coinsText.text = "Coins : " + coinsCollected;
    }

    // Met à jour le texte affiché dans le Canvas
    private void UpdateKeyText()
    {
        keyText.text = "Key is collected: True";
    }

    // Met à jour le texte affiché dans le Canvas
    private void UpdateTrophyText()
    {
        TrophyText.text = "Trophy is collected: True";
    }

    // Met à jour le texte affiché pour l'arme
    private void UpdateWeaponText()
    {
        WeaponText.text = "Shuriken collected: True"; // Affiche l'arme comme collectée
    }

    // Méthode pour quand l'utilisateur s'approche du tonneau et qu'il n'a pas la clé
    public void SetActiveTextBarrelInfo(bool printed)
    {
        BarrelInfoGameObject.SetActive(printed);
    }
}
