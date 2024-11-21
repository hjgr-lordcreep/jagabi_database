using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public void Init(Item _itemInfo)
    {
        Image img = GetComponent<Image>();
        img.color = _itemInfo.itemColor;
    }
}