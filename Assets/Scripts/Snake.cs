using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;

    private void Update()
    {
        // Calls the direction function, handles changing direction when pressing keys.
        ChangeDirection();
    }

    // Might wanna replace this with Unity inputs later.
    private void ChangeDirection()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                _direction = Vector2.down;
            }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    _direction = Vector2.right;
                }
                    else if (Input.GetKeyDown(KeyCode.A))
                    {
                        _direction = Vector2.left;
                    }
    }

    // Applies the actual movement to my snake, the Mathf is so that we wont get float numbers and we stay on grid.
    private void Movement()
    {
        float posx = Mathf.Round(this.transform.position.x);
        float posy = Mathf.Round(this.transform.position.y);

        this.transform.position = new Vector3(posx + _direction.x, posy + _direction.y, 0.0f);
    }

    private void FixedUpdate()
    {
        // We apply movement here in fixedUpdate so we can check for collisions.
        Movement();
    }
}
