using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{

    [CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/DialogueScriptableObject", order = 1)]
    public class DialogueScriptableObject : ScriptableObject
    {
        public string[] _lines;
    }
}