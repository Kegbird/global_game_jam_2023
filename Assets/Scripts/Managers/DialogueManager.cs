using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private Image _next_line_key;
    [SerializeField]
    private Image _dialogue_box;
    [SerializeField]
    private TextMeshProUGUI _dialogue_text;
    [SerializeField]
    private float _letters_per_second;
    [SerializeField]
    private bool _reading;
    [SerializeField]
    private PlayerController _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).GetComponent<PlayerController>();
    }

    public void StartDialogue(DialogueScriptableObject dialogue)
    {
        if (_reading)
            return;

        StartCoroutine(ReadDialogue(dialogue));
    }

    public IEnumerator ReadDialogue(DialogueScriptableObject dialogue)
    {
        _dialogue_box.gameObject.SetActive(true);
        _reading = true;
        for (float i = 0; i <= 0.5f; i += Time.deltaTime)
        {
            _dialogue_box.color = new Color(0, 0, 0, i / 0.5f);
            yield return new WaitForEndOfFrame();
        }
        _dialogue_text.color =  new Color(1, 1, 1, 1);
        float text_speed = 1f / _letters_per_second;

        for(int i=0; i<dialogue._lines.Length; i++)
        {
            yield return StartCoroutine(SetDialogueText(dialogue._lines[i], text_speed));
            _next_line_key.gameObject.SetActive(true);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
            _next_line_key.gameObject.SetActive(false);
        }

        for (float i = 0.5f; i >= 0; i -= Time.deltaTime)
        {
            _dialogue_box.color = new Color(0, 0, 0, i / 0.5f);
            _dialogue_text.color = new Color(1, 1, 1, i / 0.5f);
            yield return new WaitForEndOfFrame();
        }
        _dialogue_text.text = "";
        _reading = false;
        _player.EnableMovement();
        _dialogue_box.gameObject.SetActive(false);
    }

    private IEnumerator SetDialogueText(string sentence, float text_speed)
    {
        for (int k = 0; k <= sentence.Length; k++)
        {
            _dialogue_text.SetText(sentence.Substring(0, k));
            yield return new WaitForSeconds(text_speed);
        }
    }
}
