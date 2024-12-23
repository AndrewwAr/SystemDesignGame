using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int CardID { get; private set; }
    public bool IsFaceUp { get; private set; }
    public bool IsMatched { get; private set; }

    [SerializeField]
    private Image frontFace;
    [SerializeField]
    private Image backFace;


    private Animator cardAnimator;
    BoardManager boardManager;

    private void Start()
    {
        cardAnimator = GetComponent<Animator>();
        boardManager = FindObjectOfType<BoardManager>();
    }

    private void UpdateCardAppearance()
    {
        frontFace.enabled = IsFaceUp;
        backFace.enabled = !IsFaceUp;
    }


    public void Initialize(int _cardID, Sprite _cardFrontSprite, Sprite _cardBackSprite)
    {
        CardID = _cardID;
        IsFaceUp = false;
        IsMatched = false;
        frontFace.sprite = _cardFrontSprite;
        backFace.sprite = _cardBackSprite;
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

    public void FlipCard_AnimationEventHandler()
    {
        FlipCard();
    }

    public void ClickCard_UIEventHandler()
    {
        cardAnimator.SetTrigger("flip");//+FlipCard() is called in the middle of the animation as event
        //boardManager.HandleCardSelection(this);
    }
}