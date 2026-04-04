using UnityEngine;
using UnityEngine.Events;

public class GeneralGameActions : MonoBehaviour
{
    [SerializeField]
    bool disableResume = false;
    
    #region Events initialization

    [SerializeField]
    UnityEvent OnPause, OnResume;

    private void Awake()
    {
        OnGamePauseStateChange += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        OnGamePauseStateChange -= OnPauseStateChanged;
    }

    #endregion

    public void QuitGame()
    {
        Application.Quit();
    }

    public static bool pause { get; private set; }  = false;
    public void TogglePause() {
        if (pause) {
            Resume();
        } else {
            Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        OnGamePauseStateChange?.Invoke(true);
        pause = true;
    }

    public void Resume(bool force = false)
    {
        if (disableResume && !force) return;
        Time.timeScale = 1;
        OnGamePauseStateChange.Invoke(false);
        pause = false;
    }

    // Events for state spread
    private void OnPauseStateChanged(bool isPaused)
    {
        if (isPaused)
        {
            OnPause.Invoke();
        }
        else
        {
            OnResume.Invoke();
        }
    }

    delegate void OnGamePauseStateChangeHandler(bool isPaused);
    private static event OnGamePauseStateChangeHandler OnGamePauseStateChange;
}
