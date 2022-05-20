using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName = "MENU";
    //[SerializeField] private GameObject loadingPanel;
    //[SerializeField] private Slider loadingSlider;

    public void LoadSceneByName(string newSceneName)
    {
        StartCoroutine(LoadAsynchronously(newSceneName));
        //LoadSceneAsynchronously(newSceneName);
    }

    void LoadSceneAsynchronously(string sceneName)
    {
        //Debug.Log("LoadNextScene: save scene name " + sceneName);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        //loadingPanel.SetActive(true);

        //Debug.Log("LoadNextScene: close all before load ");
        CloseAllBeforeLoading();
    }


    IEnumerator LoadAsynchronously(string sceneName)
    {
        //Debug.Log("LoadNextScene: save scene name " + sceneName);
    
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        //loadingPanel.SetActive(true); 

        while (operation.isDone == false)
        {
            //float progress = Mathf.Clamp01(operation.progress / 0.9f);
            //loadingSlider.value = progress;
            yield return null;
        }  
    }

    public virtual void CloseAllBeforeLoading()
    {

    }

    public void LoadMainManu()
    {
        LoadSceneByName(mainMenuSceneName);
    }

    public void ReloadCurrentLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        LoadSceneByName(currentScene.name);
    }
}
