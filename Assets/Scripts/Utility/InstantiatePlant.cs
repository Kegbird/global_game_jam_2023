using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
public class InstantiatePlant : MonoBehaviour
{
    private
    // Start is called before the first frame update
    void Start()
    {
        spawnPlantByEnum(PickupEnum.MEDUSA_FLYTRAP, new Vector3(2.0f, 0, 0));
    }

    // public gameObject getPrefabByPickupEnum( PickupEnum enum) {
    //     return GameObject.find("/ {ENUM.stringify}")
    // }
    // Update is called once per frame
    void Update()
    {
        
    }


    public void spawnPlantByEnum(PickupEnum seed_type, Vector3 transform)
    {
        GameObject game_object = PickupUtilities.getPrefabByPickupEnum(seed_type);
        Debug.Log(game_object);
        if (game_object != null) {
            Instantiate(game_object, transform, Quaternion.identity);
        }
    }
}
