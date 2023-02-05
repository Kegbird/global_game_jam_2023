using Managers;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class EnergyMachine : MonoBehaviour
{
    [SerializeField]
    private DialogueScriptableObject _positive_dialogue_feedback;
    [SerializeField]
    private DialogueScriptableObject _negative_dialogue_feedback;


    public void Interact()
    {
        GameObject player = GameObject.FindWithTag(Tags.PLAYER_TAG);
        PlayerInventory _inventory = player.GetComponent<PlayerInventory>();

        int pickup_index = _inventory.ContainsPickupOfType(PickupEnum.ENERGY);
        if (pickup_index >= 0)
            StartCoroutine(PositiveInteractCoroutine(pickup_index));
        else
            StartCoroutine(NegativeInteractCoroutine());
    }

    IEnumerator PositiveInteractCoroutine(int index)
    {
        GameObject player = GameObject.FindWithTag(Tags.PLAYER_TAG);
        PlayerController _player_controller = player.GetComponent<PlayerController>();
        _player_controller.PlayInteractAnimation();
        PlayerInventory _inventory = player.GetComponent<PlayerInventory>();
        CountersManager _counters_manager = GameObject.FindWithTag(Tags.LOGIC_TAG).GetComponent<CountersManager>();
        DialogueManager _dialogue_manager = GameObject.FindWithTag(Tags.DIALOGUE_MANAGER_TAG).GetComponent<DialogueManager>();
        GameUIManager _game_ui_manager = GameObject.FindWithTag(Tags.LOGIC_TAG).GetComponent<GameUIManager>();

        yield return StartCoroutine(_dialogue_manager.ReadDialogue(_positive_dialogue_feedback));

        PickupScriptableObject pickup = _inventory.GetPickupAtIndex(index);
        _player_controller.DisableMovement();
        _counters_manager.DecreaseOxygenDecrementStep(pickup._weight);
        _inventory.RemovePickup(index);
        _game_ui_manager.RemoveInventoryItem(index);

        _counters_manager.IncreaseEnergyLevel();
        _player_controller.EnableMovement();
    }

    IEnumerator NegativeInteractCoroutine()
    {
        GameObject player = GameObject.FindWithTag(Tags.PLAYER_TAG);
        PlayerController _player_controller = player.GetComponent<PlayerController>();
        _player_controller.PlayInteractAnimation();
        DialogueManager _dialogue_manager = GameObject.FindWithTag(Tags.DIALOGUE_MANAGER_TAG).GetComponent<DialogueManager>();
        yield return StartCoroutine(_dialogue_manager.ReadDialogue(_negative_dialogue_feedback));
        _player_controller.DisableMovement();
        _player_controller.EnableMovement();
    }
}
