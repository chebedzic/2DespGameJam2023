using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenuScene : MonoBehaviour
{
    private bool sceneLoad = true;

    void Update()
    {

        if(sceneLoad)
        {
            IntroToMenu();
        }
        
    }


    private void IntroToMenu()
    {
        sceneLoad = false;
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

}