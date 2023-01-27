using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playermove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpspeed = 20f;
    [SerializeField] float climbingspeed = 5f;
    [SerializeField] Vector2 deathjump = new Vector2(20f, 20f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    float gravityatStart;
    Vector2 move;
    Rigidbody2D myrigidbody;
    Animator myanimation;
    CapsuleCollider2D mycapcsule2d;
    BoxCollider2D myfeet;
    bool isAlive = true;
    void Start()
    {
        myrigidbody= GetComponent<Rigidbody2D>();
        myanimation= GetComponent<Animator>();
        mycapcsule2d= GetComponent<CapsuleCollider2D>();
        gravityatStart = myrigidbody.gravityScale;
        myfeet = GetComponent<BoxCollider2D>();


    }
   
    void OnFire(InputValue value)  
    {
      if (!isAlive) { return; }
      Instantiate(bullet, gun.position, transform.rotation);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        run();
        flipplayer();
        climbing();
        Die();
    }
    void climbing()
    {
        if (!myfeet.IsTouchingLayers(LayerMask.GetMask("Ladder"))) {
            myrigidbody.gravityScale = gravityatStart;
            myanimation.SetBool("isclimbing", false);
            return; }
        Vector2 playervelo = new Vector2(myrigidbody.velocity.x, move.y * climbingspeed);
        myrigidbody.velocity = playervelo;
        myrigidbody.gravityScale = 0f;
        bool isplayervertical = Mathf.Abs(myrigidbody.velocity.y) > Mathf.Epsilon;
        myanimation.SetBool("isclimbing", isplayervertical);
    }

    void flipplayer()
    {
        bool isplayerhorixantal = Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;
        transform.localScale= new Vector2(Mathf.Sign(myrigidbody.velocity.x),1f);

    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        move = value.Get<Vector2>();
        Debug.Log(move);
    }
    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }

        if (!myfeet.IsTouchingLayers(LayerMask.GetMask("Ground"))){ return; }
        if(value.isPressed) {
            myrigidbody.velocity += new Vector2(0f, jumpspeed);
        }
    }
    void run()
    {
        Vector2 playervelo = new Vector2(move.x * speed, myrigidbody.velocity.y);
        myrigidbody.velocity = playervelo;
        bool isplayerhorixantal = Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;
        myanimation.SetBool("isrunning", isplayerhorixantal);
    }
    void Die()
    {
        if (mycapcsule2d.IsTouchingLayers(LayerMask.GetMask("enemy","hazards")))
        {
            isAlive = false;
            myanimation.SetTrigger("death");
            myrigidbody.velocity = deathjump;
            FindObjectOfType<GameSession>().processPlayerDeath();
        }
    }
}
