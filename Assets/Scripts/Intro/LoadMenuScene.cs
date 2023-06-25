using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenuScene : MonoBehaviour
{
    [SerializeField]
    private bool sceneLoad = true;

    void Start()
    {

        if(sceneLoad)
        {
            IntroToMenu();
        }
        
    }


    public void IntroToMenu()
    {
        sceneLoad = false;
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
    
    public void LoadGame()
    {
        sceneLoad = false;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

}