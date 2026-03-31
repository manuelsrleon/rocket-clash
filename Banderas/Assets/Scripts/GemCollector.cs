using UnityEngine;

public class GemCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reproducir efecto de luz
            GemEffects.CreateLightBeam(transform.position);
            
            // Notificar al contador de gemas
            GemCounter.Instance.AddGem();
            
            // Destruir la gema
            Destroy(gameObject);
        }
    }
}
