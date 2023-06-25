using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private GameObject enterName;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Button btnOkName;
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnLeaderboards;
    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Application.targetFrameRate = 300;
        if (PlayerPrefs.HasKey("Name"))
        {
            ActivateMainMenu();
        }
        else
        {
            ActivateEnterName();
        }

        btnOkName.enabled = true;
        btnPlay.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Game");
        });
        btnOkName.onClick.AddListener(() =>
        {
            PlayerPrefs.SetString("Name", input.text);
            ActivateMainMenu();
        });
    }
    
    private void ActivateMainMenu()
    {
        mainMenu.SetActive(true);
        enterName.SetActive(false);
    }

    private void ActivateEnterName()
    {
        mainMenu.SetActive(false);
        enterName.SetActive(true);
    }
}
