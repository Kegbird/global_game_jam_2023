using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class Readable : MonoBehaviour
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private DialogueScriptableObject _dialogue;
        [SerializeField]
        private Animator _animator;

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
            _animator.SetBool("show", false);
            DialogueManager _dialogue_manager = GameObject.FindWithTag(Tags.DIALOGUE_MANAGER_TAG).GetComponent<DialogueManager>();
            _dialogue_manager.StartDialogue(_dialogue);
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
}
