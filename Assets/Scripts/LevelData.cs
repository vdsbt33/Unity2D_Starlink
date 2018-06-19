using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Scriptable Objects/New Level")]
public class LevelData : ScriptableObject {

    public Vector2[] positions;

    public Image endLevelImage;

    public LevelData(Vector2[] positions, Image endLevelImage)
    {
        this.positions = positions;
        this.endLevelImage = endLevelImage;
    }


}
