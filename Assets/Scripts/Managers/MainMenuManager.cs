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
        public Canvas _commands_canvas;//andrea
        public Canvas _credits_canvas;//
        public Canvas _canvas; //
        private string _showed_canvas; //

        private void Start()
        {
            StartCoroutine(HideBlackScreen());
            _commands_canvas.gameObject.SetActive(false); //
            _credits_canvas.gameObject.SetActive(false); //
        }

        public void CommandsButtonClick() //
        {
            _canvas.gameObject.SetActive(false);
            _commands_canvas.gameObject.SetActive(true);
        }

        public void CreditsButtonClick() //
        {
            _canvas.gameObject.SetActive(false);
            _credits_canvas.gameObject.SetActive(true);
        }

        public void BackButtonClick() //
        {
            _canvas.gameObject.SetActive(true);
            _commands_canvas.gameObject.SetActive(false);
            _credits_canvas.gameObject.SetActive(false);
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
