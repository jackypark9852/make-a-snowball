using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasBehavior : MonoBehaviour
{
    [SerializeField] private GameObject facade;

    private void Start()
    {
        StartCoroutine(FadeInCoro(1f));
    }

    //time: the time it takes to fade out
    //scene: the path to the scene to load (add to build settings first!)
    public IEnumerator FadeOutAsyncLoad(float time, string scene)
    {
        yield return FadeOutCoro(time);
        yield return AsyncLoadScene(scene);
    }
    
    public static IEnumerator AsyncLoadScene(string path)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(path);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    
    public IEnumerator FadeOutCoro(float time)
    {
        for (float alpha = 0f; alpha <= 1f; alpha += 0.05f / time)
        {
            var color = facade.GetComponent<Image>().color;
            facade.GetComponent<Image>().color = new Color(color.r, color.g, color.b, alpha);
            yield return new WaitForSeconds(.05f);
        }
        var colorFinal = facade.GetComponent<Image>().color;
        facade.GetComponent<Image>().color = new Color(colorFinal.r, colorFinal.g, colorFinal.b, 1f);
        yield return null;
    }
    
    public IEnumerator FadeInCoro(float time)
    {
        for (float alpha = 1f; alpha >= 0f; alpha -= 0.05f / time)
        {
            var color = facade.GetComponent<Image>().color;
            facade.GetComponent<Image>().color = new Color(color.r, color.g, color.b, alpha);
            yield return new WaitForSeconds(.05f);
        }
        var colorFinal = facade.GetComponent<Image>().color;
        facade.GetComponent<Image>().color = new Color(colorFinal.r, colorFinal.g, colorFinal.b, 0f);
    }
}
