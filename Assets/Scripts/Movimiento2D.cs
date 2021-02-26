using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movimiento2D : MonoBehaviour
{
    SceneManager scene;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;
    bool super = false;
    public Sprite Super;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D (Collider2D collider) {
        if(collider.CompareTag("Power up")){
            Destroy(collider.gameObject);
            super = true;
        }

        if(collider.CompareTag("Enemy")){
            if(super == true){
                Destroy(collider.gameObject);
            } else {
                Destroy(gameObject);
            }
        }
    }

    void FixedUpdate()
    {   
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("IsSuper",super);
        // if (Mathf.Abs(rb.velocity.y) >= 0.05f){
        //         animator.SetBool("IsJumping",true);
        //     } else {
        //         animator.SetBool("IsJumping",false);
        //     }

        if (rb && Input.GetAxis("Horizontal") > 0){
            if (super != true){
                rb.AddForce(new Vector2(5,0));
            } else{
                rb.AddForce(new Vector2(8,0));
            }
            spriteRenderer.flipX = false;
        }

        if (rb && Input.GetAxis("Horizontal") < 0){
            if (super != true){
                rb.AddForce(new Vector2(-5,0));
            } else{
                rb.AddForce(new Vector2(-8,0));
            }
            spriteRenderer.flipX = true;
        }

        if (rb && Input.GetButtonDown("Jump")){
            if (Mathf.Abs(rb.velocity.y) < 0.05f){
                rb.AddForce(new Vector2(0,5), ForceMode2D.Impulse);
            }
        }
    }

}
