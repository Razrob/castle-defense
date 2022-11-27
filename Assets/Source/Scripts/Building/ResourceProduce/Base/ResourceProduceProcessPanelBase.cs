using TMPro;
using UnityEngine;

public abstract class ResourceProduceProcessPanelBase : MonoBehaviour
{
    [SerializeField] protected TMP_Text _text;

    public void SetFill(int value, int max)
    {
        _text.text = $"{value} / {max}";
        OnFillSet(value, max);
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    protected virtual void OnFillSet(int value, int max) { }
}