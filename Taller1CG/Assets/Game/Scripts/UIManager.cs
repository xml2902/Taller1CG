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
        textoPaquetesEnEspera.text = "En espera: " + servidorManager.CantidadEnCola;
        textoTotalProcesados.text = "Procesados: " + servidorManager.CantidadProcesados;
        textoTiempoPromedio.text = "Promedio: " + servidorManager.GetTiempoPromedio.ToString("F2") + "s";
    }

    public void VerificarSaturacion()
    {
        bool saturado = servidorManager.CantidadEnCola > 20;
        textoEstadoServidor.text = saturado ? "SERVIDOR SATURADO" : "ESTADO: NORMAL";
        if (panelEstado != null) panelEstado.color = saturado ? Color.red : Color.green;
    }

    public void MostrarUltimoProcesado(string id, int tamano, float espera)
    {
        textoUltimoID.text = "ID: " + id;
        textoUltimoTamano.text = "Tamaño: " + tamano;
        textoUltimoTiempoEspera.text = "Espera: " + espera.ToString("F2") + "s";
    }

    public void Buscar()
    {
        PaqueteDato p = servidorManager.BuscarPaquete(inputBuscar.text);
        textoResultado.text = (p != null) ? "Carga: " + p.TamanoCarga : "No encontrado";
    }
}