using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SceneNavigator), typeof(Button))]
public class ButtonSceneNavigator : MonoBehaviour
{
    private SceneNavigator sceneNavigator;
    private Button button;

    private void Awake()
    {
        sceneNavigator = GetComponent<SceneNavigator>();
        button = GetComponent<Button>();

        button.onClick.AddListener(sceneNavigator.Navigate);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(sceneNavigator.Navigate);
    }
}
