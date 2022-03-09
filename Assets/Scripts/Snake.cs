using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // GLOBAL VARS

    public Transform segmentPrefab;
    private List<Transform> segments = new List<Transform>();
    public Vector2 direction = Vector2.right;
    private Vector2 input;
    public int initSize = 6;

    private void Start()
    {
        // Segment Handeling
        Death();
    }


    private void Update()
    {
        // Calls the direction function, handles changing direction when pressing keys.
        ChangeDirection();
    }

    // Might wanna replace this with Unity inputs later.
    private void ChangeDirection()
    {
        if(direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                direction = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                direction = Vector2.down;
            }
        }
        else if(direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                direction = Vector2.right;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                direction = Vector2.left;
            }
        }  
    }

    // Applies the actual movement to my snake, the Mathf is so that we wont get float numbers and we stay on grid.
    private void Movement()
    {
        if(input != Vector2.zero)
        {
            direction = input;
        }

        // Moving the tail.
        for(int i = segments.Count -1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }
        // Moving the player.
        float posx = Mathf.Round(transform.position.x) + direction.x;
        float posy = Mathf.Round(transform.position.y) + direction.y;

        transform.position = new Vector2(posx,posy);


    }

    private void FixedUpdate()
    {
        // We apply movement here in fixedUpdate so we can check for collisions.
        Movement();
    }


    // Grows the snake. Spawns a segment on the position of the last segment ie the tail end.
    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    public void Death()
    {
        direction = Vector2.right;
        transform.position = Vector3.zero;

        // Detroy the tail, leaving the head.
        for(int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(transform);

        for(int i = 1; i < this.initSize; i++)
        {
            Grow();
        }
    }

    // Calls Grow() when we hit food.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }
            else if(other.tag == "Wall")
            {
                Death();
            }
    }
}
