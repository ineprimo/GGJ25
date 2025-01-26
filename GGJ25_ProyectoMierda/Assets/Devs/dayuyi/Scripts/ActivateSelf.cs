using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSelf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeActive(bool hola)
    {
        gameObject.SetActive(hola);
    }
}
