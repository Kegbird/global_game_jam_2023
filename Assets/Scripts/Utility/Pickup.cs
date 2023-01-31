using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
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

    public void ShowPopUp()
    {

    }

    public void Interact()
    {
        IEnumerator InteractCoroutine()
        {
            DialogueManager _dialogue_manager = GameObject.FindWithTag(Tags.DIALOGUE_MANAGER_TAG).GetComponent<DialogueManager>();
            yield return StartCoroutine(_dialogue_manager.ReadDialogue(_dialogue));
            PlayerInventory _inventory = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerInventory>();
            _inventory.AddPickup(_pickup);
            this.gameObject.SetActive(false);
            Destroy(this);
        }
        StartCoroutine(InteractCoroutine());
    }

    public void HidePopUp()
    {

    }

    private void OnCollisionEnter(Collision collision)
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