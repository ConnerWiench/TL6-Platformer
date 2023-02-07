using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    [SerializeField]
    HealthBar healthBar;

    [SerializeField]
    private float maxVelocity;
    
    private float horizontalVelocity;
    private float verticalVelocity;
    private float acceleration;
    
    private bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        acceleration = 10;

        horizontalVelocity = 0;
        verticalVelocity = 0;

        onGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ----- Handles Horizontal Movement Input -----
        if(Input.GetKey(KeyCode.D)){
            horizontalVelocity += (acceleration * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.A)){
            horizontalVelocity -= (acceleration * Time.deltaTime);
        }
            // If not moving, decellerate
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

            // Protects the player from going faster than maxVelocity
        if(horizontalVelocity > maxVelocity){
            horizontalVelocity -= (acceleration * Time.deltaTime);
        }
        else if(horizontalVelocity < -maxVelocity){
            horizontalVelocity += (acceleration * Time.deltaTime);
        }
        // -----

        // ----- Handles wheather the player can jump, and jump input. -----
            // Reset verticalVelocity when you reach the ground.
        if(onGround){
            verticalVelocity = 0;
        }
            // Simulates gravity for player object.
        else{
            verticalVelocity -= 2 * (acceleration * Time.deltaTime);
        }

            // Does the action of Jumping
        if(Input.GetKey(KeyCode.Space) && onGround){
            Debug.Log("Jump");
            verticalVelocity = maxVelocity * 2;
        }
        // ----- 

        // ----- Applies the changes to posistion -----
        transform.position += (Vector3.right * horizontalVelocity * Time.deltaTime);
        transform.position += (Vector3.up * verticalVelocity * Time.deltaTime);
        // -----

        // Debug.Log(gameObject.transform.position);
        if(transform.position.y <= -7){
            healthBar.Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D coll){

        // ----- Checks Every collided object to see if any are below and therefore "ground" -----
        foreach(ContactPoint2D hit in coll.contacts){
            if(hit.point.y < transform.position.y){
                onGround = true;
                // Debug.Log("onGround set to true");
            }
        }
        // -----
    }

    private void OnCollisionExit2D(Collision2D coll){
        
        // ----- onGround sets to false if no longer colliding with object below player -----
        onGround = false;
        foreach(ContactPoint2D hit in coll.contacts){
            if(hit.point.y < transform.position.y){
                onGround = true;
                // Debug.Log("onGround set to false");
            }
        }
        // -----
    }    
}
