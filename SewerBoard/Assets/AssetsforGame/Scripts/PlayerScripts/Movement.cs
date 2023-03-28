using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    public groundCheck gc;

    public float jumpForce;

    public float downForce;

    public bool OnSkateBoard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        rb.AddForce(Vector2.down * (downForce) * Time.deltaTime);

    if (OnSkateBoard == true) { 
        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jump");
            if (gc.grounded == true) {
                rb.AddForce(Vector2.up * jumpForce);
                Debug.Log("Jump");
            }
        }
    }

    }

    
}
