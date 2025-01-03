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

    private int coinsCollected = 0; 

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
        UpdateKeyText();
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
