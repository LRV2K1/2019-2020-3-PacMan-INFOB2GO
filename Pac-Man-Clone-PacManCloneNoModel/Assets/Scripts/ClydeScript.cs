using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeScript : GhostBehaviourScript
{
    public GameObject PacMan;

    private void Awake()
    {
        startPos = transform.position;
    }
    // Start is called before the first frame update
    public override void Start()
    {
        scatterTile = new Vector2(1, -1);
        Destination = gameObject.transform.position.Round()/* + new Vector3(-1f, 0f, 0f)*/;
        currentDir = new Vector2(-1f, 0f);
    }

    public override void Chase()
    {
        float distanceToTarget = (PacMan.transform.position.Round() - transform.position.Round()).magnitude;
        if (distanceToTarget > 8)
        {
            targetTile = PacMan.transform.position.Round();
        }
        else
        {
            targetTile = scatterTile;
        }
    }

    public override void StartGame()
    {
        enabled = true;
        Destination = transform.position;
        UpdateLists();
    }
}
