using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3.0f;
    public float maxRight = 7;
    public float maxLeft = -7;
    public float maxUp = 4;
    public float maxDown = -4.5f;

    public int maxHealth = 4;
    public int currentHealth;

    Hearts hearts;
    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        hearts = GetComponent<Hearts>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector2 position = transform.position;
        if (horizontal == 1 && position.x >= maxRight || horizontal == -1 && position.x <= maxLeft)
        {
            horizontal = 0;
        }
        if (vertical == 1 && position.y >= maxUp || vertical == -1 && position.y <= maxDown)
        {
            vertical = 0;
        }
        position.x += speed * horizontal * Time.deltaTime;
        position.y += speed * vertical * Time.deltaTime;
        transform.position = position;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = hearts.GetHealth(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }



    void OnEnable() {
        TeleporterZone.OnTeleport += Teleport;
        RoomLayerOuter.OnSpawn += Goto;
    }
    void OnDisable() {
        TeleporterZone.OnTeleport -= Teleport;
        RoomLayerOuter.OnSpawn -= Goto;
    }


    void Teleport(Vector2 pos) {
        Vector3 position = transform.position;
        position.x += pos.x;
        position.y += pos.y;
        transform.position = position;
    }

    void Goto(Vector2 pos) {
        Vector3 position = transform.position;
        position.x = pos.x;
        position.y = pos.y;
        transform.position = position;
    }
}
