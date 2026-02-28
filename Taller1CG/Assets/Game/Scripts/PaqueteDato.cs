using UnityEngine;
using System;

[System.Serializable]
public class PaqueteDato
{
    [SerializeField] private string id;
    [SerializeField] private int tamanoCarga;
    [SerializeField] private float tiempoLlegada;

    public string Id => id;
    public int TamanoCarga => tamanoCarga;
    public float TiempoLlegada => tiempoLlegada;

    public PaqueteDato(int tamano, float tiempo)
    {
        this.id = System.Guid.NewGuid().ToString();
        this.tamanoCarga = tamano;
        this.tiempoLlegada = tiempo;
    }
}