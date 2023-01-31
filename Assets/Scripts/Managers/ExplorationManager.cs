using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;

namespace Managers
{
    public class ExplorationManager : MonoBehaviour
    {
        [SerializeField]
        private Image _black_screen;
        [SerializeField]
        private float _oxygen_level;
        [SerializeField]
        private bool _game_over;
        [SerializeField]
        private float _oxygen_decrement_step;

        private void Awake()
        {
            _oxygen_level = Constants.DEFAULT_OXYGEN_LEVEL;
            _oxygen_decrement_step = Constants.DEFAULT_OXYGEN_DECREMENT_STEP;
        }

        private void Start()
        {
            StartCoroutine(HideBlackScreen());
            StartCoroutine(RandomizeItemsOverMap());
            StartCoroutine(BreathOxigen());   
        }

        private IEnumerator BreathOxigen()
        {
            do
            {
                _oxygen_level -= _oxygen_decrement_step;
                yield return new WaitForSeconds(Constants.OXYGEN_DECREMENT_DELAY);
            }
            while (_oxygen_level > 0);
            _game_over = true;

            yield return StartCoroutine(GameOver());
        }

        private IEnumerator GameOver()
        {
            yield return StartCoroutine(ShowBlackScreen());
            SceneManager.LoadScene(Constants.BAD_ENDING_SCENE_INDEX);
        }

        public void IncreaseDecrementStep(float increment)
        {
            _oxygen_decrement_step += increment;
        }

        private IEnumerator RandomizeItemsOverMap()
        {
            yield return null;
        }

        private IEnumerator HideBlackScreen()
        {
            _black_screen.raycastTarget = true;
            for (float i = 1f; i >= 0; i -= Time.deltaTime)
            {
                _black_screen.color = new Color(0, 0, 0, i / 1f);
                yield return new WaitForEndOfFrame();
            }
            _black_screen.raycastTarget = false;
        }

        private IEnumerator ShowBlackScreen()
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
}
