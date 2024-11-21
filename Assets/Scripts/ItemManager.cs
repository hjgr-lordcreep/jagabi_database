using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
public class ItemManager : MonoBehaviour
{
    //��ư�� ����
    private Button[] buttons = new Button[10];

    //�α׾ƿ� ��ư ����
    [SerializeField] private Button logout;
    [SerializeField] private TextMeshProUGUI mailText;
    [SerializeField] private TextMeshProUGUI itemText;

    //�����ͺ��̽��� ����
    public class DatabaseItem
    {
        public string userid { get; set; }
        public string itemid { get; set; }
        public string count { get; set; }

    }

    private string mail;


    // ������ Prefab ����Ʈ (���� ���� �迭�� ����)
    [SerializeField] private List<GameObject> itemPrefabs = new List<GameObject>();


    // �� ������ ����Ʈ�� ������ ����Ʈ
    private List<List<GameObject>> itemLists = new List<List<GameObject>>();
    [SerializeField] private List<string> invenIdList = new List<string>();
    Dictionary<string, int>  idDic = new Dictionary<string, int>();

    Dictionary<string, int>  originDic = new Dictionary<string, int>();
    


    private void Awake()
    {
        // ����Ʈ �ʱ�ȭ (�� ����Ʈ�� �ʱ�ȭ)
        foreach (var prefab in itemPrefabs)
        {
            itemLists.Add(new List<GameObject>());
        }

        mail = PlayerPrefs.GetString("email");
        Debug.Log("mail: " + mail);
        mailText.text = mail;

        AddDic();
    }


    private void Start()
    {
        //�����۸���Ʈ ��������
        StartCoroutine(GetDatabaseCoroutine());

        InitialCreateItems();

        //�α׾ƿ���ư�� Ŭ������ �� ��ųʸ� ���
        logout.onClick.AddListener(() =>
        {
            foreach(KeyValuePair<string, int>iddic in idDic)
            {
                Debug.Log("�������� ���");
                StartCoroutine(AddItemCoroutine(mail, iddic.Key, iddic.Value));
                SceneManager.LoadScene("Jagabee1");
            }


        });

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
        int numItems = 3; // ������ �������� ������ ���մϴ�.

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
            itemText.text =  _itemId + "�� ������" + idDic[_itemId]+"��";
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

                    Debug.Log(databaseItem.userid + " : " + databaseItem.itemid + " : " + databaseItem.count);
                }
            }
        }
    }

    private IEnumerator AddItemCoroutine(string _mail, string _itemid, int _count)
    {
        string uri = "http://127.0.0.1/itemupdate.php";

        //int count = _count + 

        //����  Ű�� �޾ƿ� ���̰�
        WWWForm form = new WWWForm();
        form.AddField("usermail", _mail);
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
                Debug.Log("AddItem Success : " + _mail + "(" + _itemid + ")" + "(" + _count + ")");
            }
        }
    }


}
