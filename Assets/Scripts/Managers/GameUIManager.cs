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
