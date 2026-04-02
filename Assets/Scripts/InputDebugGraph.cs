using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InputDebugGraphBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    InputAction throttleAction;
    float throttleValue;
    GameObject inputDebugLabel;
    void Start()
    {
        // throttleAction = InputSystem.actions.FindAction("throttleAction");
        // inputDebugLabel = GameObject.GetComponent<TextMeshProUGUI>("InputDebugLabel");
    }

    // Update is called once per frame
    void Update()
    {
        // throttleValue = throttleAction.ReadValue<float>();
        // inputDebugLabel.SetText("Throttle value: "+throttleValue);
    }
}
