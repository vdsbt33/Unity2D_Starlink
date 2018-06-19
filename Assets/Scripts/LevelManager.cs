using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    #region Singleton
    public static LevelManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    // End Level overlay image configuration
    private Image levelEnd_OverlayImage;
    public float levelEnd_ImageSmoothTime;
    public float levelEnd_DelayUntilNextLevel;


    #region LEVELS
    // Current level index
    private int currentLevel;
    // List for all existing levels
    public List<LevelData> levelList;
    // Premade Levels
    #region LEVEL_HEXAGON
    private Vector2[] LEVEL_HEXAGON
    {
        get
        {
            Vector2[] positions =
            {
                new Vector2(-2.5f, 1.2f),
                new Vector2(-1.3f, 3.6f),
                new Vector2(1.5f, 3.6f),
                new Vector2(2.7f, 1.0f),
                new Vector2(1.6f, -1.4f),
                new Vector2(-1.3f, -1.3f)
            };
            return positions;
        }
    }
    #endregion
    #endregion

    // Use this for initialization
    void Start () {
        // Sets current level to index 0
        //levelList = new List<LevelData>();
        currentLevel = 0;

        // Generates the first level on the list
        GenerateLevel(levelList[0].positions);
    }

    // Generates level out of a Vector2 array
    private void GenerateLevel(Vector2[] stars)
    {

        foreach (Vector2 v in stars)
        {
            StarManager.instance.AddStar(v);
        }

        // Notifies MouseManager that level was generated
        MouseManager.instance.OnLevelBuild();

        // Resets the loop of the Line Renderer
        LineDrawingManager.instance.SetLoop(false);
    }

    // Ends game
    public void EndLevel()
    {
        // Show image slowly

        // Shows Stars drawing and wait for the set amount of time before generating the next level
        StartCoroutine(DelayEndLevel(levelEnd_DelayUntilNextLevel));

    }

    // Waits set amount of time (in seconds) before generating the next level
    IEnumerator DelayEndLevel(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        // Generates the next level on the list
        currentLevel++;
        if (levelList[currentLevel] != null)
        {
            // Clears scene from all Stars, Lines and Particles
            StarManager.instance.ClearStars();
            LineDrawingManager.instance.ClearLines();
            ParticleManager.instance.ClearParticles();

            // Generates the next set level
            GenerateLevel(levelList[currentLevel].positions);

        }
        else
        {
            // Show credits scene
        }
    }
}
