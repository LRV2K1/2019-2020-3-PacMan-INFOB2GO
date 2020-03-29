using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 targetposition;
    Vector2 passengerOffset;
    public LayerMask layermask;
    public LayerMask layermask2;
    //Collider2D passenger;
    MovingEntity passenger;
    LevelController level;
    bool move;

    void Start()
    {
        level = GetLevelController(transform);
        move = false;
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = (Vector3)position;
        targetposition = position;
    }

    public void SetTargetPosition(Vector2 targetposition)
    {
        if (transform.position == (Vector3)targetposition)
        {
            return;
        }
        move = true;
        Collider2D collider = Physics2D.OverlapArea(transform.position - transform.localScale * 2, transform.position + transform.localScale * 2, layermask + layermask2);
        if (collider != null)
        {
            passenger = collider.GetComponent<MovingEntity>();
        }
        else
        {
            passenger = null;
        }
       
        this.targetposition = targetposition;

        if (passenger != null)
        {
            passenger.SetMove(false);
            //passengerOffset = transform.position - passenger.transform.position;
            //passengerOffset.Normalize();
            passengerOffset = Vector2.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(transform.position - transform.localScale * 2, transform.position + transform.localScale * 2, Color.red);
        if ((Mathf.Abs(transform.position.x - targetposition.x) > 0.1f || Mathf.Abs(transform.position.y - targetposition.y) > 0.1f) && move)
        {
            transform.position += (new Vector3(targetposition.x, targetposition.y, 0) - transform.position) * 16 * Time.deltaTime;

            if (passenger != null)
            {
                passenger.gameObject.transform.position = transform.position - new Vector3(passengerOffset.x, passengerOffset.y, 0);
            }
        }
        else if (move)
        {
            move = false;
            transform.position = targetposition;

            if (passenger != null)
            {
                passenger.SetMove(true);
                passenger.gameObject.transform.position = transform.position - new Vector3(passengerOffset.x, passengerOffset.y, 0);
            }
        }
    }

    LevelController GetLevelController(Transform t)
    {
        if (t.GetComponent<LevelController>() != null)
        {
            return t.GetComponent<LevelController>();
        }

        if (t.parent != null)
        {
            GetLevelController(t.parent.transform);
        }
        return null;
    }

    public bool IsMoving
    {
        get { return move; }
    }
}