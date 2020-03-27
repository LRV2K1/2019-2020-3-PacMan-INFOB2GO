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
    Collider2D passenger;
    bool move;

    void Start()
    {
        move = false;
    }

    public void SetTargetPosition(Vector2 targetposition)
    {
        move = true;
        passenger = Physics2D.OverlapArea(transform.position - transform.localScale / 2, transform.position + transform.localScale / 2, layermask + layermask2);
        Debug.DrawLine(transform.position - transform.localScale / 2, transform.position + transform.localScale / 2, Color.red);
       
        this.targetposition = targetposition;

        if (Player())
        {
            PacManMoveScript player = passenger.GetComponent<PacManMoveScript>();
            player.SetMove(false);
        }

        if (passenger != null)
        {
            passengerOffset = transform.position - passenger.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {   
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

            if (Player())
            {
                PacManMoveScript player = passenger.GetComponent<PacManMoveScript>();
                player.SetMove(true);
            }

            if (passenger != null)
            {
                passenger.gameObject.transform.position = transform.position - new Vector3(passengerOffset.x, passengerOffset.y, 0);
            }
        }

    }

    bool Player()
    {
        if (passenger != null)
        {
            PacManMoveScript player = passenger.GetComponent<PacManMoveScript>();
            return player != null;
        }
        return false;
    }
}
