using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PacManMoveScript : MovingEntity
{
    public float movespeed = 0.3f;
    public AudioSource chomp;
    public Vector2 moveDirection;
    public TextMeshProUGUI score;
    public GameObject GameManager;

    [HideInInspector]
    public float timeSpent;

    void Start()
    {
        startPos = transform.position;
        Destination = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!move)
        {
            return;
        }
        // Come up with better name for 'p'
        Vector2 p = Vector2.MoveTowards(transform.position, Destination, movespeed);
        GetComponent<Rigidbody2D>().MovePosition(p);

        // Check for Input if not moving
        if ((Vector2)transform.position == Destination)
        {
            if (Input.GetKey(KeyCode.UpArrow) && valid(Vector2.up))
                Destination = (Vector2)transform.position + Vector2.up;
            if (Input.GetKey(KeyCode.RightArrow) && valid(Vector2.right))
                Destination = (Vector2)transform.position + Vector2.right;
            if (Input.GetKey(KeyCode.DownArrow) && valid(-Vector2.up))
                Destination = (Vector2)transform.position - Vector2.up;
            if (Input.GetKey(KeyCode.LeftArrow) && valid(-Vector2.right))
                Destination = (Vector2)transform.position - Vector2.right;
        }
        
        //works for now, counts moving in a wall as moving
        if ((Vector2)transform.position == Destination && !Input.anyKey)
        {
            timeSpent += Time.deltaTime;
        }


        //To Fix: Plays on awake, stops/starts a lot (turned off because annoying)
            if ((Vector2)transform.position == Destination && (Input.GetKey("up")))
        {
            //chomp.Play();
        }

        // Animation Parameters
        Vector2 dir = Destination - (Vector2)transform.position;
        moveDirection = CheckMoveDirection(dir);
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    //Checks the direction Pacman is moving in. This is used in Inky and Pinky's pathfinding logic
    Vector2 CheckMoveDirection(Vector2 dir)
    {
        dir.Normalize();
        moveDirection = dir;
        return moveDirection;
    }

    // Cast Line from 'next square in movedirection to 'Pac-Man'. True = hit pac man, ignores ghosts
    bool valid(Vector2 dir)
    {
        LayerMask layerMask = LayerMask.GetMask("Ghosts");
        Vector2 pos = transform.position.Round();
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos, ~layerMask);
        Debug.DrawLine(pos, pos + dir, Color.green);
        return (hit.collider == GetComponent<Collider2D>());
    }

    //Determines what happens when collided with. PacMan increases score when eating pellets, powers up due to power pellets and dies to ghosts
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PowerPellet" && move)
        {
            GameManager.GetComponent<GameManagerScript>().PowerPelletCollected();
            GameManager.GetComponent<GameManagerScript>().pelletsCollected += 1;

            score.GetComponent<ScoreScript>().ScorePowerPellet();
            StartCoroutine(GameManager.GetComponent<GameManagerScript>().CheckForGameEnd());
        }

        if (collision.tag == "Pellet" && move)
        {
            GameManager.GetComponent<GameManagerScript>().pelletsCollected += 1;
            score.GetComponent<ScoreScript>().ScorePellet();
            StartCoroutine(GameManager.GetComponent<GameManagerScript>().CheckForGameEnd());
        }
        if (collision.tag == "Ghost" && collision.gameObject.GetComponent<GhostBehaviourScript>().frightened && move)
        {
            score.GetComponent<ScoreScript>().ScoreGhost();
        }

        if (collision.name == "TeleportLeft")
        {
            Vector2 tp = new Vector2(32, 19);
            gameObject.transform.position = tp;
            Destination = transform.position;
        }
        if (collision.name == "TeleportRight")
        {
            Vector2 tp = new Vector2(0, 19);
            gameObject.transform.position = tp;
            Destination = transform.position;
        }
        if (collision.name == "TeleportTop")
        {
            Vector2 tp = new Vector2(16, 3);
            gameObject.transform.position = tp;
            Destination = transform.position;
        }
        if (collision.name == "TeleportBottom")
        {
            Vector2 tp = new Vector2(16, 35);
            gameObject.transform.position = tp;
            Destination = transform.position;
        }
    }

    public void ResetGame()
    {
        gameObject.transform.position = startPos;
        Destination = startPos;
    }
}
