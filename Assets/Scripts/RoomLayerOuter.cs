using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomLayerOuter : MonoBehaviour
{
    public delegate void SpawnTrigger(Vector2 location);
    public static event SpawnTrigger OnSpawn;
    public delegate void SurroundingRooms(Room[,] list, Vector2Int current);
    public static event SurroundingRooms Inform;


    public static Vector2Int RoomDimensions = new Vector2Int(40, 40);
    public List<Tilemap> Rooms = new List<Tilemap>();
    public List<Tilemap> BossRooms = new List<Tilemap>();
    public List<Tile> WallParts = new List<Tile>();
    public Tile Wall;
    public Tile WallTopMid;
    public Tile WallTopBottomRight;
    public Tile WallTopBottomLeft;
    public Tile WallTopRight;
    public Tile WallTopLeft;
    public List<Vector2Int> ExitLocations = new List<Vector2Int>();

    public class Room {
        public GameObject realobj; // The tilemap gameobject as it appears ingame
        public bool occupied = false; // whether the room is occupied or empty
        public int number = -1; // unique room number for determining which rooms are what
        public int seen = 0;
        
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
    Vector2Int CurrentRoom;


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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool TryForRoom(int x, int y, int n, bool gen) {
        string dgbOut = "";
        dgbOut += "Trying for room " + n + " at (" + x + ", " + y + ")" + "\n";
        // If the cell is alreayd occupied
        if (plan[x, y].occupied) {
            Debug.Log(dgbOut + "(" + x + ", " + y + ") was occupied.");
            return false;
        }
        // Chance for the cell to fail with adjacent rooms
        // 1: 0%
        // 2: 50%
        // 3: 75%
        if (!(GetAdjacentRooms(x, y) < 2) && GetAdjacentRooms(x, y) * 25 + 25 < Random.value * 100) {
            Debug.Log(dgbOut + "(" + x + ", " + y + ") Had too many adjacent rooms: " + GetAdjacentRooms(x, y));
            return false;
        }
        if(cRoomCount < 0) {
            Debug.Log(dgbOut + "(" + x + ", " + y + ") not placed because all rooms were placed: " + cRoomCount);
            return false;
        }
        /*if(FlipCoin()) {
            Debug.Log(dgbOut + "Flipped coin vetoed (" + x + ", " + y + ")");
            return false;
        }*/
        if(gen) {
            plan[x, y].occupied = true;
            plan[x, y].number = n;
            if(n == 0)
                CurrentRoom = new Vector2Int(x, y);
            Debug.Log(dgbOut + "Successfully set (" + x + ", " + y + ") as " + n);
        }
        return true;
        
    }
    Vector3Int v3i(int x, int y, int z) {
        return new Vector3Int(x, y, z);
    }

    bool TryForRoom(Vector2Int pos, int n, bool gen) {
        return TryForRoom(pos.x, pos.y, n, gen);
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

    void Generate() {

        cRoomCount = roomCount;
        TryForRoom(StartingRoomCoordinates, 0, true);

        for(int i = 1; i < roomCount; i++) {
            bool gott = false; // for checking if a room has been created at all
            for(int x = 0; x < plan.GetLength(0); x++) {
                for(int y = 0; y < plan.GetLength(1); y++) {
                    if(plan[x, y].number != i-1)
                        continue;
                    gott = true;
                    Vector2Int pointer = new Vector2Int(x, y);
                        if (x - 1 >= 0)
                            if (TryForRoom(x - 1, y, i, true) ) {
                                cRoomCount--;
                                i++;
                                if (FlipCoin()) continue;
                            }
                            
                        if (x + 1 < plan.GetLength(0))
                            if (TryForRoom(x + 1, y, i, true)) {
                                cRoomCount--;
                                i++;
                                if (FlipCoin()) continue;
                            }
                            
                        if (y - 1 >= 0)
                            if (TryForRoom(x, y - 1, i, true)) {
                                cRoomCount--;
                                i++;
                                if (FlipCoin()) continue;
                            }
                            
                        if (y + 1 < plan.GetLength(1))
                            if (TryForRoom(x, y + 1, i, true)) {
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
                        plan[x, y].realobj = Instantiate(Rooms[plan[x, y].number == 0 ? 0 : 1 + (int)(Random.value * (Rooms.Count - 1))].gameObject, new Vector3((x) * (RoomDimensions.x), (y) * (RoomDimensions.y), 0), Quaternion.identity);
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
        SendInfo(Vector2.zero);
    }

    void SendInfo(Vector2 pos) {
        
        if(pos.x > 0) {
            CurrentRoom.x++;
        }
        else if(pos.x < 0) {
            CurrentRoom.x--;
        }
        else if(pos.y > 0) {
            CurrentRoom.y++;
        }
        else if(pos.y < 0) {
            CurrentRoom.y--;
        }
        int x = CurrentRoom.x;
        int y = CurrentRoom.y;
        if(plan[CurrentRoom.x, CurrentRoom.y].seen <= 2)
            plan[CurrentRoom.x, CurrentRoom.y].seen = 2;
        else
            plan[CurrentRoom.x, CurrentRoom.y].seen = 12;

        
        for(int j = 0; j < 4; j++) {
            Vector2Int next = new Vector2Int(x, y);
            switch(j) {
                case 0:
                    next.y++;
                    break;
                case 1:
                    next.y--;
                    break;
                case 2:
                    next.x++;
                    break;
                case 3:
                    next.x--;
                    break;
            }
            if(next.x >= 0 && next.x < plan.GetLength(0) && next.y >= 0 && next.y < plan.GetLength(1) && plan[next.x, next.y].occupied) {
                if(plan[next.x, next.y].seen < 2)
                    plan[next.x, next.y].seen = 1;
                else if(plan[next.x, next.y].seen != 12)
                    plan[next.x, next.y].seen = 11;
            }

        }

        Inform(plan, CurrentRoom);
    }

    void OnEnable() {
        Main.OnStart += Generate;
        TeleporterZone.OnTeleport += SendInfo;
    }
    void OnDisable() {
        Main.OnStart -= Generate;
        TeleporterZone.OnTeleport -= SendInfo;
    }


    void GenerateBossRoom(int n) {
        if(n < 0)
            throw new System.Exception("No available places for boss rooms!");
        for(int x = 0; x < plan.GetLength(0); x++) {
            for(int y = 0; y < plan.GetLength(1); y++) {
                if(plan[x, y].number == 0) {
                    int j = (int)(Random.value * 4);
                    for(int i = 0; i < 4; i++) {
                        j -= ++j >= 4 ? 4 : 0; // keep between 0 and 4 for all directions
                        Vector2Int next = new Vector2Int(x, y);
                        switch(j) {
                            case 0:
                                next.y++;
                                break;
                            case 1:
                                next.y--;
                                break;
                            case 2:
                                next.x++;
                                break;
                            case 3:
                                next.x--;
                                break;
                        }
                        if(next.x >= 0 && next.x < plan.GetLength(0) && next.y >= 0 && next.y < plan.GetLength(1)) {
                            if(GetAdjacentRooms(next.x, next.y) == 1) {
                                x = next.x;
                                y = next.y;
                                plan[x, y].occupied = true;
                                plan[x, y].number = RoomCount * 2;
                                plan[x, y].seen = 10;
                                plan[x, y].realobj = Instantiate(BossRooms[(int)(Random.value * BossRooms.Count)].gameObject, new Vector3((x) * (RoomDimensions.x), (y) * (RoomDimensions.y), 0), Quaternion.identity);
                            }
                        }
                    }
                }
            }
        }
    }
}
