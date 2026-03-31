using UnityEngine;
using TMPro;

public class GemCounter : MonoBehaviour
{
    public static GemCounter Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private int gemsNeededToLevelUp = 5;
    [SerializeField] private int currentLevel = 1;
    
    private int gemsCollected = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddGem()
    {
        gemsCollected++;
        UpdateUI();
        
        // Verificar si se completó el nivel
        if (gemsCollected >= gemsNeededToLevelUp)
        {
            LevelUp();
        }
    }

    private void UpdateUI()
    {
        if (counterText != null)
        {
            counterText.text = $"Gemas: {gemsCollected}/{gemsNeededToLevelUp}\nNivel: {currentLevel}";
        }
    }

    private void LevelUp()
    {
        currentLevel++;
        gemsCollected = 0;
        gemsNeededToLevelUp += 2; // Aumenta la dificultad
        
        Debug.Log($"¡NIVEL COMPLETADO! Ahora en nivel {currentLevel}");
        
        // Aquí puedes cargar la siguiente escena o regenerar gemas
        LevelManager.Instance.NextLevel();
    }

    public int GetGemsCollected() => gemsCollected;
    public int GetGemsNeeded() => gemsNeededToLevelUp;
    public int GetCurrentLevel() => currentLevel;
}
