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

    private void Start()
    {
        SpawnItems();
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

                Item.ItemInfo itemInfo = hit.transform.GetComponent<Item>().Info;
                SpawnInvenItem(itemInfo);

                itemList.Remove(hit.transform.gameObject);
                Destroy(hit.transform.gameObject);

                if (hit.transform.CompareTag("Items"))
                    hit.transform.gameObject.SetActive(false);
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnItems();
            itemManager.InitialCreateItems();
        }

        //µð¹ö±ë¿ë
        if(Input.GetKeyDown(KeyCode.F10))
        {
            SceneManager.LoadScene("Jagabee3");
        }
    }


    private void SpawnInvenItem(
        Item.ItemInfo _itemInfo)
    {
        GameObject invenItemGo =
            Instantiate(invenItemPrefab);
        invenItemGo.transform.SetParent(invenContentTr);

        InventoryItem invenItem =
            invenItemGo.GetComponent<InventoryItem>();
        invenItem.Init(_itemInfo);
    }


    private void SpawnItems()
    {
        StartCoroutine(SpawnItemsCoroutine());
    }

    private IEnumerator SpawnItemsCoroutine()
    {

            GameObject itemGo =
                Instantiate(
                    itemPrefab,
                    Vector3.up,
                    Quaternion.identity);
            itemList.Add(itemGo);

            yield return new WaitForSeconds(0.1f);

    }

}
