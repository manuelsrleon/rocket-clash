using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PauseActions : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnPauseTrigger, OnResumeTrigger;

    InputAction pauseAction;

    private void Awake()
    {
        pauseAction = InputSystem.actions.FindAction("Pause");
        pauseAction.performed += OnAction;
    }

    private void OnDestroy()
    {
        pauseAction.performed -= OnAction;
    }

    private void OnAction<T>(T cb) {
        var isPaused = GeneralGameActions.pause;
        if (isPaused) {
            OnResumeTrigger.Invoke();
        } else {
            OnPauseTrigger.Invoke();
        }
    }
}
