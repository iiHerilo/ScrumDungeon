using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterZone : MonoBehaviour
{
    public delegate void TeleportTrigger(Vector2 location);
    public static event TeleportTrigger OnTeleport;

    public enum Cardinal {
        North,
        South,
        East,
        West
    }

    public Vector2 JumpDistance = new Vector2(6, 4);
    public Cardinal Direction = Cardinal.North;


    void OnTriggerEnter2D(Collider2D other) {
        CharacterMovement chr = other.GetComponent<CharacterMovement>();

        if(chr != null) {
            Debug.Log("Whoop!!!!! at " + Direction);

            Vector2 next = JumpDistance;

            switch(Direction) {
                case Cardinal.North:
                    next.x = 0;
                    break;
                case Cardinal.South:
                    next.x = 0;
                    next.y *= -1;
                    break;
                case Cardinal.East:
                    next.y = 0;
                    next.x *= -1;
                    break;
                case Cardinal.West:
                    next.y = 0;
                    break;
            }
            
            OnTeleport(next);

        }
    }
}
