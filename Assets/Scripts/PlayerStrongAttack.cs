using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrongAttack : MonoBehaviour
{
    private GameObject attackArea = default;
    private bool attacking = false;
    private float timeToAttack = 0.5f;
    private float timer = 0f;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        attackArea = transform.GetChild(1).gameObject;
	anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)){
            Attack();
        }
        if(attacking){
            timer += Time.deltaTime;
            if(timer >= timeToAttack){
                timer = 0f;
                attacking = false;
                attackArea.SetActive(attacking);
		anim.SetBool("StrongAttack", false);
            }
        }
    }
    
    private void Attack(){
        attacking = true;
        attackArea.SetActive(attacking);
	anim.SetBool("StrongAttack", true);
    }
}
    