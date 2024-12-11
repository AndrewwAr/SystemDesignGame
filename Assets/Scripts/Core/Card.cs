using UnityEngine;

public class Card : MonoBehaviour
{
    public int CardID { get; private set; }
    public bool IsFaceUp { get; private set; }
    public bool IsMatched { get; private set; }

    [SerializeField] private GameObject frontFace;
    [SerializeField] private GameObject backFace;

    public void Initialize(int cardID)
    {
        CardID = cardID;
        IsFaceUp = false;
        IsMatched = false;
        UpdateCardAppearance();
    }

    public void FlipCard()
    {
        if (IsMatched) return;

        IsFaceUp = !IsFaceUp;
        UpdateCardAppearance();
    }

    public void SetMatched()
    {
        IsMatched = true;
        UpdateCardAppearance();
    }

    private void UpdateCardAppearance()
    {
        frontFace.SetActive(IsFaceUp);
        backFace.SetActive(!IsFaceUp);
    }
}