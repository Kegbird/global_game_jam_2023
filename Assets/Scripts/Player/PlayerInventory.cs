using Managers;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private PickupScriptableObject[] _inventory;
    [SerializeField]
    private GameObject _inventory_panel;
    [SerializeField]
    public DialogueScriptableObject _no_space_dialogue;


    private void Awake()
    {
        _inventory = new PickupScriptableObject[Constants.INVENTORY_SLOTS];
    }

    private void Start()
    {
        _inventory_panel = GameObject.FindGameObjectWithTag(Tags.INVENTORY_TAG);
    }
    public bool CanAdd()
    {
        for (int i = 0; i < Constants.INVENTORY_SLOTS; i++)
        {
            if (_inventory[i] == null)
                return true;
        }
        return false;
    }

    public void AddPickup(PickupScriptableObject pickup)
    {
        for (int i = 0; i < Constants.INVENTORY_SLOTS; i++)
        {
            if (_inventory[i] == null)
            {
                _inventory[i] = pickup;
                return;
            }
        }
    }

    public void RemovePickup(int index)
    {
        _inventory[index] = null;
        //TODO Remove inventory
    }

    public int ContainsPickupOfType(PickupEnum pickup_type)
    {
        for (int i = 0; i < Constants.INVENTORY_SLOTS; i++)
        {
            if (_inventory[i] != null && _inventory[i]._type == pickup_type)
                return i;
        }
        return -1;
    }
}
