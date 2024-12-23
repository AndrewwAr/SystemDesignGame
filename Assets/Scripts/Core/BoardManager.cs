using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    CardsData cardsData;
    [SerializeField]
    int rows = 2;
    [SerializeField]
    int columns = 4;

    private List<Card> cards = new List<Card>();
    private Card selectedCard1, selectedCard2;
    private bool isCheckingMatch;


    private CardsPlacer placer;


    private void Start()
    {
        ShuffleCardsFrontSprites(cardsData.CardsFront);
    }

    public void GenerateBoard()
    {

        placer = FindObjectOfType<CardsPlacer>();

        List<int> cardIDs = new List<int>();

        

        for (int i = 0; i < (rows * columns) / 2; i++)
        {
            cardIDs.Add(i);
            cardIDs.Add(i);
        }

        ShuffleCards(cardIDs);

        cards = placer.InstantiateCardsInPositions(rows, columns, cardIDs , cardsData);


    }

    public void HandleCardSelection(Card selectedCard)
    {
        if (selectedCard.IsMatched) return;

        //selectedCard.FlipCard();

        if (selectedCard1 == null)
        {
            selectedCard1 = selectedCard;
        }
        else if (selectedCard2 == null)
        {
            selectedCard2 = selectedCard;
            //StartCoroutine(CheckMatch());
            CheckMatch();
        }
    }

    private void ShuffleCards(List<int> _cardIDs)
    {
        for (int i = 0; i < _cardIDs.Count; i++)
        {
            int randomIndex = Random.Range(0, _cardIDs.Count);
            int temp = _cardIDs[i];
            _cardIDs[i] = _cardIDs[randomIndex];
            _cardIDs[randomIndex] = temp;
        }
    }

    private void ShuffleCardsFrontSprites(Sprite[] _cardsSprite)
    {
        for (int i = 0; i < _cardsSprite.Length; i++)
        {
            int randomIndex = Random.Range(0, _cardsSprite.Length);
            Sprite temp = _cardsSprite[i];
            _cardsSprite[i] = _cardsSprite[randomIndex];
            _cardsSprite[randomIndex] = temp;
        }
    }

    private void CheckMatch()
    {
        

        if (selectedCard1.CardID == selectedCard2.CardID)
        {
            Debug.Log("Card Matched" + selectedCard1.CardID);
            selectedCard1.SetMatched();
            selectedCard2.SetMatched();
        }
        else
        {
            selectedCard1.UnFlipCards();
            selectedCard2.UnFlipCards();
        }

        selectedCard1 = null;
        selectedCard2 = null;

        if (CheckGameOver())
        {
            GameManager.Instance.EndGame();
        }
    }

    /*
    private IEnumerator CheckMatch()
    {
        isCheckingMatch = true;

        yield return new WaitForSeconds(3.0f);

        if (selectedCard1.CardID == selectedCard2.CardID)
        {
            selectedCard1.SetMatched();
            selectedCard2.SetMatched();
        }
        else
        {
            selectedCard1.FlipCard();
            selectedCard2.FlipCard();
        }

        selectedCard1 = null;
        selectedCard2 = null;
        isCheckingMatch = false;

        if (CheckGameOver())
        {
            GameManager.Instance.EndGame();
        }
    }
    */

    private bool CheckGameOver()
    {
        foreach (Card card in cards)
        {
            if (!card.IsMatched)
            {
                return false;
            }
        }
        return true;
    }
}