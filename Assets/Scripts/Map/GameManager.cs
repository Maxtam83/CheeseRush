using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Instance statique pour un accès global
    public static GameManager Instance;

    // Référence au texte dans le Canvas pour afficher le nombre de pièce collecté
    [SerializeField] private TMP_Text coinsText;

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

    // Met à jour le texte affiché dans le Canvas
    private void UpdateCoinsText()
    {
        coinsText.text = "Coins: " + coinsCollected;
    }
}
