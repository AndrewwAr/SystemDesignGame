using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCardsData", menuName = "CardsData")]

public class CardsData : ScriptableObject
{
    [Header("CardsSprites")]
    public Sprite CardsBack;
    public Sprite[] CardsFront;

    
}
