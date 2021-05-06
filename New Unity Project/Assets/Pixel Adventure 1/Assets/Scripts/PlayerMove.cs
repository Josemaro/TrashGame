using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //VALORES DE MOVIMIENTO
    public float runSpeed=2;
    public float jumSpeed=3;
    //REFERENCIA AL CUERPO DE PERSONAJE 
    public float doubleJumpspeed = 2.5f;

    private bool canDoubleJump;
    Rigidbody2D rb2D;

    public SpriteRenderer spriteRenderer;


    //SALTO x DIFERENCIA DE PRESIONA EN SPACE
    public bool betterJump = false;
    public float fallmultiplier = 0.5f;
    public float lowJumpMultiplier = 1f; 

    public Animator animator;


    void Start()
    {
    rb2D = GetComponent<Rigidbody2D>();    
    }

    //ACTUALIZACION DEL DOBLE SALTO
    private void Update() 
    {

        //SALTO DOBLE
        if(Input.GetKey("space")){
            if(CheckGround.isGrounded){
                canDoubleJump = true;
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumSpeed);
            }    
            else{
                if (Input.GetKeyDown("space"))
                {
                    if (canDoubleJump)
                    {
                        animator.SetBool("DoubleJump", true);
                        rb2D.velocity = new Vector2(rb2D.velocity.x, doubleJumpspeed);
                        canDoubleJump = false;

                    }
                }
            }
        }

//COMPROBACION DEL SUELO PARA CUERPO - PERSONAJE
        if(CheckGround.isGrounded==false){
            animator.SetBool("Jump",true);
            animator.SetBool("Run",false);
        }

        if(CheckGround.isGrounded==true){
            animator.SetBool("Jump",false);
            animator.SetBool("DoubleJump",false);
            animator.SetBool("Falling",false);
        }

        if(rb2D.velocity.y<0){
            animator.SetBool("Falling",true);
        }
        else if(rb2D.velocity.y >0){
            animator.SetBool("Falling",false);
        }
    }
    
    void FixedUpdate()
    {
        //MOVIMIENTO HACIA LA DERECHA
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2D.velocity= new Vector2(runSpeed,rb2D.velocity.y);
            spriteRenderer.flipX = false;
            animator.SetBool("Run",true);
        }
        //MOVIMIENTO HACIA LA IZQUIERDA
        else if(Input.GetKey("a") || Input.GetKey("left"))
        {
            //PARA QUE EL MOVIMIENTO SEA CONTRARIO, runSpeed se coloca negativo
            rb2D.velocity= new Vector2(-runSpeed,rb2D.velocity.y);
            spriteRenderer.flipX = true;
            animator.SetBool("Run",true);
        }
        else
        {
            //PARA QUE EL PERSONAJE SE QUEDE ESTATICO O INERTE
            rb2D.velocity= new Vector2(0, rb2D.velocity.y);           
            animator.SetBool("Run",false);
        }
 


        //Salto x mayor o menor PRESION de space 
        if(betterJump){
            if(rb2D.velocity.y<0)
            {
                rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallmultiplier) * Time.deltaTime;
            }
            if(rb2D.velocity.y>0 && !Input.GetKey("space"))
            {
                 rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallmultiplier) * Time.deltaTime;
            }
        }

    }
}
