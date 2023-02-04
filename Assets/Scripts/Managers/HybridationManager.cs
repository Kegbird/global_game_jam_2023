using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HybridationManager
{

    Dictionary<(Utility.PickupEnum, Utility.PickupEnum), Utility.PickupEnum> _herbarium2;

    public HybridationManager()
    {
        _herbarium2 = new Dictionary<(Utility.PickupEnum, Utility.PickupEnum), Utility.PickupEnum>();
        _herbarium2.Add((Utility.PickupEnum.LAVANDULA_X, Utility.PickupEnum.SNOWHEAP), Utility.PickupEnum.WHITELION);
        _herbarium2.Add((Utility.PickupEnum.SNOWHEAP, Utility.PickupEnum.CUCURBITA_X), Utility.PickupEnum.GLACIPILA);
        _herbarium2.Add((Utility.PickupEnum.HAILTREE, Utility.PickupEnum.CORYLUS_X), Utility.PickupEnum.IRONGIANT);
        _herbarium2.Add((Utility.PickupEnum.LAVANDULA_X, Utility.PickupEnum.LION_FLOWER), Utility.PickupEnum.CATNIP_2_0);
        _herbarium2.Add((Utility.PickupEnum.CUCURBITA_X, Utility.PickupEnum.MEDUSA_FLYTRAP), Utility.PickupEnum.PITFALL_GOURD);
        _herbarium2.Add((Utility.PickupEnum.CORYLUS_X, Utility.PickupEnum.MEDUSA_FLYTRAP), Utility.PickupEnum.DRAGONBORN);
        _herbarium2.Add((Utility.PickupEnum.LAVANDULA_X, Utility.PickupEnum.FERE_MOSS), Utility.PickupEnum.LIFE_HERB);
        _herbarium2.Add((Utility.PickupEnum.CUCURBITA_X, Utility.PickupEnum.GEHENNA), Utility.PickupEnum.LIBRA_DE_FOCUS);
        _herbarium2.Add((Utility.PickupEnum.CORYLUS_X, Utility.PickupEnum.GEHENNA), Utility.PickupEnum.PYROPHITE);
        _herbarium2.Add((Utility.PickupEnum.LIBRA_DE_FOCUS, Utility.PickupEnum.LIFE_HERB), Utility.PickupEnum.HELLFLOWER);
        _herbarium2.Add((Utility.PickupEnum.GLACIPILA, Utility.PickupEnum.DRAGONBORN), Utility.PickupEnum.GHIDORAH);
        _herbarium2.Add((Utility.PickupEnum.HELLFLOWER, Utility.PickupEnum.GHIDORAH), Utility.PickupEnum.SACRED_LIFE);
    }

    public Utility.PickupEnum? GetHybridation(Utility.PickupEnum plantA, Utility.PickupEnum plantB)
    {
        if(_herbarium2.ContainsKey((plantA, plantB)))
        {
            return _herbarium2[(plantA, plantB)];
        } else if (_herbarium2.ContainsKey((plantB, plantA)))
        {
            return _herbarium2[(plantB, plantA)];
        }
        
        return null;
    }
}
