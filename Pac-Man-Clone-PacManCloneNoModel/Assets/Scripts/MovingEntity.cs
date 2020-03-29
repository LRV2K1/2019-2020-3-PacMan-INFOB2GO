using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntity : MonoBehaviour
{
    protected bool move = true;
    Vector2 destination;
    public LevelController level;
    public Vector2 GridPos;

    private void Update()
    {
        GridPos = level.GridPos((Vector2)transform.position + Vector2.down);
        Debug.DrawLine(transform.position, destination, Color.red);
    }

    private void Start()
    {
        destination = transform.position;
    }

    public virtual void SetMove(bool move)
    {
        this.move = move;
        transform.position = transform.position.Round();
        destination = transform.position;
    }

    protected Vector2 Destination
    {
        get { return destination; }
        set
        {
            if (LegalDestination(value + (value - (Vector2)transform.position)/2))
            {
                destination = value;
            }
            else
            {
                destination = transform.position;
            }
        }
    }

    bool LegalDestination(Vector2 destination)
    {
        Vector2 gridPos = level.GridPos(destination);
        /*
        if (gridPos.x < 0)
        {
            return false;
        }
        if (gridPos.x >= level.size.x)
        {
            return false;
        }
        if (gridPos.y < 0)
        {
            return false;
        }
        if (gridPos.y >= level.size.y)
        {
            return false;
        }
        */
        return !level.IsTileMoving(gridPos);
    }
}