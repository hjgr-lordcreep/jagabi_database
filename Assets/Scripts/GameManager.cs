using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

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
                    //ItemManager�� GeiItemg�Լ� ȣ��
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

        //������
        if(Input.GetKeyDown(KeyCode.F10))
        {
            SceneManager.LoadScene("Jagabee3");
        }

       
    }

    
}
