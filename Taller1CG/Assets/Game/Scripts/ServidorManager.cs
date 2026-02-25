using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ServidorManager : MonoBehaviour
{
   [Header("Cola y Diccionario")]
   private Queue<PaqueteDato> colaProcesamiento = new Queue<PaqueteDato>();
   private Dictionary<string, PaqueteDato> historialProcesados = new Dictionary<string, PaqueteDato>();

    private float tiempoTotalEspera = 0f;
    private int totalProcesados = 0;
    private float tiempoPromedio = 0f;


    public UIManager uiManager;

    [Header("Configuración")]
    public float intervaloMinimo = 2f;
    public float intervaloMaximo = 4f;
   


    void Start()
    {
        // Iniciar la corrutina que genera paquetes
        StartCoroutine(GeneradorPaquetes());

        // Actualizar UI al inicio
        uiManager.ActualizarUI();
    }

    IEnumerator GeneradorPaquetes()
    {
        while (true) // Bucle infinito (se ejecuta mientras el juego corre)
        {
            // Esperar entre 2 y 4 segundos
            float espera = Random.Range(intervaloMinimo, intervaloMaximo);
            yield return new WaitForSeconds(espera);

            // Generar entre 1 y 5 paquetes
            int cantidadPaquetes = Random.Range(1, 6);

            for (int i = 0; i < cantidadPaquetes; i++)
            {
                // Crear un nuevo paquete
                int tamaño = Random.Range(1, 101); // Tamaño entre 1 y 100
                float tiempoLlegada = Time.time;

                PaqueteDato nuevoPaquete = new PaqueteDato(tamaño, tiempoLlegada);

                // Encolar el paquete
                colaProcesamiento.Enqueue(nuevoPaquete);

                Debug.Log($"Paquete generado: ID={nuevoPaquete.Id}, Tamaño={tamaño}");
            }

            // Actualizar UI después de generar
            uiManager.ActualizarUI();

            // Verificar si el servidor está saturado
            uiManager.VerificarSaturacion();
        }
    }

    public int CantidadEnCola => colaProcesamiento.Count;

    public int TotalProcesados => historialProcesados.Count;

    public float TiempoPromedio => tiempoPromedio;

    // Método público para procesar siguiente (lo usará el botón en MÓDULO 3)
    public void ProcesarSiguiente()
    {
        if (colaProcesamiento.Count == 0)
            return;

        PaqueteDato paquete = colaProcesamiento.Dequeue();

        if (!historialProcesados.ContainsKey(paquete.id))
        {
            historialProcesados.Add(paquete.id, paquete);
        }

        float tiempoEspera = Time.time - paquete.tiempoLlegada;

        tiempoTotalEspera += tiempoEspera;
        totalProcesados++;

        float tiempoEspera = Time.time - paquete.tiempoLlegada;

        // usarla todas las veces que quieras
        tiempoTotalEspera += tiempoEspera;

        uiManager.MostrarUltimoProcesado(paquete.id, paquete.tamaño, tiempoEspera);
        //uiManager.ActualizarPromedio(promedio);
        uiManager.ActualizarUI();
       Debug.Log("Procesar siguiente paquete");
    }        
}
