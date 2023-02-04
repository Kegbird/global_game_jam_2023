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
    [SerializeField]
    private bool quest;
    [SerializeField]
    private int choice;

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
        _reading = true;
        for (float i = 0; i <= 1f; i += Time.deltaTime)
        {
            _dialogue_box.color = new Color(0, 0, 0, i / 1f);
            yield return new WaitForEndOfFrame();
        }
        _dialogue_text.color =  new Color(1, 1, 1, 1);
        float text_speed = 1f / _letters_per_second;

        yield return new WaitForSeconds(1f);

        for(int i=0; i<dialogue._lines.Length; i++)
        {
            yield return StartCoroutine(SetDialogueText(dialogue._lines[i], text_speed));
            _next_line_key.gameObject.SetActive(true);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
            _next_line_key.gameObject.SetActive(false);
        }

        for (float i = 1f; i >= 0; i -= Time.deltaTime)
        {
            _dialogue_box.color = new Color(0, 0, 0, i / 1f);
            _dialogue_text.color = new Color(1, 1, 1, i / 1f);
            yield return new WaitForEndOfFrame();
        }
        _dialogue_text.text = "";
        _reading = false;
        _player.EnableMovement();
    }

    void Update()
    {
        if(quest)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                choice = 1;
                quest = false;
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                choice = 2;
                quest = false;
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                choice = 3;
                quest = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                choice = 4;
                quest = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                choice = 5;
                quest = false;
            }
        }
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
