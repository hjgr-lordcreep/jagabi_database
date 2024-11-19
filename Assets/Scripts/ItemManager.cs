using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // 아이템 Prefab 리스트 (정적 변수 배열로 관리)
    [SerializeField] private List<GameObject> itemPrefabs = new List<GameObject>();

    // 각 아이템 리스트를 저장할 리스트
    private List<List<GameObject>> itemLists = new List<List<GameObject>>();

    private void Awake()
    {
        // 리스트 초기화 (빈 리스트로 초기화)
        foreach (var prefab in itemPrefabs)
        {
            itemLists.Add(new List<GameObject>());
        }
    }

    private void Start()
    {
        // 아이템 생성 (각 아이템마다 5개씩)
        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            CreateItems(itemPrefabs[i], itemLists[i]);
        }
    }

    // 아이템을 생성하고 리스트에 추가하는 함수
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
