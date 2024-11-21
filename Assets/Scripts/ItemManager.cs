using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.Linq;
public class ItemManager : MonoBehaviour
{
    //��ư�� ����
    private Button[] buttons = new Button[10];

    //�α׾ƿ� ��ư ����
    [SerializeField] private Button logout;

    //�����ͺ��̽��� ����
    public class DatabaseItem
    {
        public string id { get; set; }
        public string itemname { get; set; }
        public string information { get; set; }

    }

    private string mail;


    // ������ Prefab ����Ʈ (���� ���� �迭�� ����)
    [SerializeField] private List<GameObject> itemPrefabs = new List<GameObject>();


    // �� ������ ����Ʈ�� ������ ����Ʈ
    private List<List<GameObject>> itemLists = new List<List<GameObject>>();
    [SerializeField] private List<string> invenIdList = new List<string>();
    Dictionary<string, int>  idDic = new Dictionary<string, int>();

    private void Awake()
    {
        // ����Ʈ �ʱ�ȭ (�� ����Ʈ�� �ʱ�ȭ)
        foreach (var prefab in itemPrefabs)
        {
            itemLists.Add(new List<GameObject>());
        }

        mail = PlayerPrefs.GetString("email");

    }


    private void Start()
    {
        //�����۸���Ʈ ��������
        StartCoroutine(GetDatabaseCoroutine());

        InitialCreateItems();

        //��ư�� Ŭ������ �� ��ųʸ� ���
        logout.onClick.AddListener(() =>
        {
            foreach (int fg in idDic.Values)
            {
                Debug.Log("Value: " + fg);
            }
        });

        AddDic();
    }
    public void InitialCreateItems()
    {
        // ������ ���� (�� �����۸��� 10����)
        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            CreateItems(itemPrefabs[i], itemLists[i]);
        }
    }

    // �������� �����ϰ� ����Ʈ�� �߰��ϴ� �Լ�
    private void CreateItems(GameObject itemPrefab, List<GameObject> itemList)
    {
        int numItems = 10; // ������ �������� ������ ���մϴ�.

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

   //��ųʸ��� �⺻ ID���� �ֱ�
    private void AddDic()
    {
        idDic.Add("1", 0);
        idDic.Add("2", 0);
        idDic.Add("3", 0);
        idDic.Add("4", 0);
        idDic.Add("5", 0);
        idDic.Add("6", 0);
        idDic.Add("7", 0);
        idDic.Add("8", 0);
        idDic.Add("9", 0);
        idDic.Add("10", 0);

    }

    //������ Ŭ������ �� ���̵� ��Ī�ϰ� ��ųʸ��� ���� +1�ϴ� �Լ�
    public void GetItem(string _itemId)
    {
        //Ŭ���� ��� �����۵��� ���� ����Ʈ
        invenIdList.Add(_itemId);

        //���࿡ �޾ƿ��� _itemID�� ��ųʸ��� �ִ� Ű�� ���� �� �� Ű�� Value���� +1
        //���� �����ϴ� �ڵ�
        if(idDic.ContainsKey(_itemId))
        {
            idDic[_itemId] += 1;
        }

        //�׽�Ʈ�� ��ųʸ� ����ڵ�
       foreach (int fg in idDic.Values)
        {
            Debug.Log("Value: " + fg);
            
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

    private IEnumerator AddItemCoroutine(string _mail, string _itemid, int _count)
    {
        string uri = "http://127.0.0.1/itemupdate.php";


        //����  Ű�� �޾ƿ� ���̰�
        WWWForm form = new WWWForm();
        form.AddField("mail", _mail);
        form.AddField("itemid", _itemid);
        form.AddField("count", _count);

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("AddScore Success : " + _mail + "(" + _itemid + ")" + "(" + _count + ")");
            }
        }
    }


}
