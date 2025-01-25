using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBubble : MonoBehaviour
{
    // para que rebote
    private int bounces = 0;

    public void setBounces(int b)
    {
        bounces = b;
    }
    public int getBounces() { return bounces; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
