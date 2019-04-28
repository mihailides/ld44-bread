using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    private Texture2D cursor;
    // Start is called before the first frame update
    void Start()
    {
        cursor = (Texture2D)Resources.Load("cursor");
    }
    
    public void OnMouseOver()
    {
        Cursor.SetCursor (cursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
