using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSwitch : MonoBehaviour
{
    public delegate void SpawnTrigger(Sprite character);
    public static event SpawnTrigger OnSwitch;

    public Sprite Character;

    void Start()
    {
        Button b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(delegate() { Next(); });
    }

    void Next() {
        OnSwitch(Character);
    }
}
