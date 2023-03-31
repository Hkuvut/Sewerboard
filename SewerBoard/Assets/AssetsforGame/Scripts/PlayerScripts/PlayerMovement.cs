using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public bool canMount;
    public GameObject Board;
    private bool canMove = true;
    public bool held;
    [SerializeField]
    private int MoveMode;
    // 1 is left; 2 is idle; 3 is right.

    



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hi");
        int BoardLayer = LayerMask.NameToLayer("Board");
        int PlayerLayer = LayerMask.NameToLayer("Player");
        int BoardHeldLayer = LayerMask.NameToLayer("BoardHeld");
        Board.layer = BoardLayer;
        this.gameObject.layer = PlayerLayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveMode == 1)
        { if (this.gameObject.GetComponent<SpriteRenderer>().flipX == false)
            {
            
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            if (held == true)
                {
                    Board.transform.localPosition = new Vector2(-0.2f, 0f);
                }
            }
        }

        if (MoveMode == 3)
        {
            if (this.gameObject.GetComponent<SpriteRenderer>().flipX == true)
            {

                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                if (held == true)
                {
                    Board.transform.localPosition = new Vector2(0.2f, 0f);
                }
            }
        }
        if (held == false && Input.GetKeyDown(KeyCode.Q) && Board.GetComponent<Movement>().OnSkateBoard == false)

        {
            int BoardHeldLayer = LayerMask.NameToLayer("BoardHeld");
            Board.layer = BoardHeldLayer;
            Board.transform.parent = this.transform;
            Board.transform.localPosition = new Vector2(0, 0.2f);
            Board.transform.localRotation = Quaternion.Euler(0, 0, 180);
            Board.GetComponent<Rigidbody2D>().isKinematic = true;
            held = true;
            Debug.Log("held");



        }

        else if (held == true && Input.GetKeyDown(KeyCode.Q) && Board.GetComponent<Movement>().OnSkateBoard == false)
        {
            int BoardLayer = LayerMask.NameToLayer("Board");
            held = false;
            Board.transform.localRotation = Quaternion.Euler(0, 0, 0);
            Board.GetComponent<Rigidbody2D>().isKinematic = false;
            Board.transform.parent = null;
            Debug.Log("dropped");
            Board.layer = BoardLayer;
        }


        if (Board.GetComponent<Movement>().OnSkateBoard == true)

        {
            this.transform.localPosition = new Vector2(0, 0);



        }
        //detects if player is in the vicinity of the board, and allows player to press E to mount it.
        if (canMount == true && Board.GetComponent<Movement>().OnSkateBoard == false && held == false)

        {
            if (Input.GetKeyDown(KeyCode.E))

            {
                Debug.Log("Goton");

                Mount();
                canMove = false;
                rb.isKinematic = true;
                Board.GetComponent<Movement>().OnSkateBoard = true;

            }

        }
        else if (Board.GetComponent<Movement>().OnSkateBoard == true && held == false)

        {
            if (Input.GetKeyDown(KeyCode.E))

            {
                Debug.Log("Gotoff");
                this.gameObject.transform.parent = null;
                this.transform.position = new Vector2(this.transform.position.x, Board.transform.position.y);
                //this.transform.localPosition = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y + 1);
                Board.GetComponent<Movement>().OnSkateBoard = false;
                canMove = true;
                rb.isKinematic = false;

            }

        }


        if (canMove == true)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(Vector2.left * speed * Time.deltaTime);
                MoveMode = 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(Vector2.right * speed * Time.deltaTime);
                MoveMode = 3;
            }
        }
        //if (!Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.D))
        //{
        //    MoveMode = 2;
        //}





    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BoardDetect"))

        {
            canMount = true;
        }

    }


    void Mount()
    {
        if (canMount == true)
        {
            this.gameObject.transform.parent = Board.transform;
            this.transform.localPosition = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y + 1);
            Board.GetComponent<Movement>().OnSkateBoard = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BoardDetect"))

        {
            canMount = false;
            //Board = collision.gameObject.transform.parent.gameObject;
            

        }

    }
    



}
