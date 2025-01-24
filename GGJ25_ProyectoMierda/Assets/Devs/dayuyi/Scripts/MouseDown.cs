using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDown : MonoBehaviour
{
    [SerializeField] private GachaponBase _gachaManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnMouseDown()
    {
        // esto va cuando se quiera hacer un pull en el gachapon
        Upgrade up = _gachaManager.pull();

        if(up == null)
            Debug.Log("no upgrade avaliable");
        else
            Debug.Log(up.getName());


        // llamar al gamemanager update ()

    }
}
