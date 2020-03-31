using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntity : MonoBehaviour
{
    protected bool move = true;
    Vector2 destination;
    public LevelController level;
    public Vector2 startPos;

    private void Start()
    {
        startPos = gameObject.transform.position;
        destination = transform.position;
    }

    private void Awake()
    {
        startPos = transform.position;
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
                ResetDestination();
            }
        }
    }

    public virtual void ResetDestination()
    {
        transform.position = transform.position.Round();
        destination = transform.position;
    }

    bool LegalDestination(Vector2 destination)
    {
        Vector2 gridPos = level.GridPos(destination);
        return !level.IsTileMoving(gridPos);
    }

    public void ResetPosition()
    {
        transform.position = startPos;
    }
}