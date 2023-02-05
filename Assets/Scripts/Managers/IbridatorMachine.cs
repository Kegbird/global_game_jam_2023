using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class IbridatorMachine : MonoBehaviour
{
    public PickupScriptableObject pickupScriptableObject0;

    public PickupScriptableObject pickupScriptableObject1;

    public Sprite _seed_placeholder;

    public GameObject _ibridation_panel;

    public Button _ibridate_btn;

    private HybridationManager hybridationManager;

    [SerializeField]
    private Drag _drag_0;
    [SerializeField]
    private Drag _drag_1;
    [SerializeField]
    private Drag _drag_2;
    [SerializeField]
    private Drag _drag_3;
    [SerializeField]
    private Drag _drag_4;

    public bool is_used;

    [SerializeField]
    public List<PickupScriptableObject> _pickups;

    private void Start()
    {
        hybridationManager = new HybridationManager();
    }

    public void ShowIbridator()
    {
        _ibridation_panel.SetActive(true);
    }

    public void HideIbridator()
    {
        pickupScriptableObject0 = null;
        pickupScriptableObject1 = null;
        GameObject player = GameObject.FindWithTag(Tags.PLAYER_TAG);
        PlayerInventory _inventory = player.GetComponent<PlayerInventory>();
        _inventory.Refresh();
        _drag_0.ResetDrag();
        _drag_1.ResetDrag();
        _drag_2.ResetDrag();
        _drag_3.ResetDrag();
        _drag_4.ResetDrag();
        _ibridation_panel.SetActive(false);
        is_used = false;
        PlayerController _player_controller = player.GetComponent<PlayerController>();
        _player_controller.EnableMovement();
    }

    public void StartIbridation()
    {
        GameObject player = GameObject.FindWithTag(Tags.PLAYER_TAG);
        PlayerInventory _inventory = player.GetComponent<PlayerInventory>();
        GameUIManager _game_ui_manager = GameObject.FindWithTag(Tags.LOGIC_TAG).GetComponent<GameUIManager>();
        PickupEnum? hibrid = hybridationManager.GetHybridation(pickupScriptableObject0._type, pickupScriptableObject1._type);
        int index = _inventory.AddPickup(_pickups[(int)hibrid]);
        _game_ui_manager.SetInventorySpriteAtIndex(index, _pickups[(int)hibrid]._dex_sprite);

        index = _inventory.GetFirstOcc(pickupScriptableObject0);
        _inventory.RemovePickup(index);
        _game_ui_manager.RemoveInventoryItem(index);
        index = _inventory.GetFirstOcc(pickupScriptableObject1);
        _inventory.RemovePickup(index);
        _game_ui_manager.RemoveInventoryItem(index);
        HideIbridator();
    }

    public void Interact()
    {
        if (is_used)
            return;
        is_used = true;
        ShowIbridator();
    }

    private void Update()
    {
        if(is_used && Input.GetKeyDown(KeyCode.Escape))
            HideIbridator();
    }

    public void SetPickup(PickupScriptableObject pickupScriptable)
    {
        if (pickupScriptableObject0 == null)
            pickupScriptableObject0 = pickupScriptable;
        else
        {
            pickupScriptableObject1 = pickupScriptable;
            if (hybridationManager.GetHybridation(pickupScriptableObject0._type, pickupScriptableObject1._type) != null)
                _ibridate_btn.interactable = true;
        }
    }

    public void SetSeedType2(PickupScriptableObject pickupScriptable)
    {
        pickupScriptableObject1 = pickupScriptable;
        if (pickupScriptableObject1 != null && hybridationManager.GetHybridation(pickupScriptableObject0._type, pickupScriptableObject1._type) != null)
            _ibridate_btn.interactable = true;
    }

    public PickupEnum? Ibridate()
    {
        _ibridate_btn.interactable = false;
        PickupEnum? result = hybridationManager.GetHybridation(pickupScriptableObject0._type , pickupScriptableObject1._type);
        return result;
    }
}
