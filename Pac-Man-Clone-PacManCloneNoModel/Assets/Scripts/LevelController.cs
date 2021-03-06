﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelController : MonoBehaviour
{
    public bool change = false;

    float timer = 2f;
    public Vector2 offset = new Vector2(0, 1);
    [System.Serializable]
    public struct Row
    {
        public Tile[] row;
    }

    public Row[] levelgrid;
    Row[] startlevelgrid;
    public Vector2 size = new Vector2(9, 9);

    // Start is called before the first frame update
    void Start()
    {
        //startlevelgrid = levelgrid;
        startlevelgrid = CopyGrid(levelgrid);
        SetPosition();
    }

    //Omdat het structs zijn moet het zo gedaan worden
    Row[] CopyGrid(Row[] box)
    {
        Row[] returnbox = new Row[box.Length];
        for (int y = 0; y < returnbox.Length; y++)
        {
            returnbox[y].row = new Tile[box[y].row.Length];
            for (int x = 0; x < returnbox[y].row.Length; x++)
            {
                returnbox[y].row[x] = box[y].row[x];
            }
        }
        return returnbox;
    }

    // Update is called once per frame
    void Update()
    {
        Change();
    }

    public void ResetTilePosition()
    {
        levelgrid = CopyGrid(startlevelgrid);
        SetPosition();
    }

    void NewTargetPosition()
    {
        for (int y = 0; y < levelgrid.Length; y++)
        {
            for (int x = 0; x < levelgrid[y].row.Length; x++)
            {
                if (levelgrid[y].row[x] != null)
                {
                    levelgrid[y].row[x].SetTargetPosition(new Vector2(x * 4 + offset.x, y * 4 + offset.y));
                }
            }
        }
    }

    void SetPosition()
    {
        for (int y = 0; y < levelgrid.Length; y++)
        {
            for (int x = 0; x < levelgrid[y].row.Length; x++)
            {
                if (levelgrid[y].row[x] != null)
                {
                    levelgrid[y].row[x].SetPosition(new Vector2(x * 4 + offset.x, y * 4 + offset.y));
                }
            }
        }
    }

    private void Change()
    {
        if (!change)
        {
            return;
        }
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
        NewTargetPosition();
    }

    private void CircleVertical(int x)
    {
        Tile last = levelgrid[0].row[x];
        for (int y = 1; y < levelgrid.Length; y++)
        {
            levelgrid[y - 1].row[x] = levelgrid[y].row[x];
        }
        levelgrid[levelgrid.Length - 1].row[x] = last;
        NewTargetPosition();
    }

    private void CircleHorizontalRight(int y)
    {
        Tile last = levelgrid[y].row[levelgrid[y].row.Length - 1];

        for (int x = levelgrid[y].row.Length - 2; x >= 0; x--)
        {
            levelgrid[y].row[x + 1] = levelgrid[y].row[x];
        }
        levelgrid[y].row[0] = last;
        NewTargetPosition();
    }

    private void CircleVerticalRight(int x)
    {
        Tile last = levelgrid[levelgrid.Length - 1].row[x];
        for (int y = levelgrid.Length - 2; y >= 0 ; y--)
        {
            levelgrid[y + 1].row[x] = levelgrid[y].row[x];
        }
        levelgrid[0].row[x] = last;
        NewTargetPosition();
    }

    public bool IsTileMoving(Vector2 gridPos)
    {
        if (gridPos.x < 0 || gridPos.x >= size.x || gridPos.y < 0 || gridPos.y >= size.y)
        {
            return true;
        }
        Tile tile = levelgrid[(int)gridPos.y].row[(int)gridPos.x];
        if (tile == null)
        {
            return false;
        }

        return tile.IsMoving;
    }

    public Vector2 GridPos(Vector2 position)
    {
        position -= offset;
        position += new Vector2(2, 2);
        return new Vector2(Mathf.Floor(position.x / 4), Mathf.Floor(position.y / 4));
    }
}