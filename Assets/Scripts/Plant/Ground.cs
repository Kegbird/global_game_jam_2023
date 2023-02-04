using Managers;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Utility;

public class Ground : MonoBehaviour
{
    [SerializeField]
    private DialogueScriptableObject _positive_dialogue;
    [SerializeField]
    private DialogueScriptableObject _negative_dialogue;
    [SerializeField]
    private bool _input;
    [SerializeField]
    private bool _planted;


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

        if(_input)
        {
            GameObject player = GameObject.FindWithTag(Tags.PLAYER_TAG);
            PlayerInventory _player_intenvory = player.GetComponent<PlayerInventory>();
            GameUIManager _game_ui_manager = GameObject.FindWithTag(Tags.LOGIC_TAG).GetComponent<GameUIManager>();

            if (Input.GetKeyUp(KeyCode.Return))
            {
                _input = false;

                PlayerController _player_controller = player.GetComponent<PlayerController>();
                if (_player_intenvory.IsSeedSelected())
                {
                    _player_controller.EnableMovement();
                    GetComponent<Collider2D>().enabled = false;
                    Vector3 position = transform.position;
                    PickupEnum seed_type = _player_intenvory.GetSelectedSeed();
                    int index = _player_intenvory.GetSelectedIndex();
                    _player_intenvory.RemovePickup(index);
                    _game_ui_manager.RemoveInventoryItem(index);
                    Debug.Log(seed_type);
                    //todo call here 
                    _planted = true;
                }
                else
                {
                    _player_controller.EnableMovement();
                }
                _player_intenvory.ResetCell();
                return;
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                _player_intenvory.Right();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _player_intenvory.Left();
            }
            _player_intenvory.HighlightCell();
        }
    }
}
