using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuVR : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ShowCredit()
    {
        // �ʴ� Canvas ���� Pop-up Credit
        Debug.Log("Show Credit");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
