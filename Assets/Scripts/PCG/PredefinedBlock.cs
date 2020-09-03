using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PredefinedBlock : MonoBehaviour
{
    public const int BitmaskLeft = 1; // 0001
    public const int BitmaskRight = 2; // 0010
    public const int BitmaskUp = 4; // 0100
    public const int BitmaskDown = 8; // 1000

    const int X = -5;
    const int Y = -3;
    public const int Width = 12;
    public const int Height = 8;

    public bool left;
    public bool right;
    public bool up;
    public bool down;

    public Transform playerStart;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    public int GetBitmask()
    {
        int result = 0;
        if (left)
            result |= BitmaskLeft;
        if (right)
            result |= BitmaskRight;
        if (up)
            result |= BitmaskUp;
        if (down)
            result |= BitmaskDown;
        return result;
    }

    public Vector3 GetPlayerPosition()
    {
        var tilemap = GetComponentInChildren<Tilemap>();
        return playerStart.position - tilemap.layoutGrid.CellToWorld(new Vector3Int(X, Y, 0));
    }

    public Map GetMap()
    {
        var result = new Map(Width, Height);
        var tilemap = GetComponentInChildren<Tilemap>();
        for (int y = 0; y < Height; y++) {
            for (int x = 0; x < Width; x++) {
                var coord = new Vector3Int(X + x, Y + y, 0);
                result.SetWall(x, y, tilemap.GetTile(coord) != null);
            }
        }
        return result;
    }
}
