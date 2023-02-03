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

        if (_inventory.ContainsPickupOfType(PickupEnum.ENERGY) >= 0)
        {

        }
        else
        {

        }
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
