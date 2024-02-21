using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    /*FadeScene fade;
    void Awake()
    {s
        fade = GameObject.Find("Fade").GetComponent<FadeScene>();
    }*/
    public void Play()
    {
        SceneManager.LoadSceneAsync("Game");
    }
    public void Menu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
    public void Shop()
    {
        SceneManager.LoadSceneAsync("Shop");
    }
    public void Spin()
    {
        SceneManager.LoadSceneAsync("Spin");
    }
    
}
