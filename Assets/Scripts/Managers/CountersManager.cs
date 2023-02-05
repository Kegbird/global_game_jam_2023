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
        private float _water_level;
        [SerializeField]
        private float _energy_level;
        [SerializeField]
        private float _oxygen_decrement_step;
        [SerializeField]
        private GameUIManager _game_ui_manager;

        private IEnumerator _oxygen_coroutine;
        private IEnumerator _water_coroutine;
        private IEnumerator _energy_coroutine;

        private void Awake()
        {
        }

        private void Start()
        {
            _game_ui_manager = GameObject.FindWithTag(Tags.LOGIC_TAG).GetComponent<GameUIManager>();
            _oxygen_level = Constants.DEFAULT_OXYGEN_LEVEL;
            _water_level = Constants.DEFAULT_WATER_LEVEL;
            _energy_level = Constants.DEFAULT_ENERGY_LEVEL;
            _water_coroutine = WaterCounter();
            _energy_coroutine = EnergyCounter();
            _game_ui_manager.SetOxygenLevel(1f);
            _game_ui_manager.SetWaterLevel(_water_level / Constants.MAX_WATER_LEVEL);
            _game_ui_manager.SetEnergyLevel(_energy_level / Constants.MAX_ENERGY_LEVEL);
            StartCoroutine(_water_coroutine);
            StartCoroutine(_energy_coroutine);
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

        public void StopEnergyCounter()
        {
            StopCoroutine(_energy_coroutine);
        }

        public void StopWaterCounter()
        {
            StopCoroutine(WaterCounter());
        }

        private IEnumerator OxygenCounter()
        {
            _oxygen_level = Constants.DEFAULT_OXYGEN_LEVEL;
            _oxygen_decrement_step = Constants.DEFAULT_OXYGEN_DECREMENT_STEP;
            do
            {
                yield return new WaitForSeconds(Constants.OXYGEN_DECREMENT_DELAY);
                _oxygen_level -= _oxygen_decrement_step;
                _game_ui_manager.SetOxygenLevel(_oxygen_level / Constants.MAX_OXYGEN_LEVEL);
            }
            while (_oxygen_level > 0);

            yield return StartCoroutine(GameOverAndDie());
        }

        private IEnumerator EnergyCounter()
        {
            do
            {
                yield return new WaitForSeconds(Constants.ENERGY_DECREMENT_DELAY);
                _energy_level -= Constants.DEFAULT_ENERGY_DECREMENT_STEP;
                _game_ui_manager.SetEnergyLevel(_energy_level/Constants.MAX_ENERGY_LEVEL);
            }
            while (_energy_level > 0);

            yield return StartCoroutine(GameOver());
        }

        public IEnumerator WaterCounter()
        {
            _water_level = Constants.DEFAULT_WATER_LEVEL;
            do
            {
                yield return new WaitForSeconds(Constants.ENERGY_DECREMENT_DELAY);
                _water_level -= Constants.DEFAULT_WATER_DECREMENT_STEP;
                _game_ui_manager.SetWaterLevel(_water_level/Constants.MAX_WATER_LEVEL);
            }
            while (_water_level > 0);

            yield return StartCoroutine(GameOver());
        }
        
        private IEnumerator GameOverAndDie()
        {
            GameObject player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
            player.GetComponent<PlayerController>().Die();

            GameUIManager game_ui_manager = GameObject.Find(Tags.LOGIC_TAG).GetComponent<GameUIManager>();
            yield return StartCoroutine(game_ui_manager.ShowBlackScreen());
            SceneManager.LoadScene((int)SceneEnum.BAD_ENDING_SCENE);
        }

        private IEnumerator GameOver()
        {
            GameObject player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
            player.GetComponent<PlayerController>().DisableMovement();

            GameUIManager game_ui_manager = GameObject.Find(Tags.LOGIC_TAG).GetComponent<GameUIManager>();
            yield return StartCoroutine(game_ui_manager.ShowBlackScreen());
            SceneManager.LoadScene((int)SceneEnum.BAD_ENDING_SCENE);
        }

        public void IncreaseOxygenDecrementStep(float increment)
        {
            _oxygen_decrement_step += increment;
        }

        public void DecreaseOxygenDecrementStep(float decrement)
        {
            _oxygen_decrement_step -= decrement;
        }

        public void IncreaseWaterLevel()
        {
            _water_level += Constants.WATER_PICKUP_INCREMENT;
        }

        public void IncreaseEnergyLevel()
        {
            _energy_level += Constants.ENERGY_PICKUP_INCREMENT;
        }
    }
}
