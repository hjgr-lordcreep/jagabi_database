using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // ������ Prefab ����Ʈ (���� ���� �迭�� ����)
    [SerializeField] private List<GameObject> itemPrefabs = new List<GameObject>();

    // �� ������ ����Ʈ�� ������ ����Ʈ
    private List<List<GameObject>> itemLists = new List<List<GameObject>>();

    private void Awake()
    {
        // ����Ʈ �ʱ�ȭ (�� ����Ʈ�� �ʱ�ȭ)
        foreach (var prefab in itemPrefabs)
        {
            itemLists.Add(new List<GameObject>());
        }
    }

    private void Start()
    {
        // ������ ���� (�� �����۸��� 5����)
        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            CreateItems(itemPrefabs[i], itemLists[i]);
        }
    }

    // �������� �����ϰ� ����Ʈ�� �߰��ϴ� �Լ�
    private void CreateItems(GameObject itemPrefab, List<GameObject> itemList)
    {
        for (int i = 0; i < Random.Range(2, 5); i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-5f, 5f), Random.Range(0.5f, 2f), Random.Range(-5f, 5f));
            GameObject spawnedItem = Instantiate(itemPrefab, randomPos, Quaternion.identity);
            itemList.Add(spawnedItem);
        }
    }
}
