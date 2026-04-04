using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActionDetector : MonoBehaviour
{
    [SerializeField]
    private string actionName;

    [SerializeField]
    private UnityEvent OnAction;

    private InputAction action;

    private void Awake()
    {
        action = InputSystem.actions.FindAction(actionName);
#if UNITY_EDITOR
        if (action == null)
        {
            Debug.LogError($"Action '{actionName}' not found in Input System actions.");
        }
#endif
        action.performed += OnActionTrigger;
    }

    private void OnDestroy()
    {
        action.performed -= OnActionTrigger;
    }

    private void OnActionTrigger<T>(T cb) {
        if (!enabled) return;
        OnAction.Invoke();
    }
}
