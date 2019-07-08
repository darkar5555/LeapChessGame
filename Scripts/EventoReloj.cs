using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventoReloj : MonoBehaviour
{
    private void OnEnable()
    {
        Reloj.AlLlegarACero += CambiarARojo;
    }

    private void OnDisable()
    {
        Reloj.AlLlegarACero -= CambiarARojo;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CambiarARojo()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
