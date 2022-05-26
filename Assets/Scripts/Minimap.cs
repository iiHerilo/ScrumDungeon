using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Minimap : MonoBehaviour
{
    public Tile roomUntouched;
    public Tile roomCleared;
    public Tile roomCurrent;
    public Tile roomOccupied;
    public Tile roomUnranged;
    public Tile roomBorder;

    public int range = 5;
    Tilemap tilemap;
    class RoomHolder {
        public enum RoomStatus {
            Untouched, // In sight, yet to be cleared
            Cleared, // Discovered and cleared
            Current, // You are Here
            Occupied, // Discovered, not cleared
            Unranged // Out of sight, yet to be cleared
        }
        public RoomStatus status = RoomStatus.Unranged;
    }
    RoomLayerOuter.Room[,] rooms;


    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Lister(RoomLayerOuter.Room[,] plan, Vector2Int current) {
        int half = Mathf.FloorToInt(range/2);
        for(int x = current.x - half, i = 0; x < current.x + half + 1; x++, i++) {
            for(int y = current.y - half, j = -(range); y < current.y + half + 1; y++, j++) {
                if(x < plan.GetLength(0) && x >= 0 && y < plan.GetLength(1) && y >= 0) {
                    Tile next;
                    switch(plan[x,y].seen) {
                        case 1:
                            next = roomUntouched;
                            break;
                        case 2:
                            next = roomCleared;
                            break;
                        default:
                        case 0:
                            next = roomUnranged;
                            break;
                    }
                    if(x == current.x && y == current.y) 
                        tilemap.SetTile(v3i(i, j, 0), roomCurrent);
                    else
                        tilemap.SetTile(v3i(i, j, 0), next);
                }
                else {
                    tilemap.SetTile(v3i(i, j, 0), roomUnranged);
                }
            }
        }
        tilemap.SetTile(v3i(half + 1, half + 1, 0), roomCurrent);
    }

    void OnEnable() {
        RoomLayerOuter.Inform += Lister;
    }
    void OnDisable() {
        RoomLayerOuter.Inform -= Lister;
    }


    Vector3Int v3i(int x, int y, int z) {
        return new Vector3Int(x, y, z);
    }

}
