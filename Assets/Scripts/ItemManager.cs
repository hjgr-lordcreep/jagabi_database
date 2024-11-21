using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
using Newtonsoft.Json;
public class ItemManager : MonoBehaviour
{
    public class DatabaseItem
    {
        public string id { get; set; }
        public string itemname { get; set; }
        public string information { get; set; }

    }




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
        //StartCoroutine(GetDatabaseCoroutine());

        InitialCreateItems();
    }
    public void InitialCreateItems()
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
        int numItems = Random.Range(0, 5); // 생성할 아이템의 개수를 정합니다.

        for (int i = 0; i < numItems; i++)
        {
            // 아이템을 생성할 랜덤 위치
            Vector3 randomPos = new Vector3(Random.Range(0f, 1f), Random.Range(0.5f, 2f), Random.Range(0f, 1f));

            // 아이템을 생성
            GameObject spawnedItem = Instantiate(itemPrefab, randomPos, Quaternion.identity);

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








    private IEnumerator GetDatabaseCoroutine()
    {
        string uri = "http://127.0.0.1/itemget.php";

        using (UnityWebRequest www =
            UnityWebRequest.PostWwwForm(uri, string.Empty))
        {
            yield return www.SendWebRequest();

            if (www.result ==
                UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                string data = www.downloadHandler.text;

                List<DatabaseItem> databaseItems =
                   JsonConvert.DeserializeObject<List<DatabaseItem>>(data);

                foreach (DatabaseItem databaseItem in databaseItems)
                {
                    Debug.Log(databaseItem.id + " : " + databaseItem.itemname + " : " + databaseItem.information);
                }
            }
        }
    }

    

}
