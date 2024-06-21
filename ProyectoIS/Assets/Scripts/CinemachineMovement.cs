using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CineMachineMovCmera : MonoBehaviour
{
    public static CineMachineMovCmera Instance;
    private CinemachineVirtualCamera m_Camera;

    private CinemachineBasicMultiChannelPerlin m_MultiChannelPerlin;

    private float tiempo_mov;

    private float tiempoMovimientoTotal;

    private float intensidadInicial;

    // Update is called once per frame
    void Awake()
    {
        Instance = this;
        m_Camera = GetComponent<CinemachineVirtualCamera>();

        m_MultiChannelPerlin = m_Camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    }


    public void MoverCamara(float intensidad, float frecuencia, float tiempo)
    {
        m_MultiChannelPerlin.m_AmplitudeGain = intensidad;
        m_MultiChannelPerlin.m_FrequencyGain = frecuencia;
        intensidadInicial = intensidad;
        tiempoMovimientoTotal = tiempo;
        tiempo_mov = tiempo;
    }

    private void Update()
    {
        if (tiempo_mov > 0)
        {
            tiempo_mov -= Time.deltaTime;
            m_MultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(intensidadInicial, 0, 1 - (tiempo_mov / tiempoMovimientoTotal));
        }
    }
}
