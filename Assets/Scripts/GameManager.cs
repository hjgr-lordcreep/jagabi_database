using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject itemPrefab = null;
    [SerializeField]
    private Transform invenContentTr = null;
    [SerializeField]
    private GameObject invenItemPrefab = null;
    
    private List<GameObject> itemList =  new List<GameObject>();


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
                //Debug.Log(hit.transform.gameObject);


                if (hit.transform.CompareTag("Items"))
                {
                    Item itemInfo = hit.transform.GetComponent<Item>();
                    SpawnInvenItem(itemInfo);

                    itemList.Remove(hit.transform.gameObject);
                    Destroy(hit.transform.gameObject);
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


    private void SpawnInvenItem(
        Item _itemInfo)
    {
        GameObject invenItemGo =
            Instantiate(invenItemPrefab);
        invenItemGo.transform.SetParent(invenContentTr);

        InventoryItem invenItem =
            invenItemGo.GetComponent<InventoryItem>();
        invenItem.Init(_itemInfo);
    }
}
