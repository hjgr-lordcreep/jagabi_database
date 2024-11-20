using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // 아이템 Prefab 리스트 (정적 변수 배열로 관리)
    [SerializeField] private List<GameObject> itemPrefabs = new List<GameObject>();

    // 각 아이템 리스트를 저장할 리스트
    private List<List<GameObject>> itemLists = new List<List<GameObject>>();

    private Transform itemsParent;
    private void Awake()
    {
        // 리스트 초기화 (빈 리스트로 초기화)
        foreach (var prefab in itemPrefabs)
        {
            itemLists.Add(new List<GameObject>());
        }

        // 'items' 빈 오브젝트를 동적으로 생성
        GameObject itemsObject = new GameObject("Items");
        itemsParent = itemsObject.transform;  // 생성된 오브젝트의 Transform을 부모로 설정
    }

    private void Start()
    {
        // 아이템 생성
        InitializeItems();
    }

    // 아이템을 생성하는 기능을 캡슐화한 함수
    public void InitializeItems()
    {
        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            CreateItems(itemPrefabs[i], itemLists[i]);
        }
    }

    ////아이템을 생성하고 리스트에 추가하는 함수
    //private void CreateItems(GameObject itemPrefab, List<GameObject> itemList)
    //{
    //    for (int i = 0; i < Random.Range(0, 5); i++)
    //    {
    //        Vector3 randomPos = new Vector3(Random.Range(-5f, 5f), Random.Range(0.5f, 2f), Random.Range(-5f, 5f));
    //        GameObject spawnedItem = Instantiate(itemPrefab, randomPos, Quaternion.identity);
    //        itemList.Add(spawnedItem);
    //    }
    //}

    // 아이템을 생성하고 리스트에 추가하는 함수
    private void CreateItems(GameObject itemPrefab, List<GameObject> itemList)
    {
        int numItems = Random.Range(1, 5); // 생성할 아이템의 개수를 정합니다.

        for (int i = 0; i < numItems; i++)
        {
            // 아이템을 생성할 랜덤 위치
            Vector3 randomPos = new Vector3(Random.Range(0f, 1f), Random.Range(0.5f, 2f), Random.Range(0f, 1f));

            // 아이템을 생성
            GameObject spawnedItem = Instantiate(itemPrefab, randomPos, Quaternion.identity);

            // 생성된 아이템의 부모를 'itemsParent'로 설정
            spawnedItem.transform.SetParent(itemsParent);

            // 랜덤한 방향으로 튕겨 나갈 수 있도록 Rigidbody에 힘을 가합니다.
            Rigidbody rb = spawnedItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 randomDirection = Random.onUnitSphere; // 구형 범위에서 랜덤 방향을 생성
                float force = Random.Range(5f, 10f); // 랜덤한 힘을 부여
                rb.AddForce(randomDirection * force, ForceMode.VelocityChange); // 폭죽처럼 퍼지도록 힘을 가함
            }

            // 아이템을 리스트에 추가
            itemList.Add(spawnedItem);
        }
    }


}
