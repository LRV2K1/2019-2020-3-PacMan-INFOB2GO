using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntity : MonoBehaviour
{
    protected bool move = true;

    public virtual void SetMove(bool move)
    {
        this.move = move;
        transform.position = transform.position.Round();
    }
}