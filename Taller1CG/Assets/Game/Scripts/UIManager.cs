using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public ServidorManager servidorManager;

    [Header("Referencias UI (asignar desde el Inspector)")]
    public TMP_Text textoPaquetesEnEspera;    // "Paquetes en espera"
    public TMP_Text textoTotalProcesados;      // "Total Procesados"
    public TMP_Text textoEstadoServidor;       // "ESTADO: NORMAL/SATURADO"
    public GameObject panelEstado;              // Panel que cambia de color
    public TMP_Text textoTiempoPromedio;

    [Header("Último Procesado")]
    public TMP_Text textoUltimoID;
    public TMP_Text textoUltimoTamaño;
    public TMP_Text textoUltimoTiempoEspera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActualizarUI()
    {
        if (textoPaquetesEnEspera != null)
            textoPaquetesEnEspera.text =
                "Paquetes en espera: " + servidorManager.CantidadEnCola;

        if (textoTotalProcesados != null)
            textoTotalProcesados.text =
                "Total Procesados: " + servidorManager.TotalProcesados;

        if (textoTiempoPromedio != null)
            textoTiempoPromedio.text =
                "Tiempo Promedio: " + servidorManager.TiempoPromedio.ToString("F2") + " s";
    }

    public void VerificarSaturacion()
    {
        if (servidorManager.colaProcesamiento.Count > 20)
        {
            // Estado SATURADO
            if (textoEstadoServidor != null)
                textoEstadoServidor.text = "ESTADO: SATURADO";
            textoEstadoServidor.color = Color.red;

            if (panelEstado != null)
                panelEstado.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        }
        else
        {
            // Estado NORMAL
            if (textoEstadoServidor != null)
                textoEstadoServidor.text = "ESTADO: NORMAL";

            //if (panelEstado != null)
               // panelEstado.GetComponent<UnityEngine.UI.Image>().color = Color.green;
        }
    }

    public void MostrarUltimoProcesado(string id, int tamaño, float tiempoEspera)
    {
        if (textoUltimoID != null)
            textoUltimoID.text = "ID: " + id;

        if (textoUltimoTamaño != null)
            textoUltimoTamaño.text = "Tamaño: " + tamaño;

        if (textoUltimoTiempoEspera != null)
            textoUltimoTiempoEspera.text = "Tiempo Espera: " + tiempoEspera.ToString("F2") + " s";
    }
}
