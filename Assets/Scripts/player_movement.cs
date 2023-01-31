using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    private float maxVelocity;
    private float horizontalVelocity;
    private float verticalVelocity;
    private float acceleration;
    
    private bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        maxVelocity = 5;
        acceleration = 10;

        horizontalVelocity = 0;
        verticalVelocity = 0;

        onGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D)){
            horizontalVelocity += (acceleration * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.A)){
            horizontalVelocity -= (acceleration * Time.deltaTime);
        }
        else{
            if(horizontalVelocity > 0){
                horizontalVelocity -= (acceleration * Time.deltaTime);
            }
            else if(horizontalVelocity < 0){
                horizontalVelocity += (acceleration * Time.deltaTime);
            }
            if(MathF.Abs(horizontalVelocity) < 1){
                horizontalVelocity = 0;
            }
        }

        if(horizontalVelocity > maxVelocity){
            horizontalVelocity -= (acceleration * Time.deltaTime);
        }
        else if(horizontalVelocity < -maxVelocity){
            horizontalVelocity += (acceleration * Time.deltaTime);
        }



        if(onGround){
            verticalVelocity = 0;
        }
        else{
            verticalVelocity -= 2 * (acceleration * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.Space) && onGround){
            Debug.Log("Jump");
            verticalVelocity = maxVelocity * 2;
        }

        transform.position += (Vector3.right * horizontalVelocity * Time.deltaTime);
        transform.position += (Vector3.up * verticalVelocity * Time.deltaTime);

        // Debug.Log(gameObject.transform.position);
    }

    private void OnCollisionEnter2D(Collision2D coll){
        foreach(ContactPoint2D hit in coll.contacts){
            if(hit.point.y < transform.position.y){
                onGround = true;
                // Debug.Log("onGround set to true");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D coll){
        onGround = false;
        foreach(ContactPoint2D hit in coll.contacts){
            if(hit.point.y < transform.position.y){
                onGround = true;
                // Debug.Log("onGround set to false");
            }
        }
    }    
}
