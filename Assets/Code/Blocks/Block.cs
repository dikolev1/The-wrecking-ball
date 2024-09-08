using UnityEngine.Tilemaps;
using UnityEngine;

[CreateAssetMenu(fileName = "Block", menuName = "Tiles/Block")]
public class Block : Tile
{
    public bool IsImortal;

    public bool canBoost;
    public bool canKill;

    public void TakeDamage(Tilemap tilemap, Vector3Int position)
    {
        if (IsImortal)
            return;

        tilemap.SetTile(position, null);
    }
}
