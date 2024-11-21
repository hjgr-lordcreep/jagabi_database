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
        //StartCoroutine(GetDatabaseCoroutine());

        InitialCreateItems();
    }
    public void InitialCreateItems()
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
        int numItems = Random.Range(0, 5); // ������ �������� ������ ���մϴ�.

        for (int i = 0; i < numItems; i++)
        {
            // �������� ������ ���� ��ġ
            Vector3 randomPos = new Vector3(Random.Range(0f, 1f), Random.Range(0.5f, 2f), Random.Range(0f, 1f));

            // �������� ����
            GameObject spawnedItem = Instantiate(itemPrefab, randomPos, Quaternion.identity);

            // ������ �������� ƨ�� ���� �� �ֵ��� Rigidbody�� ���� ���մϴ�.
            Rigidbody rb = spawnedItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 randomDirection = Random.onUnitSphere; // ���� �������� ���� ������ ����
                float force = Random.Range(5f, 10f); // ������ ���� �ο�
                rb.AddForce(randomDirection * force, ForceMode.VelocityChange); // ����ó�� �������� ���� ����
            }

            // �������� ����Ʈ�� �߰�
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
