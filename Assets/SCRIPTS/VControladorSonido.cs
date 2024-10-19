using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VControladorSonido : MonoBehaviour
{
    public static VControladorSonido Instance;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }
    // Método para ejecutar el sonido con volumen ajustable
    public void EjecutarSonido(AudioClip sonido, float volumen)
    {
        audioSource.volume = volumen;  // Ajusta el volumen
        audioSource.PlayOneShot(sonido);
    }
}
