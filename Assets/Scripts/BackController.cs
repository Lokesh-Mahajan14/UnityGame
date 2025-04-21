using UnityEngine;
using UnityEngine.SceneManagement;
public class BackController : MonoBehaviour
{
    public void BackGame()
    {
        SceneManager.LoadScene("MenuSection");
    }
}
