using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_bullet : MonoBehaviour
{
    private float maxVelocity;
    private float timeCreated;

    private GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0];

        maxVelocity = 5;
        timeCreated = Time.time;
    }

    void Update()
    {
        transform.position += (transform.up * maxVelocity * Time.deltaTime);

        // The bullet self-terminates after 6 seconds
        if((Time.time - timeCreated) > 6){
            Destroy(gameObject);
        }
    }

    // When the bullet hits any non-enemy object
    private void OnTriggerEnter2D(Collider2D other) {
        // Debug.Log("Trigger Active");
        if(!other.CompareTag("Enemy")){
            Destroy(gameObject);
        }
    }
}