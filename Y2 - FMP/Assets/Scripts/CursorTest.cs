using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorTest : MonoBehaviour
{
    public Sprite cursorArrow;
    public Sprite cursorArrowUpdate;
    public GameObject ClampLeft;
    public GameObject ClampRight;
    public GameObject ClampTop;
    public GameObject ClampBottom;

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
       //  CursorSprite.transform.position = Input.mousePosition;
        Vector3 cursorPosition = new Vector2(Mathf.Clamp(Input.mousePosition.x , ClampLeft.transform.position.x , ClampRight.transform.position.x), Mathf.Clamp(Input.mousePosition.y , ClampBottom.transform.position.y , ClampTop.transform.position.y));
        CursorSprite.transform.position = cursorPosition;
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