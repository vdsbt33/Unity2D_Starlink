using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarData {

    public GameObject star;
    private GameObject connectedFrom;
    private GameObject connectsTo;

    // Constructor
    public StarData(GameObject star)
    {
        this.star = star;
    }

    void Start()
    {
        star = null;
        connectedFrom = null;
        connectsTo = null;
    }

    // Sets the connection made from another star to this star
    public void SetConnectedFrom(GameObject connectedFrom)
    {
        this.connectedFrom = connectedFrom;
    }

    // Sets the connection that this star makes to another star
    public void SetConnectsTo(GameObject connectsTo)
    {
        this.connectsTo = connectsTo;
    }

    // Gets connectedFrom value
    public GameObject GetConnectedFrom()
    {
        return this.connectedFrom;
    }

    // Gets connectsTo value
    public GameObject GetConnectsTo()
    {
        return this.connectsTo;
    }

}
