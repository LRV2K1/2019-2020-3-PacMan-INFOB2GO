using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 targetposition;
    public LayerMask layermask;
    public LayerMask layermask2;
    void Start()
    {
    }

    public void SetTargetPosition(Vector2 targetposition)
    {
        this.targetposition = targetposition;
    }

    // Update is called once per frame
    void Update()
    {   
        if (Mathf.Abs(transform.position.x - targetposition.x) > 0.001 || Mathf.Abs(transform.position.y - targetposition.y) > 0.001)
        {
            transform.position += (new Vector3(targetposition.x, targetposition.y, 0) - transform.position) * 16 * Time.deltaTime;
            Collider2D objects = Physics2D.OverlapArea(transform.position - transform.localScale / 2, transform.position + transform.localScale/2, layermask+layermask2);
            if (objects != null)
            {
                objects.gameObject.transform.position += (new Vector3(targetposition.x, targetposition.y, 0) - transform.position) * 16 * Time.deltaTime;
                PacManMoveScript player = objects.GetComponent<PacManMoveScript>();
                if (player != null)
                {
                    player.destination += (new Vector2(targetposition.x, targetposition.y) - new Vector2(transform.position.x, transform.position.y)) * 16 * Time.deltaTime;
                }
            }
        }
    }
}
