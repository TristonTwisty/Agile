using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : MonoBehaviour
{

    [SerializeField] private int _itemIndex = 0;

    [SerializeField] public List<ItemBase> _inventory = new List<ItemBase>();

    [SerializeField] public List<ItemBase> Inventory { get { return _inventory; } }

    public Vector3 xyz;
    public Quaternion xyzOri;

    public bool Startwith;
    [Header("Debug These do NOTHING they just tell")]
    public bool hasdisk;
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
            if (hasbatt) { Inventory.Add(new BattItem()); }
            if (hassheild) { Inventory.Add(new SheildItem()); }
            if (haswhip) { Inventory.Add(new WhipItem()); }
            if (hasdash) { Inventory.Add(new DashItem()); }
        }
        //Debug.Log(Inventory.Count);

        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_inventory.Count != 0)
            {
                _inventory[_itemIndex].UseItem(gameObject);
            }
            else
            {
                Debug.Log("nothing here");
            }
        }
        CheckItemButton();

        if (Input.GetKeyDown(KeyCode.F1))
        {
            SavePlayer();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            LoadPlayer();
        }

        if (PlayerRefs.instance.PlayerHealth <= 0)
        {
            _inventory.Clear();
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
        if (e.isKey && _inventory != null)
        {
            //Debug.Log(e);
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i].PressSelectKey(e.keyCode))
                {
                    if (_itemIndex != i || _inventory[_itemIndex].ReturnGO().activeInHierarchy == false)
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
        Debug.Log("Calling Load and clearing items");

        InventoryToken data = SaveSystem.LoadPlayer();

        /*_inventory = new List<ItemBase>();
        Debug.Log("adding items back");
        _inventory.Add(data.Item1);
        _inventory.Add(data.Item2);
        _inventory.Add(data.Item3);*/

        _inventory = data.ItemsInInventory;
        PlayerRefs.instance.currentHealth = data.PlayerHealth_;

        xyz.x = data.x;
        xyz.y = data.y;
        xyz.z = data.z;

        xyzOri.x = data.xor;
        xyzOri.y = data.yor;
        xyzOri.z = data.zor;


        Prefs.Player.transform.position = xyz;
        Prefs.Player.transform.rotation = xyzOri;
    }

    public void TakeDamage(float DamageTaken)
    {
        PlayerRefs.instance.currentHealth -= DamageTaken;
        Scriptforui.instance.playerHealthSlider.value -= DamageTaken;
    }

    public void HealPlayer(float HealthReceived)
    {
        PlayerRefs.instance.currentHealth += HealthReceived;
        Scriptforui.instance.playerHealthSlider.value += HealthReceived;
    }
}
