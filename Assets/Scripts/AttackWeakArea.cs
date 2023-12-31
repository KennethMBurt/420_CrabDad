using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWeakArea : MonoBehaviour
{
    private int damage = 3;

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.GetComponent<Health>() != null){
            Health health = collider.GetComponent<Health>();
            health.Damage(damage);
        }

        //If PufferFish Call Hurt
        var puffer = collider.GetComponent<Pufferfish>();
        if(puffer != null)
        {
            puffer.HurtPuffer(damage);
        }
    }

}
