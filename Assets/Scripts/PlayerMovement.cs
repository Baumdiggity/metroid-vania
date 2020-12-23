using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator _anim;
    Rigidbody2D _body;
    [SerializeField]
    float moveSpeed = 10f;

    float vert = 0, hor = 0;
    Vector2 movement = Vector2.zero;
    int direction = 0;
    bool isRight = true;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Set delta's waiting for input
        movement = Vector2.zero;
        direction = 0;

        vert = Input.GetAxis("Vertical");
        hor = Input.GetAxis("Horizontal");


        if (vert != 0)
        {
            if (vert > 0)
            {
                if (hor > 0)
                {
                    direction = 1; //diagup right
                    movement = transform.right * moveSpeed;
                    isRight = true;
                }
                else if (hor < 0)
                {
                    direction = 7; //diag up left
                    movement = -transform.right * moveSpeed;
                    isRight = false;
                }
                else
                {
                    direction = 8; //up
                }
            }
            else if (vert < 0)
            {
                if (hor > 0)
                {
                    direction = 3; //diag down right
                    movement = transform.right * moveSpeed;
                    isRight = true;
                }
                else if (hor < 0)
                {
                    direction = 5; //diag down left
                    movement = -transform.right * moveSpeed;
                    isRight = false;
                }
                else
                {
                    direction = 4; //down
                }
            }

        }
        else if (hor != 0)
        {
            if (hor > 0)
            {
                direction = 2; //right
                movement = transform.right * moveSpeed;
                isRight = true;
            }
            else if (hor < 0)
            {
                direction = 6; //left
                movement = -transform.right * moveSpeed;
                isRight = false;
            }
        }

        //set based on input
        _anim.SetInteger("FireDirection", direction);
        _anim.SetBool("FaceRight", isRight);
        _body.velocity = movement;
    }
}
