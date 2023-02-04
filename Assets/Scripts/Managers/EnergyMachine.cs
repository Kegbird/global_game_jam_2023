using Managers;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class EnergyMachine : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private DialogueScriptableObject _positive_dialogue_feedback;
    [SerializeField]
    private DialogueScriptableObject _negative_dialogue_feedback;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ShowPopUp()
    {
        _animator.SetBool("show", true);
    }

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
        PlayerInventory _inventory = player.GetComponent<PlayerInventory>();
        PlayerController _player_controller = player.GetComponent<PlayerController>();
        CountersManager _counters_manager = GameObject.FindWithTag(Tags.LOGIC_TAG).GetComponent<CountersManager>();
        DialogueManager _dialogue_manager = GameObject.FindWithTag(Tags.DIALOGUE_MANAGER_TAG).GetComponent<DialogueManager>();
        yield return StartCoroutine(_dialogue_manager.ReadDialogue(_positive_dialogue_feedback));
        _player_controller.DisableMovement();
        _inventory.RemovePickup(index);
        _player_controller.PlayInteractAnimation();
        _counters_manager.IncreaseEnergyLevel();
        yield return new WaitForSeconds(1f);
        _player_controller.EnableMovement();
        _animator.SetBool("show", true);
    }

    IEnumerator NegativeInteractCoroutine()
    {
        GameObject player = GameObject.FindWithTag(Tags.PLAYER_TAG);
        PlayerController _player_controller = player.GetComponent<PlayerController>();
        DialogueManager _dialogue_manager = GameObject.FindWithTag(Tags.DIALOGUE_MANAGER_TAG).GetComponent<DialogueManager>();
        yield return StartCoroutine(_dialogue_manager.ReadDialogue(_negative_dialogue_feedback));
        _player_controller.DisableMovement();
        _player_controller.PlayInteractAnimation();
        yield return new WaitForSeconds(1f);
        _player_controller.EnableMovement();
        _animator.SetBool("show", true);
    }

    public void HidePopUp()
    {
        _animator.SetBool("show", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            ShowPopUp();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            HidePopUp();
    }
}
