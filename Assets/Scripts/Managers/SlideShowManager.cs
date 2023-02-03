using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;
using Color = UnityEngine.Color;

public enum SceneEnum
{
    MAIN_MENU_SCENE,
    INTRO_SCENE,
    GAME_SCENE,
    BAD_ENDING_SCENE,
    GOOD_ENDING_SCENE
}

public class SlideShowManager : MonoBehaviour
{
    [SerializeField]
    private Image _black_screen;
    [SerializeField]
    private Image _image;
    [SerializeField]
    private TextMeshProUGUI _sentence_text;
    [SerializeField]
    private List<string> _sentences;
    [SerializeField]
    private List<Sprite> _images;
    [SerializeField]
    private List<int> _sentences_per_image;
    [SerializeField]
    private bool _no_image_intro;
    [SerializeField]
    [Range(1f, 10f)]
    private float _letters_per_second;
    [SerializeField]
    private AudioSource _audio_source;
    [SerializeField]
    public SceneEnum _scene;

    private void Awake()
    {
        _audio_source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(IntroCoroutine());
    }

    private IEnumerator IntroCoroutine()
    {
        string sentence;
        float text_speed = 1f / _letters_per_second;
        _audio_source.Play();
        yield return StartCoroutine(RaiseVolume());

        if(_no_image_intro)
        {
            _image.color = new Color(0, 0, 0, 1f);
            yield return StartCoroutine(HideBlackScreen());
            for (int i = 0; i < _sentences.Count; i++)
            {
                sentence = _sentences[i];
                yield return StartCoroutine(SetSentenceText(sentence, text_speed));
                yield return new WaitForSeconds(2f);
            }
            yield return StartCoroutine(ShowBlackScreen());
        }
        else
        {
            int k = 0;
            for (int i = 0; i < _images.Count; i++)
            {
                _image.sprite = _images[i];
                yield return StartCoroutine(HideBlackScreen());
                for (int j=0; j < _sentences_per_image[i]; j++)
                {
                    sentence = _sentences[k];
                    yield return StartCoroutine(SetSentenceText(sentence, text_speed));
                    yield return new WaitForSeconds(2f);
                    k++;
                }
                yield return StartCoroutine(ShowBlackScreen());
                _sentence_text.text = "";
            }
        }
        yield return StartCoroutine(DecreaseVolume());
        SceneManager.LoadScene((int)_scene);
        yield return null;
    }

    private IEnumerator SetSentenceText(string sentence, float text_speed)
    {
        for (int k = 0; k <= sentence.Length; k++)
        {
            _sentence_text.SetText(sentence.Substring(0, k));
            yield return new WaitForSeconds(text_speed);
        }
    }

    private IEnumerator HideBlackScreen()
    {
        _black_screen.raycastTarget = true;
        for (float i = 2f; i >= 0; i -= Time.deltaTime)
        {
            _black_screen.color = new Color(0, 0, 0, i / 2f);
            yield return new WaitForEndOfFrame();
        }
        _black_screen.raycastTarget = false;
    }

    private IEnumerator ShowBlackScreen()
    {
        _black_screen.raycastTarget = true;
        for (float i = 0; i <= 2f; i += Time.deltaTime)
        {
            _black_screen.color = new Color(0, 0, 0, i / 2f);
            yield return new WaitForEndOfFrame();
        }
        _black_screen.raycastTarget = false;
    }

    private IEnumerator RaiseVolume()
    {
        float step = 0.1f;
        for (int i = 0; i <= 10f; i++)
        {
            _audio_source.volume += step;
            yield return new WaitForSeconds(step);
        }
    }
    
    private IEnumerator DecreaseVolume()
    {
        float step = 0.1f;
        for (int i = 0; i <= 10f; i++)
        {
            _audio_source.volume -= step;
            yield return new WaitForSeconds(step);
        }
    }
}
