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

        private void Start()
        {
            StartCoroutine(HideBlackScreen());
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
