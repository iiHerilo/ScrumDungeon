using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int health = 2;
    public float speed = 4.0f;
    public string PlayerName = "Player";

    Vector2 direction;
    Rigidbody2D physics;
    GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(Player);
    }

    // Update is called once per frame
    void Update()
    {
        float theta = Mathf.Atan((transform.transform.position.y - Player.transform.position.y) / (transform.position.x - Player.transform.position.x));
        if(transform.position.x - Player.transform.position.x <= 0) 
            theta += Mathf.PI;
        Vector2 velocity = new Vector2(Mathf.Cos(theta) * speed, Mathf.Sin(theta) * speed);
        physics.velocity = -velocity;
        
    }
}
