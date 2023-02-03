using UnityEngine;
using Utility;

[CreateAssetMenu(fileName = "Pickup", menuName = "ScriptableObjects/PickupScriptableObject", order = 1)]
public class PickupScriptableObject : ScriptableObject
{
    public string _name;
    public Sprite _sprite;
    public PickupEnum _type;
    public float _weight;
}
