using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour
{

    private float horizontalVelocity;
    private float verticalVelocity;
    private float acceleration;
    
    private bool onGround;
    private bool Grounded;

    [SerializeField]
    private float maxVelocity;
    // Start is called before the first frame update
    void Start()
    {
        acceleration = 10;

        horizontalVelocity = 0;
        verticalVelocity = 0;

        onGround = false;
    }

    private void Jump(){    // jump animation 
            if(Grounded == true){
                verticalVelocity = maxVelocity * 2;
                Grounded = false;
            }else{
                //do nothing...
                //this is to only allow one jump
            }
    }

    private void Left(){
        horizontalVelocity -= (acceleration * Time.deltaTime);
    }

    private void Right(){
        horizontalVelocity += (acceleration * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (horizontalVelocity > 0.01) {
            transform.localScale = Vector3.one;
        }
        else if (horizontalVelocity < -0.01) {
            transform.localScale = new Vector3(-1,1,1);
        }

        if(horizontalVelocity > 0){
            horizontalVelocity -= (acceleration * Time.deltaTime);
        }
        else if(horizontalVelocity < 0){
            horizontalVelocity += (acceleration * Time.deltaTime);
        }
        if(MathF.Abs(horizontalVelocity) < 1){
            horizontalVelocity = 0;
        }
            // Protects the player from going faster than maxVelocity
        if(horizontalVelocity > maxVelocity){
            horizontalVelocity -= (acceleration * Time.deltaTime);
        }
        else if(horizontalVelocity < -maxVelocity){
            horizontalVelocity += (acceleration * Time.deltaTime);
        }
        // ----- Handles wheather the player can jump, and jump input. -----
            // Simulates gravity for player object.
        if(!onGround){
            verticalVelocity -= 2 * (acceleration * Time.deltaTime);
        }
        // ----- Applies the changes to posistion -----
        transform.Translate(Vector3.right * horizontalVelocity * Time.deltaTime);
        transform.Translate(Vector3.up * verticalVelocity * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D coll){

        if (coll.gameObject.tag == "Floor"){
            Grounded = true;
        }

        // ----- Checks Every collided object to see if any are below and therefore "ground" -----
        foreach(ContactPoint2D hit in coll.contacts){
            if(hit.point.y < transform.position.y){
                onGround = true;
                
                if(verticalVelocity < 0){
                    verticalVelocity = 0;
                }
                // Debug.Log("onGround set to true");
            }
            else if((hit.point.y > transform.position.y) && (verticalVelocity > 0)){
                verticalVelocity = 0;
            }
            else if((hit.point.x > transform.position.x) && (horizontalVelocity > 0)){
                horizontalVelocity = 0;
            }
            else if((hit.point.x < transform.position.x) && (horizontalVelocity < 0)){
                horizontalVelocity = 0;
            }
        }
        // -----
    }
}
