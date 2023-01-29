using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Managers
{
    public class ExplorationManager : MonoBehaviour
    {
        [SerializeField]
        private float _oxygen_level;
        [SerializeField]
        private bool _game_over;
        [SerializeField]
        private float _oxygen_decrement_step;

        private void Awake()
        {
            _oxygen_level = Constants.DEFAULT_OXYGEN_LEVEL;
        }

        private void Start()
        {
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
            yield return null;
        }

        public void IncreaseDecrementStep(float increment)
        {
            _oxygen_decrement_step += increment;
        }

        private IEnumerator RandomizeItemsOverMap()
        {
            yield return null;
        }
    }
}
