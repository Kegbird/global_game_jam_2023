using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class IbridatorMachine : MonoBehaviour
{
    public PickupScriptableObject pickupScriptableObject0;

    public PickupScriptableObject pickupScriptableObject1;

    public Image _seed_sprite_1;

    public Image _seed_sprite_2;
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

    public void SetSeedSprite1(Sprite sprite)
    {
        _seed_sprite_1.sprite = sprite;
    }

    public void SetSeedSprite2(Sprite sprite)
    {
        _seed_sprite_2.sprite = sprite;
    }

    public void SetSeedType1(PickupScriptableObject pickupScriptable)
    {
        pickupScriptableObject0 = pickupScriptable;
        if(pickupScriptableObject1 != null && hybridationManager.GetHybridation(pickupScriptableObject0._type, pickupScriptableObject1._type)!=null)
            _ibridate_btn.interactable = true;
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
        _seed_sprite_1.sprite = _seed_placeholder;
        _seed_sprite_2.sprite = _seed_placeholder;
        return result;
    }
}
