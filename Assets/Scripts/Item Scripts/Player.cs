using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _itemIndex = 0;

    private List<ItemBase> _inventory = new List<ItemBase>();

    public List<ItemBase> Inventory { get { return _inventory; } }

    public Vector3 xyz;

    public bool Startwith;
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

    public PlayerRefs Prefs;

    private void Start()
    {
        Prefs = gameObject.GetComponent<PlayerRefs>();

        if (Startwith)
        {
            if (hasdisk) { Inventory.Add(new DiskItem()); }
            if (hasboard) { Inventory.Add(new BoardItem()); }
            if (hasbatt) { Inventory.Add(new BattItem()); }
            if (hassheild) { Inventory.Add(new SheildItem()); }
            if (haswhip) { Inventory.Add(new WhipItem()); }
            if (hasdash) { Inventory.Add(new DashItem()); }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _inventory[_itemIndex].UseItem(gameObject);
        }
        CheckItemButton();

        if (Input.GetKeyDown(KeyCode.F1))
        {
            LoadPlayer();
        }
    }

    private void CheckItemButton()
    {
        /*if (Input.GetKeyDown(KeyCode.E))
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
        }*/

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

    private void OnGUI()
    {
        Event e = Event.current;
        //Debug.Log(e);
        if (e.isKey)
        {
            //Debug.Log(e);
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i].PressSelectKey(e.keyCode))
                {
                    if (_itemIndex != i)
                    {
                        _inventory[_itemIndex].DeActivateObject(gameObject);
                        _itemIndex = i;
                        _inventory[_itemIndex].ActivateObject(gameObject);
                    }

                }
            }
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
        xyz.x = data.x;
        xyz.y = data.y;
        xyz.z = data.z;
        Prefs.Player.transform.position = xyz;
    }
}
