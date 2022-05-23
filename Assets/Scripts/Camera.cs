using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    void OnEnable() {
        TeleporterZone.OnTeleport += Teleport;
    }
    void OnDisable() {
        TeleporterZone.OnTeleport -= Teleport;
    }


    void Teleport(Vector2 pos) {
        Vector2 next = RoomLayerOuter.RoomDimensions;

        if(pos.x > 0) {
            next.y = 0;
        }
        else if(pos.x < 0) {
            next.y = 0;
            next.x *= -1;
        }
        else if(pos.y > 0) {
            next.x = 0;
        }
        else if(pos.y < 0) {
            next.x = 0;
            next.y *= -1;
        }

        Vector3 position = transform.position;
        position.x += next.x;
        position.y += next.y;
        transform.position = position;
    }
}
