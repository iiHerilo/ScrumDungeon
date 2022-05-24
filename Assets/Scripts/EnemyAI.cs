using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int health = 2;
    public float speed = 4.0f;
    public float sightRange = 15f;

    Rigidbody2D physics;
    GameObject Player;

    bool active = false;


    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 diffs = new Vector2((transform.position.x - Player.transform.position.x), (transform.transform.position.y - Player.transform.position.y));
        // Enemy only moves if player is within a certain range
        if(Mathf.Abs(Mathf.Sqrt((diffs.x * diffs.x) + (diffs.y * diffs.y))) <= sightRange) {
            float theta = Mathf.Atan(diffs.y / diffs.x);
            // For some reason omitting this causes the enemy to run in the wrong direction
            if(diffs.x <= 0) 
                theta += Mathf.PI;
            Vector2 velocity = new Vector2(Mathf.Cos(theta) * speed, Mathf.Sin(theta) * speed);
            physics.velocity = -velocity;
        }
        
        
    }

    void ActivateAI() {

    }
}
