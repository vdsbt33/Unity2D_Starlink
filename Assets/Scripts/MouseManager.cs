using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MouseManager : MonoBehaviour {

    #region Singleton
    public static MouseManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    // Cursor style. Will use OS if none is selected
    public Texture2D cursorImage;

    // Selected Star will, by default, be the first inserted
    private StarData selectedStar;
    private StarData clickedStar;

	// Use this for initialization
	void Start () {
		if (cursorImage)
        {
            Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.ForceSoftware);
        }
	}

    // Updated every frame
    void Update()
    {
        // Send mouse position to LineDrawingManager to draw a line from the selectedStar to the mouse position
    }
	
	// LateUpdate is called after Update
	void LateUpdate () {

        // Mouse left click
        if (Input.GetMouseButtonDown(0))
        {
            // Does not allow star selection if all stars are connected
            if (StarManager.instance.AllStarsConnected())
            {
                // Allow only clicking in UI elements, such as NEXT LEVEL
            }
            else
            {
                Debug.Log("Selected star: " + selectedStar.star.name);

                // Collision detection to check if a GameObject was clicked
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D[] col = Physics2D.OverlapCircleAll(mousePos, 0.02f);

                if (col.Length > 0)
                {
                    Collider2D collider = (Collider2D)col.GetValue(0); // Collider of the GameObject clicked
                    GameObject parent = collider.gameObject; // GameObject clicked
                    clickedStar = StarManager.instance.GetStarFromGameObject(parent);

                    // Star cannot be itself
                    if (clickedStar != selectedStar)
                    {
                        // Check if the clickedStar does not have a connection
                        if (!clickedStar.GetConnectedFrom())
                        {
                            selectedStar = StarManager.instance.ConnectStars(selectedStar, clickedStar);
                        }
                    }

                }
            }

        }
	}

    // Called when LevelManeger builds the level
    public void OnLevelBuild()
    {
        //selectedStar = StarManager.instance.GetStarAtIndex(0);

    }

    // Change the selectedStar Star
    public void SetSelectedStar(StarData selectedStar)
    {
        this.selectedStar = selectedStar;
    }

}
