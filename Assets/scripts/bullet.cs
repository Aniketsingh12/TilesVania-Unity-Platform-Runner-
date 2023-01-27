using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float bulletspeed = 20f;
    Rigidbody2D myrigid2d;
    float xSpeed;
    playermove player;
    void Start()
    {
        myrigid2d = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<playermove>();
        xSpeed = player.transform.localScale.x * bulletspeed;
    }

    
    void Update()
    {
        myrigid2d.velocity = new Vector2 (xSpeed, 0f);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
