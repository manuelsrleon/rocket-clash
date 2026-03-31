using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private GemSpawner gemSpawner;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void NextLevel()
    {
        // Opción 1: Regenerar gemas en la misma escena (aumentando dificultad)
        int currentLevel = GemCounter.Instance.GetCurrentLevel();
        int newGemCount = 5 + (currentLevel * 2); // Aumenta 2 gemas por nivel
        
        gemSpawner.SetGemCount(newGemCount);
        gemSpawner.SpawnGems();
        
        Debug.Log($"¡Nivel {currentLevel} iniciado! Necesitas recoger {newGemCount} gemas");
        
        /* Opción 2: Cargar siguiente escena (descomenta si tienes múltiples escenas)
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("¡JUEGO COMPLETADO!");
        }
        */
    }
}
