using UnityEngine;
using Utility;

[CreateAssetMenu(fileName = "Pickup", menuName = "ScriptableObjects/PickupScriptableObject", order = 1)]
public class PickupScriptableObject : ScriptableObject
{
    public string _name;
    public string _description;
    public string _basic_infos;

    public Sprite _sprite;
    public Sprite _dex_sprite;
    public PickupEnum _type;

    public float _weight;
}
