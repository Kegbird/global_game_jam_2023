using UnityEngine;
using UnityEditor;
namespace Utility
{   
    public enum PickupEnum
    {
        ENERGY,
        WATER,
        LAVANDULA_X,
        CUCURBITA_X,
        CORYLUS_X,
        SNOWHEAP,
        HAILTREE,
        MEDUSA_FLYTRAP,
        LION_FLOWER,
        FERE_MOSS,
        GEHENNA,
        GLACIPILA,
        DRAGONBORN,
        LIFE_HERB,
        LIBRA_DE_FOCUS,
        HELLFLOWER,
        GHIDORAH,
        SACRED_LIFE
        
    }
    
    public class PickupUtilities : MonoBehaviour {
        public static GameObject getPrefabByPickupEnum(PickupEnum pickup_enum) {
            string stringified_enum = pickup_enum.ToString();
            GameObject plant_prefab = Resources.Load<GameObject>($"Plants/{stringified_enum}");
            Debug.Log(plant_prefab);
            return plant_prefab;
        }
    }
}
