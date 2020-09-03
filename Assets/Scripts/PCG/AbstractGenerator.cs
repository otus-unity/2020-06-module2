using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class AbstractGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public TileData tileData;

    public int width;
    public int height;

    protected Map map;

    protected abstract void GeneratorImpl();

    protected void Apply()
    {
        map.ApplyToTilemap(tilemap, tileData, Vector2Int.zero, map.rect);
    }

    [ContextMenu("Generate")]
    public void Generate()
    {
        map = new Map(width, height);
        GeneratorImpl();
        Apply();
    }
}
