using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextController : MonoBehaviour
{
    TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public void SetText(string newText)
    {
        text.text = newText;
    }

    public void SetText(int textNumber) => SetText(textNumber.ToString());
    public void SetText(short textNumber) => SetText(textNumber.ToString());
}
