using UnityEngine;

public class Item : MonoBehaviour
{
    public struct ItemInfo
    {
        public int rare;

        public Color RareToColor()
        {
            switch (rare)
            {
                case 0: return Color.gray;
                case 1: return Color.green;
                case 2: return Color.blue;
                case 3: return new Color(0.8f, 0.3f, 0.8f);
            }

            return Color.white;
        }
    }

    private ItemInfo info = new ItemInfo();

    public ItemInfo Info { get { return info; } }


    private void Start()
    {
        info.rare = Random.Range(0, 4);
        SetColorWithRare();
    }

    private void SetColorWithRare()
    {
        MeshRenderer mr =
            GetComponentInChildren<MeshRenderer>();
        mr.material.color = info.RareToColor();
    }

}

