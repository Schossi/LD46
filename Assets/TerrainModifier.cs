using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainModifier : MonoBehaviour
{
    public Terrain Terrain;

    private float[,,] originalMap;

    // Start is called before the first frame update
    void Start()
    {
        Terrain.terrainData = Instantiate(Terrain.terrainData);
        originalMap = Terrain.terrainData.GetAlphamaps(0, 0, Terrain.terrainData.alphamapWidth, Terrain.terrainData.alphamapHeight);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(2))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var coords = ConvertWordCor2TerrCor(hit.point);
                coverArea(Mathf.RoundToInt(coords.x), Mathf.RoundToInt(coords.z), 3, Input.GetMouseButtonDown(0));

                //coverArea(256, 256, 200, false);

                //if (Input.GetMouseButtonDown(2))
                //    Terrain.terrainData.SetAlphamaps(0, 0, originalMap);
            }
        }
    }

    private Vector3 ConvertWordCor2TerrCor(Vector3 wordCor)
    {
        Vector3 vecRet = new Vector3();
        Vector3 terPosition = Terrain.transform.position;
        vecRet.x = ((wordCor.x - terPosition.x) / Terrain.terrainData.size.x) * Terrain.terrainData.alphamapWidth;
        vecRet.z = ((wordCor.z - terPosition.z) / Terrain.terrainData.size.z) * Terrain.terrainData.alphamapHeight;
        return vecRet;
    }

    private void coverArea(int x, int y, int radius, bool taint)
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

                    //var xCurrent = x - radius + i;
                    //var yCurrent = y - radius + j;

                    //float[,,] singleMap = new float[1, 1, 5];

                    //singleMap[0, 0, 0] = originalMap[yCurrent, xCurrent, 0];
                    //singleMap[0, 0, 1] = originalMap[yCurrent, xCurrent, 1];
                    //singleMap[0, 0, 2] = originalMap[yCurrent, xCurrent, 2];
                    //singleMap[0, 0, 3] = originalMap[yCurrent, xCurrent, 3];
                    //singleMap[0, 0, 4] = originalMap[yCurrent, xCurrent, 4];

                    //Terrain.terrainData.SetAlphamaps(xCurrent, yCurrent, singleMap);
                }
            }
        }

        //var check = Terrain.terrainData.GetAlphamaps(x - radius, y - radius, radius * 2 + 1, radius * 2 + 1);

        //if (taint)
        Terrain.terrainData.SetAlphamaps(x - radius, y - radius, map);
    }
}
