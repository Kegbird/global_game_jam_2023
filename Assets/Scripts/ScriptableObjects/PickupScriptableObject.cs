using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pickup", menuName = "ScriptableObjects/PickupScriptableObject", order = 1)]
public class PickupScriptableObject : ScriptableObject
{
    public string _name;
    public Sprite _sprite;
    public float _weight;
}
