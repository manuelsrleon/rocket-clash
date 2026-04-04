using UnityEngine;

public class ForceTimescale : MonoBehaviour
{
    [SerializeField]
    bool forceTimescale = true;

    private void Awake()
    {
        if (forceTimescale)
        {
            Time.timeScale = 1;
        }
    }
}
