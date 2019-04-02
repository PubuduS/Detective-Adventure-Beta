using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    //public Inventory inventory;
    private static playerController instance;

    public static playerController MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<playerController>();
            }

            return instance;
        }
    }



    [SerializeField]
    float speedX = 10.0f;
    [SerializeField]
    float speedY = 10.0f;
    Rigidbody2D myRigidBody2d;
    Animator myAnimator;
    SpriteRenderer mySpriteRenderer;
    Collider2D myCollider2D;
    Collider compare;

    // Use this for initialization
    void Start()
    {
        myRigidBody2d = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        myCollider2D = GetComponent<Collider2D>();
        compare = GetComponent<Collider>();
       

    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
        flipSprite();
    }

    void Run()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
        Vector2 runVelocity = new Vector2(directionX * speedX, myRigidBody2d.velocity.y);
        myRigidBody2d.velocity = runVelocity;

        bool movingX = Mathf.Abs(myRigidBody2d.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", movingX);
    }

    void Jump()
    {
        if ((myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) && (Input.GetButtonDown("Jump")))
        {
            Vector2 jumpVelocity = new Vector2(0.0f, speedY);
            myRigidBody2d.velocity += jumpVelocity;
        }
        bool movingY = Mathf.Abs(myRigidBody2d.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Jumping", movingY);
    }

    void flipSprite()
    {
         bool flipSprite = (mySpriteRenderer.flipX ?
                           (myRigidBody2d.velocity.x) > 0.5 :
                           (myRigidBody2d.velocity.x < -0.5f));

        if (flipSprite)
        {
            mySpriteRenderer.flipX = !mySpriteRenderer.flipX;
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        compare = other.GetComponent<Collider>();
        if(compare.GetType() == typeof(CapsuleCollider)){
            Debug.Log("start animation");
        }

        if (other.tag == "Item") {
            //inventory.AddItem(other.GetComponent<Item>());

        }
    }


}
