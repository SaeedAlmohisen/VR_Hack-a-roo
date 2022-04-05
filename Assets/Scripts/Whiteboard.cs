// Inspiration from: https://www.youtube.com/watch?v=sHE5ubsP-E8&list=PLwhlAQL-Q2jlrzJF1FSFX5Z81kGB8Ja2Q&index=3
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{
    public Texture2D texture;
    public Vector2 textureSize = new Vector2(2048, 2048);
    void Start()
    {
        var r = GetComponent<Renderer>();

        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        r.material.mainTexture = texture;
    }

    
}
