using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomLayerOuter : MonoBehaviour
{
    public Vector2Int RoomDimensions = new Vector2Int(16, 10);
    public List<Tilemap> Rooms = new List<Tilemap>();

    class Room {
        public Tilemap association;
        public bool occupied = false;
        public int number = -1;
    }

    Room[,] plan = new Room[10, 10];
    public Vector2Int StartingRoomCoordinates = new Vector2Int();
    int roomCount = 10;
    int cRoomCount;
    public int RoomCount {
        get {
            return roomCount;
        }
        set {
            if(value < plan.GetLength(0) * plan.GetLength(1)) 
                roomCount = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {   
        for(int i = 0; i < plan.GetLength(0); i++) {
            for(int j = 0; j < plan.GetLength(1); j++) {
                plan[i, j] = new Room();
            }
        }
        StartingRoomCoordinates.x = (int)(Random.value * 10);
        StartingRoomCoordinates.y = (int)(Random.value * 10);
        

        Debug.Log(StartingRoomCoordinates);

        cRoomCount = roomCount;
        TryForRoom(StartingRoomCoordinates, 0);
        Debug.Log("aaaa");

        for(int i = 1; i < roomCount; i++) {
            bool gott = false; // for checking if a room has been created at all
            for(int x = 0; x < plan.GetLength(0); x++) {
                for(int y = 0; y < plan.GetLength(1); y++) {
                    if(plan[x, y].number != i-1)
                        continue;
                    gott = true;
                    Vector2Int pointer = new Vector2Int(x, y);
                        if (x - 1 >= 0)
                            if (TryForRoom(x - 1, y, i) ) {
                                cRoomCount--;
                                i++;
                                if (FlipCoin()) continue;
                            }
                            
                        if (x + 1 < plan.GetLength(0))
                            if (TryForRoom(x + 1, y, i)) {
                                cRoomCount--;
                                i++;
                                if (FlipCoin()) continue;
                            }
                            
                        if (y - 1 >= 0)
                            if (TryForRoom(x, y - 1, i)) {
                                cRoomCount--;
                                i++;
                                if (FlipCoin()) continue;
                            }
                            
                        if (y + 1 < plan.GetLength(1))
                            if (TryForRoom(x, y + 1, i)) {
                                cRoomCount--;
                                i++;
                                if (FlipCoin()) continue;
                            }
                            
                    
                }
            }
            if(gott) i--;
        }
        string s = "";
        for(int i = 0; i < plan.GetLength(0); i++) {
            for(int j = 0; j < plan.GetLength(1); j++) {
                s += "[" + (plan[i, j].occupied ? plan[i, j].number.ToString() : " ") + "]";
            }
            s += "\n";
        }
        Debug.Log(s);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool TryForRoom(int x, int y, int n) {
        Debug.Log("Trying for room (" + x + ", " + y + ") as " + n);
        // If the cell is alreayd occupied
        if (plan[x, y].occupied) {
            Debug.Log("(" + x + ", " + y + ") was occupied.");
            return false;
        }
        // Chance for the cell to fail with adjacent rooms
        // 1: 0%
        // 2: 50%
        // 3: 75%
        if (!(GetAdjacentRooms(x, y) < 2) && GetAdjacentRooms(x, y) * 25 + 25 < Random.value * 100) {
            Debug.Log("(" + x + ", " + y + ") Had too many adjacent rooms: " + GetAdjacentRooms(x, y));
            return false;
        }
        if(cRoomCount < 0) {
            Debug.Log("(" + x + ", " + y + ") not placed because all rooms were placed: " + cRoomCount);
            return false;
        }
        if(FlipCoin()) {
            Debug.Log("Flipped coin vetoed (" + x + ", " + y + ")");
        }
        plan[x, y].occupied = true;
        plan[x, y].number = n;

        Debug.Log("Successfully set (" + x + ", " + y + ") as " + n);
        return true;
        
    }
    bool TryForRoom(Vector2Int pos, int n) {
        return TryForRoom(pos.x, pos.y, n);
    }

    int GetAdjacentRooms(int x, int y) {
        int c = 0;
            if (x - 1 >= 0) {
                if(plan[x - 1, y].occupied)
                    c++;
            }
            if (x + 1 < plan.GetLength(0)) {
                if(plan[x + 1, y].occupied)
                    c++;
            }
            if (y - 1 >= 0) {
                if(plan[x, y - 1].occupied)
                    c++;
            }
            if (y + 1 < plan.GetLength(1)) {
                if(plan[x, y + 1].occupied)
                    c++;
            }
        
        return c;
    }
    bool FlipCoin() {
        return Random.value * 100 < 50;
    }
}
