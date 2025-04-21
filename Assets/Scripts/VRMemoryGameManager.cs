using System.Collections;
using UnityEngine;

public class VRMemoryGameManager : MonoBehaviour
{
    private VRMemoryCard firstCard;
    private VRMemoryCard secondCard;
    private bool isChecking = false;
    private int matchCount = 0;
    public int totalPairs = 4; // Adjust as needed

    public void CardFlipped(VRMemoryCard card)
    {
        if (isChecking || card.isMatched) return;

        if (firstCard == null)
        {
            firstCard = card;
        }
        else if (secondCard == null && card != firstCard)
        {
            secondCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        isChecking = true;
        yield return new WaitForSeconds(1f);

        if (firstCard.frontMaterial == secondCard.frontMaterial)
        {
            // Match found
            firstCard.isMatched = true;
            secondCard.isMatched = true;
            matchCount++;

            if (matchCount >= totalPairs)
            {
                Debug.Log("Game Over - All matches found!");
            }
        }
        else
        {
            // No match - flip back
            firstCard.FlipCard();
            secondCard.FlipCard();
        }

        firstCard = null;
        secondCard = null;
        isChecking = false;
    }

    void Start()
    {
        // Randomize card positions
        VRMemoryCard[] cards = FindObjectsOfType<VRMemoryCard>();
        for (int i = 0; i < cards.Length; i++)
        {
            Vector3 tempPos = cards[i].transform.position;
            int randomIndex = Random.Range(0, cards.Length);
            cards[i].transform.position = cards[randomIndex].transform.position;
            cards[randomIndex].transform.position = tempPos;
        }
    }
}