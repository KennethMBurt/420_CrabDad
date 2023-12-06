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
            //Send Em Flying Boys
            Vector2 direction = (collided.gameObject.transform.position - body.transform.position).normalized;
            collided.HurtPlayer(direction * 500f * Time.deltaTime, 1);
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
            ishurt = true;
            isattack = false;

            anim.SetBool("Attacking", false);
            anim.SetBool("Hurt", true);

            //If weve Blown Up Insta Kill So the hurt animation doesnt need to be messed with :p
            if (crittime)
            {
                anim.SetBool("Dead", true);
                anim.SetBool("Hurt", false);
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
        if (health <= 0)
        {
            anim.SetBool("Dead", true);
            anim.SetBool("Hurt", false);
        }
        else
        {
            ishurt = false;
            anim.SetBool("Hurt", false);
            health--;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
