using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;

namespace Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        public Image _black_screen;
        public Text _game_title;
        public Text _commands_title;
        public Text _credits_title;
        public Button _back_button;

        private void Start()
        {
            StartCoroutine(HideBlackScreen());
        }

        public void CommandsButtonClick()
        {
            GameObject[] _main_buttons = GameObject.FindGameObjectsWithTag("MainButton");
            //Debug.Log(_main_buttons.Length);
            foreach (GameObject _item in _main_buttons)
            {
                _item.SetActive(false);
            }

            _game_title.gameObject.SetActive(false);
            _commands_title.gameObject.SetActive(true);
            _back_button.gameObject.SetActive(true);


        }

        public void CreditsButtonClick()
        {
            GameObject[] _main_buttons = GameObject.FindGameObjectsWithTag("MainButton");
            //Debug.Log(_main_buttons.Length);
            foreach (GameObject _item in _main_buttons)
            {
                _item.SetActive(false);
            }

            _game_title.gameObject.SetActive(false);
            _credits_title.gameObject.SetActive(true);
            _back_button.gameObject.SetActive(true);
        }

        public void BackButtonClick()
        {
            _game_title.gameObject.SetActive(true);
            _commands_title.gameObject.SetActive(false);
            _credits_title.gameObject.SetActive(false);
            _back_button.gameObject.SetActive(false);


            GameObject[] _main_buttons = GameObject.FindGameObjectsWithTag("MainButton");
            //Debug.Log(_main_buttons.Length);
            foreach (GameObject _item in _main_buttons)
            {
                _item.SetActive(true);
            }


        }
        public void PlayButtonClick()
        {
            IEnumerator ShowBlackScreenAndPlay()
            {
                yield return StartCoroutine(ShowBlackScreen());
                SceneManager.LoadScene(Constants.INTRO_SCENE_INDEX);
            }
            StartCoroutine(ShowBlackScreenAndPlay());
        }

        public void ExitButtonClick()
        {
            IEnumerator ShowBlackScreenAndQuit()
            {
                yield return StartCoroutine(ShowBlackScreen());
                Application.Quit();
            }
            StartCoroutine(ShowBlackScreenAndQuit());
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
