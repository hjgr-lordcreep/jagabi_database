using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text1;
    [SerializeField] private TextMeshProUGUI text2;
    [SerializeField] private TextMeshProUGUI text3;
    [SerializeField] private TextMeshProUGUI text4;
    [SerializeField] private TextMeshProUGUI text5;
    [SerializeField] private TextMeshProUGUI text6;
    [SerializeField] private TextMeshProUGUI text7;
    [SerializeField] private TextMeshProUGUI text8;
    [SerializeField] private TextMeshProUGUI text9;
    [SerializeField] private TextMeshProUGUI text10;

    ItemManager itemManager;

    private void Awake()
    {
        itemManager = FindAnyObjectByType<ItemManager>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject);
                if (hit.transform.CompareTag("Items"))
                {
                    //ItemManager의 GeiItemg함수 호출
                    itemManager.GetItem(hit.transform.GetComponent<Item>().id);
                    //itemManager.CountItem();
                    hit.transform.gameObject.SetActive(false);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            itemManager.InitialCreateItems();
        }

        //디버깅용
        if(Input.GetKeyDown(KeyCode.F10))
        {
            SceneManager.LoadScene("Jagabee3");
        }

       
    }

    
}
