using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntity : MonoBehaviour
{
    protected bool move = true;
    public Vector2 destination;

    public virtual void SetMove(bool move)
    {
        this.move = move;
        transform.position = transform.position.Round();
        destination = transform.position;
    }
}