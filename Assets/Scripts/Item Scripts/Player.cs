using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _itemIndex = 0;

    private List<ItemBase> _inventory = new List<ItemBase>();

    public List<ItemBase> Inventory { get { return _inventory; } }

    [Header("Debug These do NOTHING they just tell")]
    public bool hasdisk;
    public bool hasboard;
    public bool hasbatt;
    public bool hassheild;
    public bool haswhip;
    public bool hasdash;
    public bool hasboots;

    [Header("Quick Item Switch")]
    public bool ScrollWheelSwitch;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _inventory[_itemIndex].UseItem(gameObject);
        }
        CheckItemButton();
    }

    private void CheckItemButton()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _inventory[_itemIndex].DeActivateObject(gameObject);
            _itemIndex++;
            if(_itemIndex > _inventory.Count-1)
            {
                _itemIndex = 0;
            }
            _inventory[_itemIndex].ActivateObject(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _inventory[_itemIndex].DeActivateObject(gameObject);
            _itemIndex--;
            if (_itemIndex < 0)
            {
                _itemIndex = _inventory.Count-1;
            }
            _inventory[_itemIndex].ActivateObject(gameObject);
        }

        if (ScrollWheelSwitch && Input.GetAxis ("Mouse ScrollWheel") > 0)
        {
            _inventory[_itemIndex].DeActivateObject(gameObject);
            _itemIndex++;
            if (_itemIndex > _inventory.Count - 1)
            {
                _itemIndex = 0;
            }
            _inventory[_itemIndex].ActivateObject(gameObject);
        }

        if (ScrollWheelSwitch && Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            _inventory[_itemIndex].DeActivateObject(gameObject);
            _itemIndex--;
            if (_itemIndex < 0)
            {
                _itemIndex = _inventory.Count - 1;
            }
            _inventory[_itemIndex].ActivateObject(gameObject);
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(new InventoryToken(this));
    }

    public void LoadPlayer()
    {
        InventoryToken data = SaveSystem.LoadPlayer();

        _inventory = data.ItemList_;
        
    }
}
