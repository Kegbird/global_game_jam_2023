using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField]
    private Image _black_screen;

    public Image _oxygen_bar;
    public Image _energy_bar;
    public Image _water_bar;

    public Image[] _inventory_images = new Image[5];
    public Sprite _sprite_test;

    public void SetInventorySpriteAtIndex(int _index, Sprite _pickup_sprite)
    {   
        Color _color = _inventory_images[_index].color;
        _inventory_images[_index] = _inventory_images[_index].GetComponent<Image>();
        _color = _inventory_images[_index].color;
        _color.a = 0;
        _inventory_images[_index].color = _color;
        _inventory_images[_index].sprite = _pickup_sprite;
        _color.a = 1;
        _inventory_images[_index].color = _color;
    }

    public void RemoveInventoryItem(int _index, Sprite pickup_sprite)
    {
        Color _color = _inventory_images[_index].color;
        _color.a = 0;
        _inventory_images[_index].color = _color;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SetInventorySpriteAtIndex(2, _sprite_test);
        }
    }
    
    public void OxygenLevel(float _oxygen_perc)
    {
        _oxygen_bar.fillAmount = _oxygen_perc;
    }

    public void EnergyLevel(float _energy_perc)
    {
        _energy_bar.fillAmount = _energy_perc;
    }

    public void WaterLevel(float _water_perc)
    {
        _water_bar.fillAmount = _water_perc;
    }

    private void Start()
    {
        StartCoroutine(HideBlackScreen());
    }

    public IEnumerator HideBlackScreen()
    {
        _black_screen.raycastTarget = true;
        for (float i = 1f; i >= 0; i -= Time.deltaTime)
        {
            _black_screen.color = new Color(0, 0, 0, i / 1f);
            yield return new WaitForEndOfFrame();
        }
        _black_screen.raycastTarget = false;
    }

    public IEnumerator ShowBlackScreen()
    {
        _black_screen.raycastTarget = true;
        for (float i = 0; i <= 1f; i += Time.deltaTime)
        {
            _black_screen.color = new Color(0, 0, 0, i / 1f);
            yield return new WaitForEndOfFrame();
        }
        _black_screen.raycastTarget = false;
    }
}
