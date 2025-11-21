using UnityEngine;
using UnityEngine.Tilemaps;

// Add this attribute to allow creating this asset via the Project window
[CreateAssetMenu(fileName = "New Resource Tile", menuName = "Custom/Resource Tile")]
public class ResourceTile : Tile
{
    [Header("Resource Stats")]
    public int maxHitPoints = 3;
    public string resourceID = "Copper"; // Identifier for logic

    // The rest of the Tile class logic (GetTileData, RefreshTile, etc.)
    // would be handled by Unity or custom overrides.
}