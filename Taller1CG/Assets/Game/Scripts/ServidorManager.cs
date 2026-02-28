using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServidorManager : MonoBehaviour
{
    private Queue<PaqueteDato> colaProcesamiento = new Queue<PaqueteDato>();
    private Dictionary<string, PaqueteDato> historialProcesados = new Dictionary<string, PaqueteDato>();

    private float tiempoTotalEspera = 0f;
    private int totalProcesados = 0;
    private float tiempoPromedio = 0f;

    public UIManager uiManager;

    [Header("Configuración")]
    public float intervaloMinimo = 2f;
    public float intervaloMaximo = 4f;

    public int CantidadEnCola => colaProcesamiento.Count;
    public int CantidadProcesados => historialProcesados.Count;
    public float GetTiempoPromedio => tiempoPromedio;

    void Start()
    {
        StartCoroutine(GeneradorPaquetes());
        uiManager.ActualizarUI();
    }

    IEnumerator GeneradorPaquetes()
    {
        while (true)
        {
            float espera = Random.Range(intervaloMinimo, intervaloMaximo);
            yield return new WaitForSeconds(espera);

            int cantidadPaquetes = Random.Range(1, 6);
            for (int i = 0; i < cantidadPaquetes; i++)
            {
                int tamano = Random.Range(1, 101);
                PaqueteDato nuevoPaquete = new PaqueteDato(tamano, Time.time);
                colaProcesamiento.Enqueue(nuevoPaquete);
            }
            uiManager.ActualizarUI();
            uiManager.VerificarSaturacion();
        }
    }

    public void ProcesarSiguiente()
    {
        if (colaProcesamiento.Count == 0) return;

        PaqueteDato paquete = colaProcesamiento.Dequeue();

        if (!historialProcesados.ContainsKey(paquete.Id))
        {
            historialProcesados.Add(paquete.Id, paquete);

            float tiempoEspera = Time.time - paquete.TiempoLlegada;
            tiempoTotalEspera += tiempoEspera;
            totalProcesados++;
            tiempoPromedio = tiempoTotalEspera / totalProcesados;

            uiManager.MostrarUltimoProcesado(paquete.Id, paquete.TamanoCarga, tiempoEspera);
            uiManager.ActualizarUI();
        }
    }

    public PaqueteDato BuscarPaquete(string id)
    {
        if (historialProcesados.ContainsKey(id))
            return historialProcesados[id];
        return null;
    }

    public void Clear()
    {
        colaProcesamiento.Clear();
        historialProcesados.Clear();
        tiempoTotalEspera = 0f;
        totalProcesados = 0;
        tiempoPromedio = 0f;
        uiManager.MostrarMensajeLimpieza("Sistema Reiniciado");
        uiManager.ActualizarUI();
        uiManager.VerificarSaturacion();
    }
}