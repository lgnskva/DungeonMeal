using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeScene : MonoBehaviour
{
    float fadeSpeed = 10;
    Image image;
    bool sceneStart;
    bool sceneEnd;
    string scene;

    void Awake()
    {
        image = GetComponent<Image>();
        image.enabled = true;
        sceneStart = true;
    }

    void Update()
    {
        if (sceneStart) StartScene();
        if (sceneEnd) EndScene(scene);
    }

    void StartScene()
    {
        image.color = Color.Lerp(image.color, Color.clear, fadeSpeed * Time.deltaTime);

        if (image.color.a <= 0.01f)
        {
            image.color = Color.clear;
            image.enabled = false;
            sceneStart = false;
        }
    }

    public void EndScene(string scene_)
    {
        scene = scene_;
        sceneEnd= true;
        image.enabled = true;
        image.color = Color.Lerp(image.color, Color.black, fadeSpeed * Time.deltaTime);

        if (image.color.a >= 0.9f)
        {
            image.color = Color.black;
            SceneManager.LoadScene(scene);
        }
    }
}