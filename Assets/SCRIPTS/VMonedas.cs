using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VMonedas : MonoBehaviour
{
    [SerializeField] private AudioClip recoger;
    private float nivelSonido=1.0f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Goku")
        {
            VControladorSonido.Instance.EjecutarSonido(recoger,nivelSonido);
            other.gameObject.GetComponent<VAgarrarMoneda>().agarrar();
            Destroy(gameObject);
        }
    }
}
