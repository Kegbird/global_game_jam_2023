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

    public Sprite _blank_inventory_space;

    public Image[] _inventory_images = new Image[5];

    private void Start()
    {
        for (int i = 0; i < _inventory_images.Length; i++)
            _inventory_images[i].sprite = _blank_inventory_space;
        StartCoroutine(HideBlackScreen());
    }

    public void SetInventorySpriteAtIndex(int _index, Sprite _pickup_sprite)
    {
        _inventory_images[_index].sprite = _pickup_sprite;
    }

    public void RemoveInventoryItem(int _index)
    {
        _inventory_images[_index].sprite = _blank_inventory_space;
    }
    
    public void SetOxygenLevel(float _oxygen_perc)
    {
        _oxygen_bar.fillAmount = _oxygen_perc;
    }

    public void SetEnergyLevel(float _energy_perc)
    {
        _energy_bar.fillAmount = _energy_perc;
    }

    public void SetWaterLevel(float _water_perc)
    {
        _water_bar.fillAmount = _water_perc;
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
