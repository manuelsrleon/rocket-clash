using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject gemPrefab; // Arrastra Gemstone_Sapphire 1.prefab aquí
    [SerializeField] private int numberOfGems = 5;
    [SerializeField] private float spawnAreaRadius = 20f;
    [SerializeField] private float minHeight = 0.5f;
    [SerializeField] private float maxHeight = 2f;
    [SerializeField] private Vector3 spawnCenter = Vector3.zero;

    private void Start()
    {
        SpawnGems();
    }

    public void SpawnGems()
    {
        // Destruir gemas anteriores si existen
        GemCollector[] existingGems = FindObjectsOfType<GemCollector>();
        foreach (GemCollector gem in existingGems)
        {
            Destroy(gem.gameObject);
        }

        // Generar nuevas gemas en posiciones aleatorias
        for (int i = 0; i < numberOfGems; i++)
        {
            Vector3 randomPosition = GetRandomSpawnPosition();
            GameObject newGem = Instantiate(gemPrefab, randomPosition, Quaternion.identity);
            
            // Asegurarse de que has añadido el componente GemCollector
            if (newGem.GetComponent<GemCollector>() == null)
            {
                newGem.AddComponent<GemCollector>();
            }
        }

        Debug.Log($"Se generaron {numberOfGems} gemas");
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(-spawnAreaRadius, spawnAreaRadius);
        float randomZ = Random.Range(-spawnAreaRadius, spawnAreaRadius);
        float randomY = Random.Range(minHeight, maxHeight);

        return spawnCenter + new Vector3(randomX, randomY, randomZ);
    }

    // Método para cambiar el número de gemas (por dificultad)
    public void SetGemCount(int count)
    {
        numberOfGems = count;
    }
}
