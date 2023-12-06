using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float SPEED = 14f;
    [SerializeField] private float SPRINT_SPEED = 20f;

    [SerializeField] private float JUMP_SPEED = 10f;

    float horizontalInput;
    bool jumpInput;

    private Animator anim;

    [SerializeField] private float dashingVelocityX = 40f;
    [SerializeField] private float dashingVelocityY = 40f;
    private float dashingTime = 0.1f;
    private Vector2 dashingDir;
    private bool isDashing = false;
    //private bool canDash = true;

    bool goingUp = false;
    bool falling = false;

    // Start is called before the first frame update
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
	    anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(isDashing)
            return;
	
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Dash") /*&& canDash*/){
            StartCoroutine(Dash());
        }
        

        //if(isGrounded()){
        //    canDash = true;
        //}
        Move();
        Jump();
	UpdateAnimationUpdate();
    }

    private void FixedUpdate(){
        
    }

    private void Move(){
        if(Input.GetKey("left shift")){
            body.velocity = new Vector2(SPRINT_SPEED * horizontalInput, body.velocity.y);
	    anim.SetBool("Sprint", true);
	    anim.SetBool("Walking", false);
	} else{
            body.velocity = new Vector2(SPEED * horizontalInput, body.velocity.y);
	    anim.SetBool("Walking", true);
	    anim.SetBool("Sprint", false);
	}
        checkForFlipping();
    }

    private void Jump(){
        if(Input.GetButtonDown("Jump") /* && isGrounded()*/){
            body.velocity = new Vector2(body.velocity.x, JUMP_SPEED);
	}
    }

    private void checkForFlipping(){
        bool movingLeft = body.velocity.x < 0;
        bool movingRight = body.velocity.x > 0;
        if(movingLeft){
            transform.localScale = new Vector2(-.25f,transform.localScale.y);
        }
        else if(movingRight){
            transform.localScale = new Vector2(.25f,transform.localScale.y);
        }
    }

    private IEnumerator Dash(){
        isDashing = true;  
        //canDash = false;
        float yVelocity = body.velocity.y;
        float xVelocity = body.velocity.x;
        dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if(dashingDir == Vector2.zero){
                dashingDir = new Vector2(transform.localScale.x, 0f);
            }
        body.velocity = new Vector2(dashingDir.normalized.x * dashingVelocityX, dashingDir.normalized.y * dashingVelocityY);
        yield return new WaitForSeconds(dashingTime);
        body.velocity = new Vector2(xVelocity, yVelocity);
        isDashing = false;
    }

    private void UpdateAnimationUpdate(){
	if(body.velocity.x == 0){
	    anim.SetBool("Walking", false);
	    anim.SetBool("Sprint", false);
	}
	if(Math.Round(body.velocity.y) > 0 && !goingUp){
	    anim.SetBool("GoingUp", true);
	    anim.SetBool("Falling", false);
	    goingUp = true;
	} else if(Math.Round(body.velocity.y) < 0 && !falling){
	    anim.SetBool("GoingUp", false);
	    anim.SetBool("Falling", true);
	    falling = true;
	} else if(Math.Round(body.velocity.y) == 0){
	    anim.SetBool("GoingUp", false);
	    anim.SetBool("Falling", false);
	    falling = false;
	    goingUp = false;
	}
    }

    public void HurtPlayer(Vector2 knockback, int damage)
    {
        Debug.Log(knockback);
        //Knock Back
        body.AddForce(knockback, ForceMode2D.Impulse);

        //Play anim and Remove health
    }
}
