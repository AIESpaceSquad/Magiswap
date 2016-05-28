using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName ="NewInventoryTemplate", menuName = "Magiswap/InventoryTemplate", order = 1)]
public class InventoryTemplate : ScriptableObject {
    public List<InventoryNode> nodes;
    public Vector2 player1UIOffset;
    public Vector2 player2UIOffset;

}
