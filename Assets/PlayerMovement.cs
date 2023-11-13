using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float SPEED = 14f;
    [SerializeField] private float SPRINT_SPEED = 20f;

    [SerializeField] private float JUMP_SPEED = 10f;

    float horizontalInput;
    bool jumpInput;

    private float dashingVelocity = 40f;
    private float dashingTime = 0.1f;
    private Vector2 dashingDir;
    private bool isDashing;
    //private bool canDash = true;

    // Start is called before the first frame update
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        this.horizontalInput = Input.GetAxisRaw("Horizontal");

        bool dashInput = Input.GetButtonDown("Dash");
        if(dashInput /*&& canDash*/){
            isDashing = true;
            //scanDash = false;
            dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if(dashingDir == Vector2.zero){
                dashingDir = new Vector2(transform.localScale.x, 0f);
            }
            StartCoroutine(StopDashing());
        }

        if(isDashing){
            body.velocity = dashingDir.normalized * dashingVelocity;
            return;
        }

        //if(isGrounded()){
        //    canDash = true;
        //}

        Move();
        Jump();
    }

    private void Move(){
        if(Input.GetKey(KeyCode.LeftShift))
            body.velocity = new Vector2(SPRINT_SPEED * horizontalInput, body.velocity.y);
        else
            body.velocity = new Vector2(SPEED * horizontalInput, body.velocity.y);
        checkForFlipping();
    }

    private void Jump(){
        if(Input.GetButtonDown("Jump"))
            body.velocity = new Vector2(body.velocity.x, JUMP_SPEED);
    }

    private void checkForFlipping(){
        bool movingLeft = body.velocity.x < 0;
        bool movingRight = body.velocity.x > 0;
        if(movingLeft){
            transform.localScale = new Vector2(-1f,transform.localScale.y);
        }
        else if(movingRight){
            transform.localScale = new Vector2(1f,transform.localScale.y);
        }
    }

    private IEnumerator StopDashing(){
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;

    }


}
