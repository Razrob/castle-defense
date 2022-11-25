using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUICell : MonoBehaviour
{
    [SerializeField] private ResourceID _resourceID;
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private Image _iconImage;

    public ResourceID ResourceID => _resourceID;

    public void SetCount(int count)
    {
        _countText.text = $"{count}";
    }

    public void SetIcon(Sprite icon)
    {
        _iconImage.sprite = icon;
    }
}