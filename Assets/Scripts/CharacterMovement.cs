using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public float maxRight = 7;
    public float maxLeft = -7;
    public float maxUp = 4;
    public float maxDown = -4.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
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
