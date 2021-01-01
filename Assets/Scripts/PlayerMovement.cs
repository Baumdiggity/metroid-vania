using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator _anim;
    Rigidbody2D _body;
    CapsuleCollider2D _cap;
    
    [SerializeField]
    float jumpForce = 30;
    [SerializeField]
    float moveSpeed = 10f;

   bool grounded = false;

    float vert = 0, hor = 0;
    bool jump = false;

    float maxSpeed = 100f;
    float minSpeed = -100f;

    float movement = 0;
    int direction = 0;
    bool isRight = true;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _body = GetComponent<Rigidbody2D>();
        _cap = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        //Read Inputs
        vert = Input.GetAxis("Vertical");
        hor = Input.GetAxis("Horizontal");
        if( grounded && Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }
    void FixedUpdate()
    {
        //Set delta's waiting for input
        movement = 0;
        direction = 0;
        grounded = false;

        //Set a raycast to check whether or not on the ground
        Vector3 max = _cap.bounds.max;
        Vector3 min = _cap.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        if (hit != null)
        {
            grounded = true;
        }

        //Check input orientation
        if (vert != 0)
        {
            if (vert > 0)
            {
                if (hor > 0)
                {
                    direction = 1; //diagup right
                    movement = moveSpeed;
                    isRight = true;
                }
                else if (hor < 0)
                {
                    direction = 7; //diag up left
                    movement = -moveSpeed;
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
                    movement = moveSpeed;
                    isRight = true;
                }
                else if (hor < 0)
                {
                    direction = 5; //diag down left
                    movement = -moveSpeed;
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
                movement = moveSpeed;
                isRight = true;
            }
            else if (hor < 0)
            {
                direction = 6; //left
                movement = -moveSpeed;
                isRight = false;
            }
        }

        //set based on input
        _anim.SetInteger("FireDirection", direction);
        _anim.SetBool("FaceRight", isRight);
        _anim.SetBool("OnGround", grounded);

        if (jump)
        {
            _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Jump");
            jump = false;
            _anim.SetTrigger("Jump");
        }
        _body.velocity = new Vector2(Mathf.Clamp(movement, minSpeed, maxSpeed), Mathf.Clamp(_body.velocity.y, minSpeed, maxSpeed));
        //Debug.Log(_body.velocity);
    }
}
