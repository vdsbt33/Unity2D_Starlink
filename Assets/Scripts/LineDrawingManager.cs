using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawingManager : MonoBehaviour {

    #region Singleton
    public static LineDrawingManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    // Line Renderer for Stars connections
    private LineRenderer lineRenderer;

    // Use this for initialization
    void Start () {
        // Line renderer config
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
    }
	
	// Update is called once per frame
	void Update () {
        // Will draw a line to the current position of the cursor

		// Will draw animated lines when a new connection is made
	}

    // Adds a connection on Line Renderer
    public void DrawLine(StarData selectedStar, StarData clickedStar)
    {
        // Draws the first vertex on the first star
        if (lineRenderer.positionCount == 0)
        {
            Debug.Log("First Vertex");
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, selectedStar.star.transform.position);
        }

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, clickedStar.star.transform.position);
    }

    // Clears all lines
    public void ClearLines()
    {
        Debug.Log("Should have cleaned lines");
        //lineRenderer.SetPositions(new Vector3[0]);
        lineRenderer.positionCount = 0;
        
    }

    // Sets the Loop setting of the Line Renderer
    public void SetLoop(bool value)
    {
        if (lineRenderer != null)
        {
            lineRenderer.loop = value;
        }
    }

}
