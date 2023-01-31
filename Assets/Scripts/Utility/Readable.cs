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

        public void ShowPopUp()
        {

        }

        public void Interact()
        {
            DialogueManager _dialogue_manager = GameObject.FindWithTag(Tags.DIALOGUE_MANAGER_TAG).GetComponent<DialogueManager>();
            _dialogue_manager.StartDialogue(_dialogue);
        }

        public void HidePopUp()
        {

        }
    }
}
