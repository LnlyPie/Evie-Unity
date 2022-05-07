using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Texture2D normalCursor;
    public Texture2D clickedCursor;
    public static Texture2D normalCursorS;
    public static Texture2D clickedCursorS;

    private void Awake() {
        normalCursorS = normalCursor;
        clickedCursorS = clickedCursor;
    }

    public static void CursorClicked() {
        Cursor.SetCursor(clickedCursorS, new Vector2(0, 0), CursorMode.Auto);
    }
    public static void CursorNormal() {
        Cursor.SetCursor(normalCursorS, new Vector2(0, 0), CursorMode.Auto);
    }
}
