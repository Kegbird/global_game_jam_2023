using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
public class InstantiatePlant : MonoBehaviour
{
    public void spawnPlantByEnum(PickupEnum seed_type, Vector3 transform)
    {
        GameObject game_object = PickupUtilities.getPrefabByPickupEnum(seed_type);
        Debug.Log(game_object);
        if (game_object != null) {
            Instantiate(game_object, transform, Quaternion.identity);
        }
    }
}
