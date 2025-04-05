using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorTest : MonoBehaviour
{
    public Sprite cursorArrow;
    public Sprite cursorArrowUpdate;

    [SerializeField] private GameObject CursorSprite;

    // Start is called before the first frame update
    void Start()
    {
        CursorSprite.GetComponent<Image>().sprite = cursorArrow;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        CursorSprite.transform.position = Input.mousePosition;

        // updates to down arrow
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CursorSprite.GetComponent<Image>().sprite = cursorArrowUpdate;
        }

        // updates to up arrow
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            CursorSprite.GetComponent<Image>().sprite = cursorArrow;
        }
    }

}