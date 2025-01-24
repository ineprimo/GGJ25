using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDown : MonoBehaviour
{
    [SerializeField] private GachaponManager _gachaManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnMouseDown()
    {
        Debug.Log("A GACHAPONERAR");
        Upgrade up = _gachaManager.pull();

        if(up == null)
            Debug.Log("no upgrade avaliable");
        else
            Debug.Log(up.name);

    }
}
