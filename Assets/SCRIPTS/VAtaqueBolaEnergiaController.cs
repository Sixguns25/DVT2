using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class VAtaqueBolaEnergiaController : MonoBehaviour
{
    public GameObject bolaEnergiaPrefab;

    SpriteRenderer sr;

    //SONIDOS
    //LanzarEnergía
    public AudioClip lanzarEnergia;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            audioSource.PlayOneShot(lanzarEnergia);
            GameObject energia = Instantiate(bolaEnergiaPrefab, transform.position, Quaternion.identity);
            energia.GetComponent<VEnergiaController>().SetDirection(sr.flipX ? "left" : "right");
            


        }
    }


}
