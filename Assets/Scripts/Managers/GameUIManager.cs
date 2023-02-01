using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField]
    private Image _black_screen;

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
