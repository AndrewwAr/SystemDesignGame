using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform boardParent;
    public int rows = 2;
    public int columns = 4;

    private List<Card> cards = new List<Card>();
    private Card selectedCard1, selectedCard2;
    private bool isCheckingMatch;

    public void GenerateBoard()
    {
        List<int> cardIDs = new List<int>();
        for (int i = 0; i < (rows * columns) / 2; i++)
        {
            cardIDs.Add(i);
            cardIDs.Add(i);
        }

        ShuffleCards(cardIDs);

        InistantiateCards(cardIDs);


    }

    public void HandleCardSelection(Card selectedCard)
    {
        if (isCheckingMatch || selectedCard.IsFaceUp || selectedCard.IsMatched) return;

        selectedCard.FlipCard();

        if (selectedCard1 == null)
        {
            selectedCard1 = selectedCard;
        }
        else if (selectedCard2 == null)
        {
            selectedCard2 = selectedCard;
            StartCoroutine(CheckMatch());
        }
    }



    private void ShuffleCards(List<int> _cardIDs)
    {
        // Shuffle the card IDs
        for (int i = 0; i < _cardIDs.Count; i++)
        {
            int randomIndex = Random.Range(0, _cardIDs.Count);
            int temp = _cardIDs[i];
            _cardIDs[i] = _cardIDs[randomIndex];
            _cardIDs[randomIndex] = temp;
        }
    }

    private void InistantiateCards(List<int> _cardIDs)
    {
        for (int i = 0; i < _cardIDs.Count; i++)
        {
            GameObject cardObj = Instantiate(cardPrefab, boardParent);
            Card card = cardObj.GetComponent<Card>();
            card.Initialize(_cardIDs[i]);
            cards.Add(card);
        }
    }

    private IEnumerator CheckMatch()
    {
        isCheckingMatch = true;

        // Wait for a brief moment to let players see the second card
        yield return new WaitForSeconds(1.0f);

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