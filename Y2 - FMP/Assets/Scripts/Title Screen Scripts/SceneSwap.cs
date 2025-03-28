using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwap : MonoBehaviour
{
    // Name of the scene to load (set this in the Inspector)
    [Tooltip("Name of the scene to load. Make sure it’s added to the build settings.")]
    public string sceneName;

    // Delay before switching scenes
    [Tooltip("Time to wait before changing the scene.")]
    public float sceneChangeDelay = 0f;

    // Whether to load the scene automatically when the script starts
    [Tooltip("Check this to load the scene automatically after delay.")]
    public bool loadOnStart = false;

    void Start()
    {
        // Optionally load the scene automatically
        if (loadOnStart && !string.IsNullOrEmpty(sceneName))
        {
            StartCoroutine(ChangeSceneWithDelay());
        }
        else if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is not assigned or is empty. Please set the scene name.");
        }
    }

    // Public method to change the scene with delay
    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            StartCoroutine(ChangeSceneWithDelay());
        }
        else
        {
            Debug.LogError("Scene name is not assigned. Please set the scene name in the Inspector.");
        }
    }

    // Coroutine to change scene after delay
    private IEnumerator ChangeSceneWithDelay()
    {
        yield return new WaitForSeconds(sceneChangeDelay);
        SceneManager.LoadScene(sceneName);
    }
}
