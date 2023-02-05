using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;

namespace Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        public Image _black_screen;
        public Image _credits_image;
        public TextMeshProUGUI _game_title;
        public TextMeshProUGUI _commands_text;
        public TextMeshProUGUI _credits_text;
        public TextMeshProUGUI _authors_text;
        public Image _commands_image;
        public Button _play_button;
        public Button _back_button;
        public Button _credits_button;
        public Button _commands_button;
        public Button _exit_button;

        private void Start()
        {
            StartCoroutine(HideBlackScreen());
        }

        public void CommandsButtonClick()
        {
            _game_title.gameObject.SetActive(false);
            _credits_button.gameObject.SetActive(false);
            _commands_button.gameObject.SetActive(false);
            _exit_button.gameObject.SetActive(false);
            _game_title.gameObject.SetActive(false);
            _play_button.gameObject.SetActive(false);

            _commands_text.gameObject.SetActive(true);
            _commands_image.gameObject.SetActive(true);
            _back_button.gameObject.SetActive(true);
        }

        public void CreditsButtonClick()
        {
            _game_title.gameObject.SetActive(false);
            _credits_button.gameObject.SetActive(false);
            _commands_button.gameObject.SetActive(false);
            _exit_button.gameObject.SetActive(false);
            _game_title.gameObject.SetActive(false);
            _play_button.gameObject.SetActive(false);

            _credits_image.gameObject.SetActive(true);
            _authors_text.gameObject.SetActive(true);
            _credits_text.gameObject.SetActive(true);
            _back_button.gameObject.SetActive(true);
        }

        public void BackButtonClick()
        {
            _commands_text.gameObject.SetActive(false);
            _commands_image.gameObject.SetActive(false);
            _authors_text.gameObject.SetActive(false);
            _credits_text.gameObject.SetActive(false);
            _credits_image.gameObject.SetActive(false);
            _back_button.gameObject.SetActive(false);

            _game_title.gameObject.SetActive(true);
            _credits_button.gameObject.SetActive(true);
            _commands_button.gameObject.SetActive(true);
            _exit_button.gameObject.SetActive(true);
            _play_button.gameObject.SetActive(true);
            _game_title.gameObject.SetActive(true);
        }
        public void PlayButtonClick()
        {
            IEnumerator ShowBlackScreenAndPlay()
            {
                yield return StartCoroutine(ShowBlackScreen());
                SceneManager.LoadScene((int)SceneEnum.INTRO_SCENE);
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
