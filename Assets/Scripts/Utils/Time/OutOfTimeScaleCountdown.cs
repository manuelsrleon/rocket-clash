using UnityEngine;
using UnityEngine.Events;

public class OutOfTimeScaleCountdown : MonoBehaviour
{
    [SerializeField]
    private bool startOnAwake;

    [SerializeField]
    private short secondsToCountDown;

    [SerializeField]
    UnityEvent<short> OnTick;

    [SerializeField]
    UnityEvent OnFinished;

    private void Awake()
    {
        if (startOnAwake)
        {
            Begin();
        }
    }

    public void Begin() { }
}
