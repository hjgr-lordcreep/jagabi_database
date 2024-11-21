using UnityEngine;

public class Item : MonoBehaviour
{
    public Color itemColor;
    private void Start()
    {
        MeshRenderer mr =
            GetComponentInChildren<MeshRenderer>();
        itemColor = mr.material.color;
    }
    //프리펩마다 아이디 부여
    public string id = string.Empty;
}

