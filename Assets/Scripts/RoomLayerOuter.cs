using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomLayerOuter : MonoBehaviour
{
    public delegate void SpawnTrigger(Vector2 location);
    public static event SpawnTrigger OnSpawn;

    public static Vector2Int RoomDimensions = new Vector2Int(22, 14);
    public List<Tilemap> Rooms = new List<Tilemap>();
    public List<Tile> WallParts = new List<Tile>();
    public Tile Wall;
    public Tile WallTopMid;
    public Tile WallTopBottomRight;
    public Tile WallTopBottomLeft;
    public Tile WallTopRight;
    public Tile WallTopLeft;
    public List<Vector2Int> ExitLocations = new List<Vector2Int>();

    class Room {
        public GameObject realobj; // The tilemap gameobject as it appears ingame
        public bool occupied = false; // whether the room is occupied or empty
        public int number = -1; // unique room number for determining which rooms are what
    }

    public Vector2Int StartingRoomCoordinates = new Vector2Int();
    public int roomCount = 10;
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
    Room[,] plan;


    // Start is called before the first frame update
    void Start()
    {   
        plan = new Room[roomCount, roomCount];

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
        string s = "ROOMS:\n";
        for(int i = 0; i < plan.GetLength(0); i++) {
            for(int j = 0; j < plan.GetLength(1); j++) {
                s += "[" + (plan[i, j].occupied ? plan[i, j].number.ToString() : " ") + "]";
            }
            s += "\n";
        }
        Debug.Log(s);

        
        for(int x = 0; x < plan.GetLength(0); x++) {
            for(int y = 0; y < plan.GetLength(1); y++) {
                if(plan[x, y].occupied)
                    {
                        plan[x, y].realobj = Instantiate(Rooms[plan[x, y].number == 0 ? 0 : (int)(Random.value * Rooms.Count)].gameObject, new Vector3((x) * (RoomDimensions.x), (y) * (RoomDimensions.y), 0), Quaternion.identity);
                        plan[x, y].realobj.transform.parent = gameObject.transform;

                        if(plan[x, y].number == 0) {
                            OnSpawn(new Vector2((x) * (RoomDimensions.x), (y) * (RoomDimensions.y)));
                        }

                        Tilemap tilemap = plan[x, y].realobj.GetComponent<Tilemap>();
                        
                        // exit locations
                        Vector3Int[] north = {v3i(-1, 5, 0), v3i(0, 5, 0)};
                        Vector3Int[] east = {v3i(-9, 0, 0), v3i(-9, -1, 0)};
                        Vector3Int[] south = {v3i(-1, -6, 0), v3i(0, -6, 0)};
                        Vector3Int[] west = {v3i(8, 0, 0), v3i(8, -1, 0)};
                        for(int i = 0; i < 2; i++) {
                                if(!(y + 1 < plan.GetLength(1) && plan[x, y + 1].occupied)) {
                                    tilemap.SetTile(north[i], Wall);
                                    tilemap.SetTile(v3i(north[i].x, north[i].y+1, 1), WallTopMid);
                                    tilemap.SetTile(v3i(north[i].x-1, north[i].y, 0), Wall);
                                    tilemap.SetTile(v3i(north[i].x+1, north[i].y, 0), Wall);
                                }
                                if(!(x - 1 >= 0 && plan[x - 1, y].occupied)) {
                                    tilemap.SetTile(east[i], WallTopRight);
                                    tilemap.SetTile(v3i(east[i].x, east[i].y, 0), WallTopRight);
                                    tilemap.SetTile(v3i(east[i].x, east[i].y + 1, 0), WallTopRight);
                                }
                            
                                if(!(y - 1 >= 0 && plan[x, y - 1].occupied)) {
                                    tilemap.SetTile(south[i], Wall);
                                    tilemap.SetTile(v3i(south[i].x, south[i].y+1, 1), WallTopMid);
                                    tilemap.SetTile(v3i(south[i].x-1, south[i].y, 0), Wall);
                                    tilemap.SetTile(v3i(south[i].x+1, south[i].y, 0), Wall);
                                }
                                if(!(x + 1 < plan.GetLength(0) && plan[x + 1, y].occupied)) {
                                    tilemap.SetTile(west[i], WallTopLeft);
                                    tilemap.SetTile(v3i(west[i].x, west[i].y + 1, 0), WallTopLeft);
                                }
                        }

                        
                    }
            }
        }

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
    Vector3Int v3i(int x, int y, int z) {
        return new Vector3Int(x, y, z);
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
