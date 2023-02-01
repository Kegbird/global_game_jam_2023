using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;

namespace Managers
{
    public class CountersManager : MonoBehaviour
    {
        [SerializeField]
        private float _oxygen_level;
        [SerializeField]
        private bool _game_over;
        [SerializeField]
        private float _oxygen_decrement_step;
        private IEnumerator _oxygen_coroutine;

        private void Awake()
        {
            _oxygen_level = Constants.DEFAULT_OXYGEN_LEVEL;
            _oxygen_decrement_step = Constants.DEFAULT_OXYGEN_DECREMENT_STEP;
        }

        private void Start()
        {
        }

        public void StartOxygenCounter()
        {
            _oxygen_coroutine = OxygenCounter();
            StartCoroutine(_oxygen_coroutine);
        }

        public void StopOxygenCounter()
        {
            StopCoroutine(_oxygen_coroutine);
        }

        private IEnumerator OxygenCounter()
        {
            _oxygen_level = Constants.DEFAULT_OXYGEN_LEVEL;
            do
            {
                yield return new WaitForSeconds(Constants.OXYGEN_DECREMENT_DELAY);
                _oxygen_level -= _oxygen_decrement_step;
            }
            while (_oxygen_level > 0);
            _game_over = true;

            yield return StartCoroutine(GameOver());
        }

        private IEnumerator GameOver()
        {
            GameObject player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
            player.GetComponent<PlayerController>().Die();

            GameUIManager game_ui_manager = GameObject.Find(Tags.LOGIC_TAG).GetComponent<GameUIManager>();
            yield return StartCoroutine(game_ui_manager.ShowBlackScreen());
            SceneManager.LoadScene(Constants.BAD_ENDING_SCENE_INDEX);
        }

        public void IncreaseDecrementStep(float increment)
        {
            _oxygen_decrement_step += increment;
        }
    }
}
