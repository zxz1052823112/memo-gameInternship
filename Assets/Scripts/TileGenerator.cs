using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TileGenerator : MonoBehaviour
{
    public Tilemap tileMap;
    public TileBase[] tileTypes;
    public int generationRadius = 10;
    public int removalRadius = 15;
    private HashSet<Vector3Int> generatedTiles = new HashSet<Vector3Int>();
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GenerateTilesAroundPlayer();
    }

    void Update()
    {
        GenerateTilesAroundPlayer();
        RemoveDistantTiles();
    }

    void GenerateTilesAroundPlayer()
    {
        Vector3Int playerPos = tileMap.WorldToCell(player.position);
        for (int x = playerPos.x - generationRadius; x <= playerPos.x + generationRadius; x++)
        {
            for (int y = playerPos.y - generationRadius; y <= playerPos.y + generationRadius; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                if (!generatedTiles.Contains(tilePos))
                {
                    TileBase tile = DetermineTileType(tilePos);
                    tileMap.SetTile(tilePos, tile);
                    generatedTiles.Add(tilePos);
                }
            }
        }
    }

    TileBase DetermineTileType(Vector3Int position)
    {
        // 示例：根据Perlin噪声生成地形
        float perlinValue = Mathf.PerlinNoise(position.x * 0.1f, position.y * 0.1f);
        if (perlinValue < 0.3f)
            return tileTypes[0]; // 水
        else if (perlinValue < 0.6f)
            return tileTypes[1]; // 草地
        else
            return tileTypes[2]; // 山地
    }

    void RemoveDistantTiles()
    {
        Vector3Int playerPos = tileMap.WorldToCell(player.position);
        List<Vector3Int> tilesToRemove = new List<Vector3Int>();
        foreach (var tilePos in generatedTiles)
        {
            if (Mathf.Abs(tilePos.x - playerPos.x) > removalRadius || Mathf.Abs(tilePos.y - playerPos.y) > removalRadius)
            {
                tileMap.SetTile(tilePos, null);
                tilesToRemove.Add(tilePos);
            }
        }
        foreach (var tilePos in tilesToRemove)
        {
            generatedTiles.Remove(tilePos);
        }
    }
}
