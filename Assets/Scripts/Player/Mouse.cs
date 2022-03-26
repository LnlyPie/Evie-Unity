using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Texture2D normalCursor;
    public Texture2D clickedCursor;
    
    void Update()
    {
        if (Input.GetMouseButton(0)) {
            Cursor.SetCursor(clickedCursor, new Vector2(0,0), CursorMode.Auto);
        } else {
            Cursor.SetCursor(normalCursor, new Vector2(0,0), CursorMode.Auto);
        }
    }
}
