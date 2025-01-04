using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Instance statique pour un accès global
    public static GameManager Instance;

    // Référence au texte dans le Canvas pour l'affichage
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text keyText;
    [SerializeField] private TMP_Text TrophyText;
    [SerializeField] private GameObject BarrelInfoGameObject;
    
    private TMP_Text BarrelInfoText;
    private int coinsCollected = 0; 
    private bool collectedKey = false;

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
            Debug.LogWarning("Aucun Texte trouvé sur le GameObject "+ BarrelInfoGameObject.name);
        }
    }

    // Getteur clé
    public bool isKeyIsCollected()
    {
        return collectedKey;
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

    // Méthode pour quand l'utilisateur s'approche du tonneau et qu'il n'a pas la clé
    public void SetActiveTextBarrelInfo(bool printed)
    {
        BarrelInfoGameObject.SetActive(printed);
    }
    
    // Méthode pour quand on récupère le trophé
    public void TrophyCollected()
    {
        UpdateTrophyText();
    }

    // Met à jour le texte affiché dans le Canvas
    private void UpdateCoinsText()
    {
        coinsText.text = "Coins : " + coinsCollected;
    }

    // Met à jour le texte affiché dans le Canvas
    private void UpdateKeyText()
    {
        keyText.text = "key is collected : True";
    }

    // Met à jour le texte affiché dans le Canvas
    private void UpdateTrophyText()
    {
        TrophyText.text = "trophy is collected : True";
    }
}
