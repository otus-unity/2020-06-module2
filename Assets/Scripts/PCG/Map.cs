using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class Map
{
    public int width { get; private set; }
    public int height { get; private set; }
    public RectInt rect => new RectInt(0, 0, width, height);
    bool[,] walls;

    public Map(int w, int h)
    {
        width = w;
        height = h;
        walls = new bool[width, height];
    }

    public bool IsWall(int x, int y)
    {
        if (x < 0 || y < 0 || x >= width || y >= height)
            return true;
        return walls[x, y];
    }

    public void SetWall(int x, int y, bool flag)
    {
        if (x < 0 || y < 0 || x >= width || y >= height)
            throw new Exception("Map coordinates out of range.");
        walls[x, y] = flag;
    }

    public void PutMap(int targetX, int targetY, Map map, RectInt mapArea)
    {
        for (int y = 0; y < mapArea.height; y++) {
            for (int x = 0; x < mapArea.width; x++)
                SetWall(targetX + x, targetY + y, map.IsWall(mapArea.x + x, mapArea.y + y));
        }
    }

    public void Fill(RectInt area, bool wall)
    {
        for (int y = 0; y < area.height; y++) {
            for (int x = 0; x < area.width; x++)
                SetWall(area.x + x, area.y + y, wall);
        }
    }

    public void FillRandom(RectInt area, float threshold)
    {
        for (int y = 0; y < area.height; y++) {
            for (int x = 0; x < area.width; x++)
                SetWall(area.x + x, area.y + y, UnityEngine.Random.Range(0.0f, 1.0f) < threshold);
        }
    }

    public void ApplyToTilemap(Tilemap tilemap, TileData tileData, Vector2Int target, RectInt area)
    {
        for (int y = 0; y < area.height; y++) {
            for (int x = 0; x < area.width; x++) {
                Tile tile = IsWall(area.x + x, area.y + y) ? tileData.wallTile : null;
                tilemap.SetTile(new Vector3Int(target.x + x, target.y + y, 0), tile);
            }
        }
    }
}
