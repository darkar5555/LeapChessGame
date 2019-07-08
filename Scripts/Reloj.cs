using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reloj : MonoBehaviour
{
    public static Reloj instance;
    [Tooltip("Tiempo inicial en segundos")]
    public int tiempoInicial;

    [Tooltip("Visualizar horas de reloj")]
    public bool incluirHoras = false;

    [Tooltip("Escala del tiempo de reloj")]
    [Range(-10.0f, 10.0f)]
    public float escalaDelTiempo = 1;

    private Text myText;
    private float tiempoDelFrameConTimeScale = 0f;
    public float tiempoAMostrarEnSegundos = 0f;
    private float escalaDeTiempoAlPausar, escalaDelTiempoInicial;
    public bool estaPausado = false;
    private bool eventoTiempoCeroInvocado = false;
    public bool sonarAqui = true;
    //Crear delegado para el tiempo cero
    public delegate void AccionTiempoCero();
    //Create evento
    public static event AccionTiempoCero AlLlegarACero;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update

    void Start()
    {
        //EStablecer la escala de tiempo original
        escalaDelTiempoInicial = escalaDelTiempo;

        //Get the text component
        myText = GetComponent<Text>();

        //Inicializamos la variable
        tiempoAMostrarEnSegundos = tiempoInicial;

        ActualizarReloj(tiempoInicial);
    }

    // Update is called once per frame
    void Update()
    {
        NetworkManager nM = NetworkManager.instance.GetComponent<NetworkManager>();
        
        if (!estaPausado)
        {
            // La siguiente variable representa el tiempo de cada frame considerando la escala de tiempo
            tiempoDelFrameConTimeScale = Time.deltaTime * escalaDelTiempo;

            //La siguiente variable va acumulando el tiempo transcurrido para luego mostrarlo en el reloj
            tiempoAMostrarEnSegundos += tiempoDelFrameConTimeScale;
            //ActualizarReloj(tiempoAMostrarEnSegundos);
            ActualizarReloj(nM.clockTurn.x);


            //nM.CommandStopClock(new Vector3(tiempoAMostrarEnSegundos, 1, nM.clockTurn.z));
            //Debug.Log(tiempoAMostrarEnSegundos);
            if (tiempoAMostrarEnSegundos < 3 && sonarAqui)
            {
                //Debug.Log(tiempoAMostrarEnSegundos);
                FindObjectOfType<AudioManager>().Play("ClockCero");
                //Debug.Log("Debi sonor");
                sonarAqui = false;
            }
        }

    }

    public void ActualizarReloj(float tiempoEnSegundos)
    {
        int minutos = 0;
        int segundos = 0;
        string textoDelReloj;

        int horas = 0;
     
        if (tiempoEnSegundos <= 0 && !eventoTiempoCeroInvocado)
        {
            if (AlLlegarACero != null)
            {
                AlLlegarACero();
            }
            eventoTiempoCeroInvocado = true;
        }
        if (tiempoEnSegundos < 0)
        {
            tiempoEnSegundos = 0;
        }
        if (incluirHoras)
        {
            // Calcular horas, minutos y segundos 
            horas = (int)tiempoEnSegundos / 3600;
            minutos = (int)(tiempoEnSegundos - (horas * 3600)) / 60;
            segundos = (int)tiempoEnSegundos % 60;
            // Crear la cadena de caracteres con 2 digitos para las horas
            textoDelReloj = horas.ToString("00") + ":" + minutos.ToString("00") + ":" + segundos.ToString("00");
        }
        else
        {
            //Calcular minutos y segundos
            minutos = (int)tiempoEnSegundos / 60;
            segundos = (int)tiempoEnSegundos % 60;

            //Crear la cadena de caracteres con 2 digitos para los minutos y segundos separados por :
            textoDelReloj = minutos.ToString("00") + ":" + segundos.ToString("00");
        }
        

        //Actualizar el componente de texto para mostrar;
        myText.text = textoDelReloj;
    }

    public void Pausar()
    {
        if (!estaPausado)
        {
            estaPausado = true;
            escalaDeTiempoAlPausar = escalaDelTiempo;
            escalaDelTiempo = 0;
        }
    }

    public void Continuar()
    {
        if (estaPausado)
        {
            estaPausado = false;
            escalaDelTiempo = escalaDeTiempoAlPausar;
        }
    }

    public void Reiniciar()
    {
        estaPausado = false;
        eventoTiempoCeroInvocado = true;
        escalaDelTiempo = escalaDelTiempoInicial;
        tiempoAMostrarEnSegundos = tiempoInicial;
        ActualizarReloj(tiempoAMostrarEnSegundos);
    }
}
