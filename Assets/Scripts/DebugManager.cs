using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour {

    public bool debugSpawnStarsOnClick;

	// Use this for initialization
	void Start () {
		
	}
	
	// LateUpdate
	void LateUpdate () {
        // Spawn stars on mouse click
        if (debugSpawnStarsOnClick)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D[] col = Physics2D.OverlapCircleAll(mousePos, 0.08f);
                //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (col.Length <= 0)
                {
                    StarManager.instance.AddStar(mousePos);
                    Debug.Log("Star added at: " + mousePos.ToString());
                }
            }
            else if (Input.GetKeyDown(KeyCode.Delete))
            {
                StarManager.instance.ClearStars();
                Debug.Log("- Stars Cleared -");
            }
        }
	}
}
