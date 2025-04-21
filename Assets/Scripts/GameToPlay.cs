using UnityEngine;
using UnityEngine.SceneManagement;
public class GameToPlay : MonoBehaviour
{
    public void Game1Btn()
    {
        SceneManager.LoadScene("Tossing_Ball_Game");
    }
    public void Game2Btn()
    {
        SceneManager.LoadScene("Bow&Arrow_Game");
    }
}
