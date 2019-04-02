using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : MonoBehaviour
{
    public int health = 60;
    public float speed;
    private float distance = 2f;
    private bool movingRight = true;
    public Transform groundDetection;




    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 10;
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        /*transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        Debug.Log("before if");
        if (groundInfo.collider == false)
        {
            Debug.Log("no ground");

            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;

            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }*/

        if (Physics2D.Raycast(groundDetection.position, transform.TransformDirection(Vector3.down), Mathf.Infinity, layerMask))
        {



        }
        else{
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;

            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }

    }

    public void takeDamage(int Damage){
        health -= Damage;

        if(health<=0){
            Die();
        }
    }

    void Die(){
        Destroy(gameObject); 
    }
}
