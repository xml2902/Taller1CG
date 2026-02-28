using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            yield return new WaitForSeconds(Random.Range(intervaloMinimo, intervaloMaximo));
            int cantidad = Random.Range(1, 6);
            for (int i = 0; i < cantidad; i++)
            {
                colaProcesamiento.Enqueue(new PaqueteDato(Random.Range(1, 101), Time.time));
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

            float espera = Time.time - paquete.TiempoLlegada;
            tiempoTotalEspera += espera;
            totalProcesados++;
            tiempoPromedio = tiempoTotalEspera / totalProcesados;

            uiManager.MostrarUltimoProcesado(paquete.Id, paquete.TamanoCarga, espera);
            uiManager.ActualizarUI();

            GuardarDatosJSON();
        }
    }

    public PaqueteDato BuscarPaquete(string id)
    {
        if (historialProcesados.ContainsKey(id)) return historialProcesados[id];
        return null;
    }

    public void GuardarDatosJSON()
    {
        HistorialWrapper wrapper = new HistorialWrapper();
        foreach (var p in historialProcesados.Values) wrapper.paquetes.Add(p);

        string json = JsonUtility.ToJson(wrapper, true);
        string ruta = Path.Combine(Application.streamingAssetsPath, "guardar_datos.json");

        if (!Directory.Exists(Application.streamingAssetsPath)) Directory.CreateDirectory(Application.streamingAssetsPath);

        File.WriteAllText(ruta, json);
        Debug.Log("Guardado en StreamingAssets");
    }

    public void Clear()
    {
        colaProcesamiento.Clear();
        historialProcesados.Clear();
        tiempoTotalEspera = 0f;
        totalProcesados = 0;
        tiempoPromedio = 0f;
        uiManager.ActualizarUI();
        uiManager.VerificarSaturacion();
    }
}

[System.Serializable]
public class HistorialWrapper { public List<PaqueteDato> paquetes = new List<PaqueteDato>(); }