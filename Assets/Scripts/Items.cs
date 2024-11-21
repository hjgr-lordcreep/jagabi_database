using UnityEngine;

public class Items : MonoBehaviour
{
    public Color itemColor = Color.white;
    private void Start()
    {
        MeshRenderer mr =
            GetComponentInChildren<MeshRenderer>();
        itemColor = mr.material.color;
    }
}

