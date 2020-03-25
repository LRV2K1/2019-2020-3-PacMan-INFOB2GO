using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelController : MonoBehaviour
{
    float timer = 2f;
    public Vector2 offset = new Vector2(0, 1);
    [System.Serializable]
    public struct Row
    {
        public Tile[] row;
    }

    public Row[] levelgrid;

    // Start is called before the first frame update
    void Start()
    {
        NewPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Change();
    }

    void NewPosition()
    {
        for (int y = 0; y < levelgrid.Length; y++)
        {
            for (int x = 0; x < levelgrid[y].row.Length; x++)
            {
                if (levelgrid[y].row[x] != null)
                {
                    levelgrid[y].row[x].targetposition = new Vector2(x * 4 + offset.x, y * 4 + offset.y);
                }
            }
        }
    }

    private void Change()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 2;
            int side = Random.Range(0, 4);
            int tile = Random.Range(0, 9);
            if (tile%2 == 0)
            {
                if (tile < 5)
                {
                    tile += 1;
                }
                else
                {
                    tile -= 1;
                }
            }

            switch (side)
            {
                case (0):
                    CircleHorizontal(tile);
                    break;
                case (1):
                    CircleVertical(tile);
                    break;
                case (2):
                    CircleVerticalRight(tile);
                    break;
                case (3):
                    CircleHorizontalRight(tile);
                    break;
            }
        }
    }

    private void CircleHorizontal(int y)
    {
        Tile last = levelgrid[y].row[0];
        for(int x = 1; x < levelgrid[y].row.Length; x++)
        {
            levelgrid[y].row[x - 1] = levelgrid[y].row[x];
        }
        levelgrid[y].row[levelgrid[y].row.Length - 1] = last;
        NewPosition();
    }

    private void CircleVertical(int x)
    {
        Tile last = levelgrid[0].row[x];
        for (int y = 1; y < levelgrid.Length; y++)
        {
            levelgrid[y - 1].row[x] = levelgrid[y].row[x];
        }
        levelgrid[levelgrid.Length - 1].row[x] = last;
        NewPosition();
    }

    private void CircleHorizontalRight(int y)
    {
        Tile last = levelgrid[y].row[levelgrid[y].row.Length - 1];

        for (int x = levelgrid[y].row.Length - 2; x >= 0; x--)
        {
            levelgrid[y].row[x + 1] = levelgrid[y].row[x];
        }
        levelgrid[y].row[0] = last;
        NewPosition();
    }

    private void CircleVerticalRight(int x)
    {
        Tile last = levelgrid[levelgrid.Length - 1].row[x];
        for (int y = levelgrid.Length - 2; y >= 0 ; y--)
        {
            levelgrid[y + 1].row[x] = levelgrid[y].row[x];
        }
        levelgrid[0].row[x] = last;
        NewPosition();
    }
}