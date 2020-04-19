using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainModifier : MonoBehaviour
{
    private static TerrainModifier _instance;
    public static TerrainModifier Instance => _instance;

    public Terrain Terrain;

    private float[,,] originalMap;

    public TerrainModifier()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Terrain.terrainData = Instantiate(Terrain.terrainData);
        originalMap = Terrain.terrainData.GetAlphamaps(0, 0, Terrain.terrainData.alphamapWidth, Terrain.terrainData.alphamapHeight);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public Vector3 ConvertWordCor2TerrCor(Vector3 wordCor)
    {
        Vector3 vecRet = new Vector3();
        Vector3 terPosition = Terrain.transform.position;
        vecRet.x = ((wordCor.x - terPosition.x) / Terrain.terrainData.size.x) * Terrain.terrainData.alphamapWidth;
        vecRet.z = ((wordCor.z - terPosition.z) / Terrain.terrainData.size.z) * Terrain.terrainData.alphamapHeight;
        return vecRet;
    }

    public void CoverArea(Vector3 position, int radius, bool taint)
    {
        if (!taint && Tree.GameOver)
            return;

        var coords = ConvertWordCor2TerrCor(position);

        int x = Mathf.RoundToInt(coords.x);
        int y = Mathf.RoundToInt(coords.z);

        if (x - (radius + 1) < 0 || y - (radius + 1) < 0)
            return;

        if (x + (radius + 1) > 512 || y + (radius + 1) > 512)
            return;

        CoverArea(x, y, radius, taint);

        Tree.Paint(position, taint);
    }

    private void CoverArea(int x, int y, int radius, bool taint)
    {
        float[,,] map = new float[radius * 2 + 1, radius * 2 + 1, 5];

        for (int i = 0; i < radius * 2 + 1; i++)
        {
            for (int j = 0; j < radius * 2 + 1; j++)
            {
                if (taint)
                {
                    map[i, j, 4] = 1f;
                }
                else
                {
                    map[i, j, 0] = originalMap[y - radius + i, x - radius + j, 0];
                    map[i, j, 1] = originalMap[y - radius + i, x - radius + j, 1];
                    map[i, j, 2] = originalMap[y - radius + i, x - radius + j, 2];
                    map[i, j, 3] = originalMap[y - radius + i, x - radius + j, 3];
                    map[i, j, 4] = originalMap[y - radius + i, x - radius + j, 4];
                }
            }
        }

        Terrain.terrainData.SetAlphamaps(x - radius, y - radius, map);
    }
}
