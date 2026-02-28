using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public ServidorManager servidorManager;

    [Header("Referencias UI")]
    public TMP_Text textoPaquetesEnEspera;
    public TMP_Text textoTotalProcesados;
    public TMP_Text textoEstadoServidor;
    public Image panelEstado;
    public TMP_Text textoTiempoPromedio;

    [Header("Buscador")]
    public TMP_InputField inputBuscar;
    public TMP_Text textoResultado;

    [Header("Detalle Último")]
    public TMP_Text textoUltimoID;
    public TMP_Text textoUltimoTamano;
    public TMP_Text textoUltimoTiempoEspera;

    public void ActualizarUI()
    {
        textoPaquetesEnEspera.text = "Paquetes en espera: " + servidorManager.CantidadEnCola;
        textoTotalProcesados.text = "Total Procesados: " + servidorManager.CantidadProcesados;
        textoTiempoPromedio.text = "Promedio: " + servidorManager.GetTiempoPromedio.ToString("F2") + "s";
    }

    public void VerificarSaturacion()
    {
        if (servidorManager.CantidadEnCola > 20)
        {
            textoEstadoServidor.text = "SERVIDOR SATURADO";
            if (panelEstado != null) panelEstado.color = Color.red;
        }
        else
        {
            textoEstadoServidor.text = "ESTADO: NORMAL";
            if (panelEstado != null) panelEstado.color = Color.green;
        }
    }

    public void MostrarUltimoProcesado(string id, int tamano, float espera)
    {
        textoUltimoID.text = "ID: " + id;
        textoUltimoTamano.text = "Tamaño: " + tamano;
        textoUltimoTiempoEspera.text = "Espera: " + espera.ToString("F2") + "s";
    }

    public void MostrarMensajeLimpieza(string mensaje)
    {
        textoUltimoID.text = mensaje;
        textoUltimoTamano.text = "";
        textoUltimoTiempoEspera.text = "";
    }

    public void Buscar()
    {
        string idBuscado = inputBuscar.text;
        PaqueteDato encontrado = servidorManager.BuscarPaquete(idBuscado);

        if (encontrado != null)
            textoResultado.text = "Encontrado - Carga: " + encontrado.TamanoCarga;
        else
            textoResultado.text = "No encontrado en historial";
    }
}