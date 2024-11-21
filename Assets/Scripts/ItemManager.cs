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
    //버튼들 선언
    private Button[] buttons = new Button[10];

    //로그아웃 버튼 선언
    [SerializeField] private Button logout;
    [SerializeField] private TextMeshProUGUI mailText;
    [SerializeField] private TextMeshProUGUI itemText;

    //데이터베이스랑 연결
    public class DatabaseItem
    {
        public string userid { get; set; }
        public string itemid { get; set; }
        public string count { get; set; }

    }

    private string mail;


    // 아이템 Prefab 리스트 (정적 변수 배열로 관리)
    [SerializeField] private List<GameObject> itemPrefabs = new List<GameObject>();


    // 각 아이템 리스트를 저장할 리스트
    private List<List<GameObject>> itemLists = new List<List<GameObject>>();
    [SerializeField] private List<string> invenIdList = new List<string>();
    Dictionary<string, int>  idDic = new Dictionary<string, int>();

    Dictionary<string, int>  originDic = new Dictionary<string, int>();
    


    private void Awake()
    {
        // 리스트 초기화 (빈 리스트로 초기화)
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
        //아이템리스트 가져오기
        StartCoroutine(GetDatabaseCoroutine());

        InitialCreateItems();

        //로그아웃버튼을 클릭했을 때 딕셔너리 출력
        logout.onClick.AddListener(() =>
        {
            foreach(KeyValuePair<string, int>iddic in idDic)
            {
                Debug.Log("이전까지 통과");
                StartCoroutine(AddItemCoroutine(mail, iddic.Key, iddic.Value));
                SceneManager.LoadScene("Jagabee1");
            }


        });

    }
    public void InitialCreateItems()
    {
        // 아이템 생성 (각 아이템마다 10개씩)
        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            CreateItems(itemPrefabs[i], itemLists[i]);
        }
    }

    // 아이템을 생성하고 리스트에 추가하는 함수
    private void CreateItems(GameObject itemPrefab, List<GameObject> itemList)
    {
        int numItems = 3; // 생성할 아이템의 개수를 정합니다.

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

   //딕셔너리에 기본 ID값이 넣기
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

    //아이템 클릭했을 때 아이디 매칭하고 딕셔너리의 값이 +1하는 함수
    public void GetItem(string _itemId)
    {
        //클릭한 모든 아이템들이 들어가는 리스트
        invenIdList.Add(_itemId);

        //만약에 받아오는 _itemID가 딕셔너리에 있는 키랑 같을 때 그 키의 Value값이 +1
        //개수 저장하는 코드
        if(idDic.ContainsKey(_itemId))
        {
            idDic[_itemId] += 1;
            itemText.text =  _itemId + "번 아이템" + idDic[_itemId]+"개";
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

        //같은  키값 받아올 것이고
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
