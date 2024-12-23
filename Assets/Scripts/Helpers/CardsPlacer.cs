using System.Collections.Generic;
using UnityEngine;

public class CardsPlacer : MonoBehaviour
{
    [SerializeField]
    GameObject cardPrefab;

    [SerializeField]
    Transform rowParent;

    [SerializeField]
    Transform boardParent;


    private List<Card> cards= new List<Card>();
    private int cardIndex = 0;
    public List<Card> InstantiateCardsInPositions(int rows , int columns , List<int> _cardIDs , CardsData _cardsData)
    {
        for (int i = 0; i < rows; i++)
        {
            Transform currentRaw = Instantiate(rowParent, boardParent);
            for (int j = 0; j < columns; j++)
            {
                GameObject cardObj = Instantiate(cardPrefab, currentRaw);
                Card card = cardObj.GetComponent<Card>();

                int cardID = _cardIDs[cardIndex];
                int frontSpriteIndex = cardID % (_cardsData.CardsFront.Length );

                card.Initialize(cardID, _cardsData.CardsFront[frontSpriteIndex],_cardsData.CardsBack);
                cards.Add(card);
                cardIndex++;
            }
        }

        return cards;
    }
}