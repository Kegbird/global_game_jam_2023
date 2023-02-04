using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class IbridatorMachine : MonoBehaviour
{
    public PickupEnum? _seed_type_1;

    public PickupEnum? _seed_type_2;

    public Image _seed_sprite_1;

    public Image _seed_sprite_2;
    public Sprite _seed_placeholder;

    public GameObject _ibridate_btn;

    private HybridationManager hybridationManager;

    private void Start()
    {
        hybridationManager = new HybridationManager();
    }

    public void ShowIbridator()
    {

    }

    public void HideIbridator()
    {

    }

    public void StartIbridation()
    {

    }

    public void SetSeedSprite1(Sprite sprite)
    {
        _seed_sprite_1.sprite = sprite;
    }

    public void SetSeedSprite2(Sprite sprite)
    {
        _seed_sprite_2.sprite = sprite;
    }

    public void SetSeedType1(PickupEnum _seed_type)
    {
        _seed_type_1 = _seed_type;


    }

    public void SetSeedType2(PickupEnum _seed_type)
    {
        _seed_type_2 = _seed_type;
    }

    public PickupEnum? Ibridate()
    {
        PickupEnum? result = hybridationManager.GetHybridation((PickupEnum)_seed_type_1, (PickupEnum)_seed_type_2);
        _seed_type_1 = null;
        _seed_type_2 = null;
        _seed_sprite_1.sprite = _seed_placeholder;
        _seed_sprite_2.sprite = _seed_placeholder;
        return result;
    }
}
