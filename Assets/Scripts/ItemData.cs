using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewItemData",menuName ="Item Data",order = 51)]//Добавление в меню Assets
public class ItemData : ScriptableObject
{
    [SerializeField]
    private string itemName;
    [SerializeField]
    private int itemChance;
    [SerializeField]
    private int itemScore;

    public string ItemName
    {
        get
        {
            return itemName;
        }
    }

    public int ItemChance
    {
        get
        {
            return itemChance;
        }
    }
}
