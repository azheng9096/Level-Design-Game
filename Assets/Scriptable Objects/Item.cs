using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 1)]

public class Item : ScriptableObject
{
    public string itemName;
    public Sprite sprite;

    public AudioClip pickUpAudio;

    public Sprite detailSprite;
    public string description;

    public string pickUpDialogue;
}
