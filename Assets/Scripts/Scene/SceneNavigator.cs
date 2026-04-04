using UnityEngine;

public class SceneNavigator : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    public void Navigate()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}