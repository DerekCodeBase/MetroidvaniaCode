using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
/*public InventoryObject inventory;

public int _xStart;
public int _yStart;
public int _xSpace;
public int _numberColumns;
public int _ySpace;
public GameObject _player;
Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
   
    void Start()
    {
        CreateDisplay();    
    }

    void Update()
    {
        UpdateDisplay();        
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            obj.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            itemsDisplayed.Add(inventory.Container[i], obj);
            
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(_xStart + (_xSpace *(i % _numberColumns)), _yStart + (-_ySpace * (i/_numberColumns)), 0f);
    }

    public void UpdateDisplay()
    {
        if(_player.GetComponent<enableDisable>()._display)
        {
        for ( int i = 0; i < inventory.Container.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                itemsDisplayed[inventory.Container[i]].SetActive(true);
            }
            else
            {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            itemsDisplayed.Add(inventory.Container[i], obj);
            }
        }
        }
    }
*/
}
