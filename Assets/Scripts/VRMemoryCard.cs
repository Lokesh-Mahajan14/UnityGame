using UnityEngine;

public class VRMemoryCard : MonoBehaviour
{
    public Material frontMaterial;
    public Material backMaterial;
    public bool isFlipped = false;
    public bool isMatched = false;

    void Start()
    {
        GetComponent<Renderer>().material = backMaterial;
    }

    public void FlipCard()
    {
        if (isMatched) return;

        isFlipped = !isFlipped;
        GetComponent<Renderer>().material = isFlipped ? frontMaterial : backMaterial;

        // Notify game manager
        FindObjectOfType<VRMemoryGameManager>().CardFlipped(this);
    }

    // Called when controller/hand interacts
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller") || other.CompareTag("Hand"))
        {
            FlipCard();
        }
    }
}