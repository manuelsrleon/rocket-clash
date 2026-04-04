using System.Collections;
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
    UnityEvent OnBegin, OnFinished;

    [SerializeField]
    TextController linkedText;

    private Coroutine countdownCoroutine;
    private bool isRunning;

    private void Awake()
    {
        if (startOnAwake)
        {
            Begin();
        }
    }

    public void Begin()
    {
        if (isRunning)
        {
            Stop();
        }
        OnBegin?.Invoke();

        countdownCoroutine = StartCoroutine(CountdownRoutine());
    }

    public void Stop()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }
        isRunning = false;
    }

    private IEnumerator CountdownRoutine()
    {
        isRunning = true;
        short remainingSeconds = secondsToCountDown;

        while (remainingSeconds > 0)
        {
            OnTick?.Invoke(remainingSeconds);
            InternalOnTick(remainingSeconds);
            yield return new WaitForSecondsRealtime(1f);
            remainingSeconds--;
        }

        isRunning = false;
        OnFinished?.Invoke();
    }

    private void OnDisable()
    {
        Stop();
    }

    private void InternalOnTick(short remaining)
    {
        if (linkedText != null)
        {
            linkedText.SetText(remaining);
        }
    }
}
