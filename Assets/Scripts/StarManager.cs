using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{

    #region Singleton
    public static StarManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion


    // - Public variables
    // Star prefab
    public GameObject starPrefab;
    // Parent for all stars
    public Transform starsParent;
    // Data from each of the instantiated stars
    private List<StarData> starData;

    // Use this for initialization
    void Start()
    {
        starData = new List<StarData>();
        
    }

    // Random position inside screen
    private Vector3 GetRandomPosition()
    {
        float width = Random.Range(3, Screen.width - 3);
        float height = Random.Range(3, Screen.height - 3);
        Vector3 position = new Vector3(width, height, 0);
        return position;
    }

    // Quaternion with X, Y and Z equal to 0
    private Quaternion GetZeroRotation()
    {
        return Quaternion.Euler(0f, 0f, 0f);
    }

    // Add StarData value
    public void AddStarData(GameObject starInstance)
    {
        /* 
        If starData, as array, is not initialized, Unity will throw the error:

        NullReferenceException: Object reference not set to an instance of an object
        StarManager.AddStarData (UnityEngine.GameObject starInstance) [...]

        So its safer to use List<>
         */
        starData.Add(new StarData(starInstance));
        
        // Sets the selectedStar in MouseManager to this added Star if it is the first one inserted
        if (starData.Count == 1)
        {
            MouseManager.instance.SetSelectedStar(starData[0]);
        }
    }

    // Returns the amount of Stars with a connection
    private int CountConnections()
    {
        int counter = 0;
        foreach (StarData sd in starData)
        {
            if (sd.GetConnectsTo())
            {
                counter++;
            }
        }
        return counter;
    }

    // Returns all Stars with a connectsTo value
    private List<StarData> GetAllConnected()
    {
        List<StarData> connectedStars = new List<StarData>();

        foreach (StarData sd in starData)
        {
            if (sd.GetConnectsTo())
            {
                connectedStars.Add(sd);
            }
        }

        return connectedStars;
    }

    // Returns all Stars' positions in a Vector3 array
    private Vector3[] GetConnectedStarsPositions()
    {
        List<StarData> connectedStars = GetAllConnected();
        List<Vector3> positions = new List<Vector3>();

        foreach (StarData sd in connectedStars)
        {
            positions.Add(sd.star.transform.position);
        }

        return positions.ToArray();
    }

    // Instantiate Star prefabs on Scene in random position
    public void GenerateRandomStars(int amount)
    {
        GameObject star = (GameObject)Instantiate(starPrefab, this.GetRandomPosition(), this.GetZeroRotation(), starsParent);
        this.AddStarData(star);
    }

    // Instantiate a Star prefab on Scene in Vector2 position
    public void AddStar(Vector2 position)
    {
        Vector3 finalPosition = new Vector3(position.x, position.y, 0f);
        GameObject star = (GameObject)Instantiate(starPrefab, finalPosition, this.GetZeroRotation(), starsParent);

        this.AddStarData(star);
    }

    // Gets the Star at the selected Index
    public StarData GetStarAtIndex(int index)
    {
        return starData[index];
    }

    /*
      Private function to find the StarData that corresponds to the star GameObject
    */
    private StarData GetStarFromGameObjectFunction(GameObject starObject)
    {
        foreach (StarData sd in starData)
        {
            if (sd.star == starObject)
            {
                return sd;
            }
        }
        return null;
    }

    // Gets the StarData from the selected GameObject
    public StarData GetStarFromGameObject(GameObject starObject)
    {
        StarData starData = GetStarFromGameObjectFunction(starObject);
        if (starObject != null && starData != null)
        {
            return starData;
        }
        return null;
    }

    // Clear instantiated stars from scene
    public void ClearStars()
    {
        foreach (StarData sd in starData)
        {
            Destroy(sd.star);
        }
        starData.Clear();

        MouseManager.instance.SetSelectedStar(null);
    }

    /*
     * Makes a connection between two stars
     * Returns the new selected star
     * */
    public StarData ConnectStars(StarData selectedStar, StarData clickedStar)
    {
        if (!clickedStar.GetConnectedFrom())
        {
            selectedStar.SetConnectsTo(clickedStar.star);
            clickedStar.SetConnectedFrom(selectedStar.star);

            Debug.Log("Connecting stars...");
            // Visual cues
            // Shine star
            selectedStar.star.transform.Find("Star_Bloom").gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            // Draw connection
            LineDrawingManager.instance.DrawLine(selectedStar, clickedStar);
            // Create particles over connected star
            ParticleManager.instance.CreateStarConnectedParticle(selectedStar.star.transform.position);

            if (CountConnections() == starData.Count)
            {
                LineDrawingManager.instance.SetLoop(true);
            }
            // Checks if, with this connection, all connections were made
            if (AllStarsConnected())
            {
                Debug.Log("Level Ended! All stars connected");
                LevelManager.instance.EndLevel();
            }

            return clickedStar;
        }
        return selectedStar;
    }
    

    // Sends LevelManager the signal to end level
    // Shows drawing of the connected stars
    public bool AllStarsConnected()
    {
        if (starData.Count > 0 && GetAllConnected().Count == starData.Count)
        {
            return true;
        }
        return false;
    }

}
