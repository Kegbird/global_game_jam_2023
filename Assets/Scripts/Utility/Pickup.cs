using Managers;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Accessibility;
using Utility;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private DialogueScriptableObject _dialogue;
    [SerializeField]
    private PickupScriptableObject _pickup;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private GameObject _popup;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Debug.Log(_dialogue);
    }

    public void ShowPopUp()
    {
        _popup.SetActive(true);
    }

    public void Interact()
    {
        HidePopUp();
        IEnumerator InteractCoroutine()
        {
            GameObject player = GameObject.FindWithTag(Tags.PLAYER_TAG);
            PlayerInventory _inventory = player.GetComponent<PlayerInventory>();
            PlayerController _player_controller = player.GetComponent<PlayerController>();
            CountersManager _counters_manager = GameObject.FindWithTag(Tags.LOGIC_TAG).GetComponent<CountersManager>();
            DialogueManager _dialogue_manager = GameObject.FindWithTag(Tags.DIALOGUE_MANAGER_TAG).GetComponent<DialogueManager>();
            yield return StartCoroutine(_dialogue_manager.ReadDialogue(_dialogue));
            _player_controller.DisableMovement();
            _inventory.AddPickup(_pickup);
            _player_controller.PlayInteractAnimation();
            _counters_manager.IncreaseOxygenDecrementStep(_pickup._weight);
            yield return new WaitForSeconds(1f);
            _player_controller.EnableMovement();
            ShowPopUp();
        }
        StartCoroutine(InteractCoroutine());
    }

    public void HidePopUp()
    {
        _popup.SetActive(false);
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
