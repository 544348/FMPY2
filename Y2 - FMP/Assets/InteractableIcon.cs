using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableIcon : MonoBehaviour
{
    public string functionName;
    private bool mouseIsHoveringOverIcon;
    public GameObject window;
    public GameObject objectsInRecyclingBin;
    public string sceneName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cursor"))
        {
            mouseIsHoveringOverIcon = true;
            Debug.Log("mouse is hovering over " + gameObject.name);
        }
    }
    private void OpenWindow() //desktop window
    {
        window.SetActive(true);
    }
    private void CloseWindow()
    {
        window.SetActive(false);
    }
    private void ClearRecyclingBin()
    {
        objectsInRecyclingBin.SetActive(false);
    }
    private void OpenComputerGame()
    {
       // yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
    private void OnTriggerExit(Collider other)
    {
        mouseIsHoveringOverIcon = false;
        Debug.Log("mouse is hovering over icon");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (mouseIsHoveringOverIcon) 
            {
                Invoke(functionName, 0);
            }
        }
    }
}
