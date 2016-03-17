using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName ="NewInventoryTemplate", menuName = "Magiswap/InventoryTemplate", order = 1)]
public class InventoryTemplate : ScriptableObject {
    public List<TemplateNode> nodes;

}
