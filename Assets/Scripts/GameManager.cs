using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 아이템 매니저를 참조하기 위한 변수
    public ItemManager itemManager;

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
                if(hit.transform.CompareTag("Items"))
                    hit.transform.gameObject.SetActive(false);
            }
        }

        //디버깅용
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Jagabee3");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            itemManager.InitializeItems();
        }
    }
}
