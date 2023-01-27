using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemtmove : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D myrigid2d;
    BoxCollider2D boxcolider;
    [SerializeField] float speed = 1f;
    void Start()
    {
        myrigid2d = GetComponent<Rigidbody2D>();
        boxcolider = GetComponent<BoxCollider2D>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
        myrigid2d.velocity = new Vector2(speed, 0f);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        speed = -speed;
        flipEnemy();
    }
    void flipEnemy()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myrigid2d.velocity.x)), 1f);

    }
}
