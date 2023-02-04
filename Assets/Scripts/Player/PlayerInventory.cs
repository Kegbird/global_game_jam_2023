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
    [SerializeField]
    private int index = 0;


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

    public int AddPickup(PickupScriptableObject pickup)
    {
        for (int i = 0; i < Constants.INVENTORY_SLOTS; i++)
        {
            if (_inventory[i] == null)
            {
                _inventory[i] = pickup;
                return i;
            }
        }
        return -1;
    }

    public PickupScriptableObject GetPickupAtIndex(int index)
    {
        return _inventory[index];
    }

    public void RemovePickup(int index)
    {
        _inventory[index] = null;
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

    public bool IsSeedSelected()
    {
        if (_inventory[index] == null)
            return false;
        PickupEnum pickupEnum = _inventory[index]._type;
        if (pickupEnum == PickupEnum.ENERGY || pickupEnum == PickupEnum.WATER)
            return false;
        return true;
    }

    public PickupEnum GetSelectedSeed()
    {
        return _inventory[index]._type;
    }

    public int GetSelectedIndex()
    {
        return index;
    }

    public void ResetCell()
    {
        GameUIManager _game_ui_manager = GameObject.FindWithTag(Tags.LOGIC_TAG).GetComponent<GameUIManager>();
        _game_ui_manager.UnHighlight(index);
        index = 0;
    }

    public void HighlightCell()
    {
        GameUIManager _game_ui_manager = GameObject.FindWithTag(Tags.LOGIC_TAG).GetComponent<GameUIManager>();
        _game_ui_manager.Highlight(index);
    }


    public void Right()
    {
        GameUIManager _game_ui_manager = GameObject.FindWithTag(Tags.LOGIC_TAG).GetComponent<GameUIManager>();
        _game_ui_manager.UnHighlight(index);
        index++;
        index = Mathf.Clamp(index, 0, 4);
    }

    public void Left()
    {
        GameUIManager _game_ui_manager = GameObject.FindWithTag(Tags.LOGIC_TAG).GetComponent<GameUIManager>();
        _game_ui_manager.UnHighlight(index);
        index--;
        index = Mathf.Clamp(index, 0, 4);
    }
}
