using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public void Init(PItem.PItemInfo _itemInfo)
    {
        Image img = GetComponent<Image>();
        img.color = _itemInfo.RareToColor();
    }

    //Items items = new Items();
    //public void Init()
    //{
    //    Image img = GetComponent<Image>();
    //    img.color = items.itemColor;
    //}
}