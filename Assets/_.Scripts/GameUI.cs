using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;

public class GameUI : MonoBehaviour
{
    [SerializeField] private AudioSource BGM;
    [SerializeField] private AudioMixer SFX;

    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TextMeshProUGUI starCountText;
    [SerializeField] private TextMeshProUGUI starCountGameOver;
    [SerializeField] private TextMeshProUGUI h_ScoreGameOver;

    [SerializeField] private GameObject gameOverPref;
    [SerializeField] private GameObject pointTable;
    [SerializeField] private GameObject pauseOption;
    [SerializeField] private GameObject pontPopUP_Pref;
    [SerializeField] private Transform popupParent;

    private int starCount = 0;
    private int hScore;

    public void bgmVolume()
    {
        BGM.volume = bgmSlider.value;
    }

    public void sfxVolume(float volume)
    {
        SFX.SetFloat("Volume", volume);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Home()
    {
        SceneManager.LoadScene("Home");
        Time.timeScale = 1;
    }

    public void Pause(bool toggle)
    {
        if (toggle)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void StarCollect(int count)
    {
        starCount += count;

        if (starCount > hScore)
        {
            hScore=starCount;
            PlayerPrefs.SetInt("HScore", hScore);
        }
       
        GameObject point = Instantiate(pontPopUP_Pref,popupParent);
        point.GetComponent<TextMeshProUGUI>().text = $"+{count.ToString("00")}";
        Destroy(point, 0.75f);

        starCountText.text = $"x {starCount.ToString("00")}"; 
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        gameOverPref.SetActive(true);
        pointTable.SetActive(false);
        pauseOption.SetActive(false);

    }

    private void Start()
    {
        hScore= PlayerPrefs.GetInt("HScore", 0);
    }

    private void Update()
    {
        starCountGameOver.text = starCount.ToString("00");
        h_ScoreGameOver.text = PlayerPrefs.GetInt("HScore").ToString("00");
    }

    private void OnEnable()
    {
        EventManager.onStarCollect += StarCollect;
        EventManager.onGameOver += GameOver;
    }

    private void OnDisable()
    {
        EventManager.onStarCollect -= StarCollect;
        EventManager.onGameOver -= GameOver;
    }
}
