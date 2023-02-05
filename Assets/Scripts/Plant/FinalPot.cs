using Managers;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

public class FinalPot : MonoBehaviour
{
    [SerializeField]
    private bool _input;
    [SerializeField]
    private bool _planted;
    [SerializeField]
    private ScriptableObject _winning_dialogue;
    [SerializeField]
    private DialogueScriptableObject _losing_dialogue;

    public void Interact()
    {
        GameObject player = GameObject.FindWithTag(Tags.PLAYER_TAG);
        PlayerController _player_controller = player.GetComponent<PlayerController>();
        _player_controller.DisableMovement();
        _input = true;
    }

    private void Update()
    {
        if (_planted)
            return;

        if (_input)
        {
            GameObject player = GameObject.FindWithTag(Tags.PLAYER_TAG);
            PlayerInventory _player_intenvory = player.GetComponent<PlayerInventory>();
            GameUIManager _game_ui_manager = GameObject.FindWithTag(Tags.LOGIC_TAG).GetComponent<GameUIManager>();
            DialogueManager _dialogue_manager = GameObject.FindWithTag(Tags.DIALOGUE_MANAGER_TAG).GetComponent<DialogueManager>();
            CountersManager _counters_manager = GameObject.FindWithTag(Tags.LOGIC_TAG).GetComponent<CountersManager>();
            _player_intenvory.HighlightCell();

            if (Input.GetKeyUp(KeyCode.Return))
            {
                _input = false;

                PlayerController _player_controller = player.GetComponent<PlayerController>();
                PickupEnum seed_type = _player_intenvory.GetSelectedSeed();

                if (seed_type == PickupEnum.SACRED_LIFE)
                {
                    _player_controller.PlayInteractAnimation();
                    StartCoroutine(GameOver());
                }
                else if (seed_type == PickupEnum.ENERGY || seed_type == PickupEnum.WATER)
                {
                    _player_controller.EnableMovement();
                }
                else
                {
                    StartCoroutine(Lose());
                }
                _player_intenvory.ResetCell();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _player_intenvory.Right();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _player_intenvory.Left();
            }
        }
    }

    private IEnumerator Lose()
    {
        GameObject player = GameObject.FindWithTag(Tags.PLAYER_TAG);
        PlayerInventory _player_intenvory = player.GetComponent<PlayerInventory>();
        GameUIManager _game_ui_manager = GameObject.FindWithTag(Tags.LOGIC_TAG).GetComponent<GameUIManager>();
        DialogueManager _dialogue_manager = GameObject.FindWithTag(Tags.DIALOGUE_MANAGER_TAG).GetComponent<DialogueManager>();
        CountersManager _counters_manager = GameObject.FindWithTag(Tags.LOGIC_TAG).GetComponent<CountersManager>();
        PlayerController _player_controller = player.GetComponent<PlayerController>();
        _player_controller.PlayInteractAnimation();
        yield return StartCoroutine(_dialogue_manager.ReadDialogue(_losing_dialogue));
        _player_controller.DisableMovement();
        int inventory_index = _player_intenvory.GetSelectedIndex();
        PickupScriptableObject pob = _player_intenvory.GetPickupAtIndex(inventory_index);
        _player_intenvory.RemovePickup(inventory_index);
        _game_ui_manager.RemoveInventoryItem(inventory_index);
        _counters_manager.DecreaseOxygenDecrementStep(pob._weight);
        _player_controller.EnableMovement();
    }

    private IEnumerator GameOver()
    {
        GameObject player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
        player.GetComponent<PlayerController>().DisableMovement();

        GameUIManager game_ui_manager = GameObject.Find(Tags.LOGIC_TAG).GetComponent<GameUIManager>();
        yield return StartCoroutine(game_ui_manager.ShowBlackScreen());
        SceneManager.LoadScene((int)SceneEnum.GOOD_ENDING_SCENE);
    }
}
