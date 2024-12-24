using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    //public int CardID { get; private set; }
    public int CardID;
    public bool IsFaceUp { get; private set; }
    public bool IsMatched { get; private set; }

    [SerializeField]
    private Image frontFace;
    [SerializeField]
    private Image backFace;


    private Animator cardAnimator;
    private BoardManager boardManager;
    private AudioManager audioManager;


    private void Start()
    {
        cardAnimator = GetComponent<Animator>();
        boardManager = FindObjectOfType<BoardManager>();
        audioManager = FindObjectOfType<AudioManager>();
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

        audioManager.PlayAudioSoundEffect(GameAudioClips.matching);


        IsMatched = true;
        //UpdateCardAppearance();

        frontFace.enabled = false;
        backFace.enabled = false;
        GetComponent<Button>().enabled = false; ;

    }

    public void UnFlipCards()
    {
        audioManager.PlayAudioSoundEffect(GameAudioClips.misMatching);

        cardAnimator.SetTrigger("unFlip");
    }

    public void OnFlipCard_AnimationEventHandler()
    {
        FlipCard();
    }

    

    public void ClickCard_UIEventHandler()
    {
        audioManager.PlayAudioSoundEffect(GameAudioClips.cardFlip);

        cardAnimator.SetTrigger("flip");//+FlipCard_AnimationEventHandler() called in the middle of the animation

        StartCoroutine(nameof(HandleCardSelectionAfterFlip)); 
    }

    private IEnumerator HandleCardSelectionAfterFlip()
    {
        float animationLength = GetAnimationClipLength(cardAnimator, "cardAnimation");
        //Debug.Log("Checking coroutineStarted" + animationLength);
        yield return new WaitForSeconds(animationLength);
        boardManager.HandleCardSelection(this);
    }

    private float GetAnimationClipLength(Animator animator, string clipName)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
            {
                return clip.length; 
            }
        }

        return -1f; 
    }
}