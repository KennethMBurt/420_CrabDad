using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Basic Pufferfish enemy script
public class Pufferfish : MonoBehaviour
{
    //Attributes
    public Transform anchor;
    public int health;
    public bool ishurt;
    public bool isattack;
    public bool crittime;
    
    
    PlayerDetector detector;
    Animator anim;
    Collider2D body;
    private void Start()
    {
        body = GetComponent<Collider2D>();
        detector = GetComponent<PlayerDetector>();
        anim = GetComponent<Animator>();
        ishurt = false;
        isattack = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Get the player and check it is the player
        var collided = collision.collider.GetComponent<PlayerMovement>();
        if (collided != null)
        {
            
        }

    }

    private void Update()
    {
        //Weve seen the player do the attack
        if (detector.Target != null && !ishurt && !isattack)
        {
            isattack = true;
            anim.SetBool("Attacking", true);
        }
        else if(isattack)
        {
            isattack = false;
        }
    }

    public void EndAttack()
    {
        isattack = false;
        anim.SetBool("Attacking", false);
        anim.Play("Idle");
    }
    public void HurtPuffer()
    {
        //Only Hurt if we can
        if (!ishurt)
        {
            isattack = false;
            anim.SetBool("Attacking", false);

            ishurt = true;
            anim.SetBool("Hurt", true);

            //If weve Blown Up Insta Kill So the hurt animation doesnt need to be messed with :p
            if (crittime)
            {
                anim.SetBool("Dead", true);
            }
        }
    }

    public void SetCritTime()
    {
        crittime = true;
    }

    public void EndCritTime()
    {
        crittime = false;
    }

    public void EndHurt()
    {
        if(health <= 0)
            anim.SetBool("Dead", true);
        else
        {
            ishurt = false;
            anim.SetBool("Hurt", false);
        }
    }
}
