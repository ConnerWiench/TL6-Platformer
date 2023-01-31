using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaser_enemy : MonoBehaviour
{
    public float damage;

    private float followDistance;
    private float maxVelocity;
    private float currentVelocity;
    private float acceleration;

    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        damage = 1;

        followDistance = 5;
        maxVelocity = 3;
        currentVelocity = 0;
        acceleration = 5;

        target = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        float targetDistance = distance_from_target(target);

        if((MathF.Abs(targetDistance) < followDistance) && (targetDistance > 0)){
            currentVelocity += (acceleration * Time.deltaTime);
        }
        else if((MathF.Abs(targetDistance) < followDistance) && (targetDistance < 0)){
            currentVelocity -= (acceleration * Time.deltaTime);
        }
        else{
            if(currentVelocity > 0){
                currentVelocity -= (acceleration * Time.deltaTime);
            }
            else if(currentVelocity < 0){
                currentVelocity += (acceleration * Time.deltaTime);
            }
            if(MathF.Abs(currentVelocity) < 1){
            currentVelocity = 0;
            }
        }

        if(currentVelocity > maxVelocity){
            currentVelocity -= (acceleration * Time.deltaTime);
        }
        else if(currentVelocity < -maxVelocity){
            currentVelocity += (acceleration * Time.deltaTime);
        }

        transform.position += (Vector3.right * currentVelocity * Time.deltaTime);

    }

    private float distance_from_target(GameObject target){
        float myPosition = gameObject.transform.position[0];
        float targetPosistion = target.transform.position[0];

        return targetPosistion - myPosition;
    }

    private void OnCollisionEnter2D(Collision2D hit){
        if(hit.gameObject.tag == "Player"){
            Debug.Log("Hit Player");
            // Destroy(gameObject);
        }
    }
}
