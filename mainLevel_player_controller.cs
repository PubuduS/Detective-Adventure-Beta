using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainLevel_player_controller : MonoBehaviour {

   

    [SerializeField]
    float speedX = 10.0f;
    float speedY = 10.0f;
    [SerializeField]
    Vector2 position;
    [SerializeField]
    bool treasure_map = false;

    Rigidbody2D myRigidbody2D;
    Animator myAnimator;

	// Use this for initialization
	void Start () {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
        Run();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pinkHouse")
            SceneManager.LoadScene(2);
        else if (collision.gameObject.tag == "pyramidEntry")
            SceneManager.LoadScene(5);
        else if (collision.gameObject.tag == "up-rightHouse")
        {
            SceneManager.LoadScene(3);
        }
        else if (collision.gameObject.tag == "down-right_house" && treasure_map == true)
        {
            SceneManager.LoadScene(4);
        }

        if (collision.gameObject.tag == "treasure-map")
        {
            Destroy(collision.gameObject);
            treasure_map = true;
        }



    }

    void Run()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");
        Vector2 runVelocity =
            new Vector2(directionX * speedX, directionY * speedY);
        myRigidbody2D.velocity = runVelocity;

        Debug.Log("hello");

        if(myRigidbody2D.velocity.x > 0)
        {
            myAnimator.SetBool("right", true);
            myAnimator.SetBool("left", false);
            myAnimator.SetBool("up", false);
            myAnimator.SetBool("down", false);
        }
        else if (myRigidbody2D.velocity.x < 0)
        {
            myAnimator.SetBool("right", false);
            myAnimator.SetBool("left", true);
            myAnimator.SetBool("up", false);
            myAnimator.SetBool("down", false);
        }
        else if (myRigidbody2D.velocity.y > 0)
        {
            myAnimator.SetBool("right", false);
            myAnimator.SetBool("left", false);
            myAnimator.SetBool("up", true);
            myAnimator.SetBool("down", false);
        }
        else if (myRigidbody2D.velocity.y < 0)
        {
            myAnimator.SetBool("right", false);
            myAnimator.SetBool("left", false);
            myAnimator.SetBool("up", false);
            myAnimator.SetBool("down", true);
        }
    }


}
