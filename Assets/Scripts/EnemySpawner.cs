using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> PossibleMonsters = new List<GameObject>();
    public float ChanceToFail = 0.01f;
    public float SpawnInRange;
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 diffs = new Vector2((transform.position.x - Player.transform.position.x), (transform.transform.position.y - Player.transform.position.y));
        // Enemy spawns if player is within a certain range
        if(Mathf.Abs(Mathf.Sqrt((diffs.x * diffs.x) + (diffs.y * diffs.y))) <= SpawnInRange) {
            if(Random.value > ChanceToFail) {
                GameObject Enemy = Instantiate(PossibleMonsters[(int)(Random.value * PossibleMonsters.Count)], transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
