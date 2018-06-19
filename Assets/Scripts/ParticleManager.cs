using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

    #region Singleton
    public static ParticleManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion


    // Particles parent. Stores all particles in the world
    public Transform particlesParent;

    // Particle effects to draw when the Star is connected
    public GameObject starConnectedBurstEffect;
    public GameObject starConnectedEffect;

    // List of particles in the world
    private List<GameObject> particleList;

    // Executed when the game runs
    void Start()
    {
        particleList = new List<GameObject>();
    }

    // Quaternion with X, Y and Z equal to 0
    private Quaternion GetZeroRotation()
    {
        return Quaternion.Euler(0f, 0f, 0f);
    }

    // Creates a particle in the world
    private void CreateParticle(Vector2 position, GameObject particle)
    {
        Vector3 finalPosition = new Vector3(position.x, position.y, 0f);
        particleList.Add((GameObject) Instantiate(particle, finalPosition, GetZeroRotation(), particlesParent));
    }

    // Creates a particle in the but destroys it after set time
    private void CreateParticle(Vector2 position, GameObject particle, float destroyTime)
    {
        Vector3 finalPosition = new Vector3(position.x, position.y, 0f);
        GameObject newParticle = (GameObject)Instantiate(particle, finalPosition, GetZeroRotation(), particlesParent);
        particleList.Add(newParticle);
        Destroy(newParticle, destroyTime);
    }

    // Creates a particle of type starConnectedEffect in a world position
    public void CreateStarConnectedParticle(Vector2 position)
    {
        CreateParticle(position, starConnectedBurstEffect, 2f);
        CreateParticle(position, starConnectedEffect);
    }

    // Clear particles in the world
    public void ClearParticles()
    {
        foreach (GameObject go in particleList)
        {
            Destroy(go);
        }
        particleList.Clear();
    }

}
