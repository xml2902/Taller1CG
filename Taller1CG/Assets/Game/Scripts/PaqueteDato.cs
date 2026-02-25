using System;
using UnityEngine;

[System.Serializable]

public class PaqueteDato
{
    private string id;
    private int tamanoCarga;
    private float tiempoLlegada;

    public string Id => id;
    public int TamanoCarga => tamanoCarga;
    public float TiempoLlegada => tiempoLlegada;

    public PaqueteDato(int tamano)
    {
        this.id = System.Guid.NewGuid().ToString(); //UUID para identificar cada paquete de forma única
        this.tamanoCarga = tamano;
        this.tiempoLlegada = Time.time;
                                        
    }
}
