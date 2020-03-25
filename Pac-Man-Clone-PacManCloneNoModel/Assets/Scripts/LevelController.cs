using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelController : MonoBehaviour
{
    public Vector2 offset = new Vector2(0, 1);
    [System.Serializable]
    public struct Row
    {
        public GameObject[] row;
    }

    public Row[] levelgrid;

    // Start is called before the first frame update
    void Start()
    {
        for(int y = 0; y < levelgrid.Length; y++)
        {
            for (int x = 0; x < levelgrid[y].row.Length; x++)
            {
                if (levelgrid[y].row[x] != null)
                {
                    levelgrid[y].row[x].transform.position = new Vector2(x * 4 + offset.x, y * 4 + offset.y);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CircleHorizontal(3);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            CircleVertical(3);
        }

        Rotate();
    }

    private void Rotate()
    {
        for (int y = 0; y < levelgrid.Length; y++)
        {
            for (int x = 0; x < levelgrid[y].row.Length; x++)
            {
                if (levelgrid[y].row[x] != null)
                {
                    levelgrid[y].row[x].transform.position += (new Vector3(x * 4 + offset.x, y * 4 + offset.y, 0)- levelgrid[y].row[x].transform.position) * 16 * Time.deltaTime;
                }
            }
        }
    }

    private void CircleHorizontal(int y)
    {
        GameObject last = levelgrid[y].row[0];
        for(int x = 1; x < levelgrid[y].row.Length; x++)
        {
            levelgrid[y].row[x - 1] = levelgrid[y].row[x];
        }
        levelgrid[y].row[levelgrid[y].row.Length - 1] = last;
    }

    private void CircleVertical(int x)
    {
        GameObject last = levelgrid[0].row[x];
        for (int y = 1; y < levelgrid.Length; y++)
        {
            levelgrid[y - 1].row[x] = levelgrid[y].row[x];
        }
        levelgrid[levelgrid.Length - 1].row[x] = last;
    }
}