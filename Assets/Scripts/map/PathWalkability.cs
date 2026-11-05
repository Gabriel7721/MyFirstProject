using UnityEngine;
using UnityEngine.Tilemaps;

public class PathWalkability : MonoBehaviour
{
    [SerializeField] private Tilemap pathTilemap;

    public bool CanMoveTo(Vector2 worldPosition)
    {
        if (pathTilemap == null)
        {
            Debug.LogError("Path Tilemap is not assigned.");
            return false;
        }

        Vector3Int cellPosition =
            pathTilemap.WorldToCell(worldPosition);

        return pathTilemap.HasTile(cellPosition);
    }
}
