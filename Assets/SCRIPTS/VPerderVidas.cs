using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VPerderVidas : MonoBehaviour
{
    public Text Vidas;
    public int CantVidas=5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CantVidas >0)
        {
            Vidas.text = "Vidas:  " + "".PadRight(CantVidas, '♥');
        }
        else
        {
            Vidas.text = "Haz perdido";
        }
        
    }

    public void perderVida()
    {
        CantVidas = CantVidas -1;
    }
}
