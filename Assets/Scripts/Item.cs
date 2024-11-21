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
    //�����鸶�� ���̵� �ο�
    public string id = string.Empty;
}

