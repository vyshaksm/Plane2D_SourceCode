using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HomeUI_Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreTXT;

    private void Start()
    {
        highScoreTXT.text = PlayerPrefs.GetInt("HScore").ToString("00");
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
