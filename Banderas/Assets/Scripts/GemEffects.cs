using UnityEngine;
using System.Collections;

public class GemEffects : MonoBehaviour
{
    // Parámetros del haz de luz (como Fortnite)
    private static float beamHeight = 20f;
    private static float beamDuration = 0.8f;
    private static float beamWidth = 2f;
    private static Color beamColor = new Color(0f, 0.8f, 1f, 0.6f); // Cyan/Azul

    public static void CreateLightBeam(Vector3 position)
    {
        // Crear un cilindro para simular el haz de luz
        GameObject beam = new GameObject("LightBeam");
        beam.transform.position = position;

        // Añadir renderer
        LineRenderer lineRenderer = beam.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = beamColor;
        lineRenderer.endColor = new Color(beamColor.r, beamColor.g, beamColor.b, 0f);
        lineRenderer.startWidth = beamWidth;
        lineRenderer.endWidth = 0.1f;

        // Posiciones del haz
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, position);
        lineRenderer.SetPosition(1, position + Vector3.up * beamHeight);

        // Corrutina para destruir después
        beam.AddComponent<BeamDestroyer>().SetDuration(beamDuration);
    }

    public static void SetBeamParameters(float height, float duration, float width, Color color)
    {
        beamHeight = height;
        beamDuration = duration;
        beamWidth = width;
        beamColor = color;
    }
}

// Componente auxiliar para destruir el haz después de cierto tiempo
public class BeamDestroyer : MonoBehaviour
{
    private float duration;

    public void SetDuration(float d) => duration = d;

    private void Start()
    {
        Destroy(gameObject, duration);
    }
}
