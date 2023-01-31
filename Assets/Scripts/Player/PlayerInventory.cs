using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private List<PickupScriptableObject> _inventory;
    [SerializeField]
    private GameObject _inventory_panel;


    private void Awake()
    {
        _inventory = new List<PickupScriptableObject>();
    }

    private void Start()
    {
        _inventory_panel = GameObject.FindGameObjectWithTag(Tags.INVENTORY_TAG);
    }

    public void AddPickup(PickupScriptableObject pickup)
    {
        _inventory.Add(pickup);
        //todo fill inventory slot with sprite and name
    }

    private void RemovePickup(int index)
    {
        if (_inventory.Count <= index)
            return;
        _inventory.RemoveAt(index);
    }
}
