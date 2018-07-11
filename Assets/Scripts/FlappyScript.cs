using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Esse Script Implementa o comportamento do Jogo Flappy Bird. No projeto, é um unicórnio.

public class FlappyScript : MonoBehaviour {

    //Player é o Unicórnio, personagem do jogo
    public GameObject Player;

    //comecou indica se já foi apertada uma tecla, pois antes disso, a gravidade é fica desativada.
    bool comecou;

    //rbdDPlayer é o componente Rigidbody2D do Player
    Rigidbody2D rb2DPlayer;

    //upVelocity é a velocidade que é adicionada em y quando acontece algum Input
    //tEntreObstaculos é o tempo entre spawns dos canos
    public float upVelocity, tEntreObstaculos;

    //esses são os textos que aparecem na tela
    public Text FlappyUnicorn, CliqueParaJogar, Placar;

    //canos e cano guardarão o prefab e as instâncias dos canos do jogo
    GameObject canos, cano;

    //minY e maxY são as alturas mínima e máxima para os canos serem instanciados
    public float minY, maxY;

    //velCanos é a velocidade que os canos se deslocam para a esquerda
    public float velCanos;

    public float tParaDestruirCanos;

    AudioSource AS;

	void Start () {
        //captura o componente Rigidbody2D do Player, e armazena na variável devida
        rb2DPlayer = Player.GetComponent<Rigidbody2D>();

        //No primeiro frame, o jogo não começou
        comecou = false;

        canos = Resources.Load("Canos") as GameObject;

        AS = GetComponent<AudioSource>();
	}
	
	void Update () {

        //é um evento para começar o jogo. Começa verificando se já começou.
        if (comecou == false)
        {
            //caso não tenha começado, verifica se alguma tecla foi apertada
            if (Input.anyKeyDown)
            {
                //caso alguma tecla seja apertada, o jogo inicia
                comecou = true;
                //a gravidade é ativada
                rb2DPlayer.simulated = true;
                //o Player recebe seu primeiro impulso para cima
                rb2DPlayer.velocity = Vector2.up * upVelocity;
                AS.Play();

                //os textos são desativados
                FlappyUnicorn.enabled = false;
                CliqueParaJogar.enabled = false;

                //o placar é ativado
                Placar.enabled = true;

                //Invoca repetidas vezes a função InstanciaObstaculo com o tempo tEntreObstaculos
                InvokeRepeating("InstanciaObstaculo", tEntreObstaculos, tEntreObstaculos);
            }
                //caso contrário, nada mais precisa ser executado, então o return termina o Update.
            else
            {
                return;
            }
        }
            // esse else será executado caso comecou seja true;
        else
        {
            //roda o Player um pouquinho, com a velocidade.
            Player.transform.rotation = Quaternion.Euler(0, 0, rb2DPlayer.velocity.y);

            //se alguma tecla for apertada
            if (Input.anyKeyDown)
            {   //o player ganha uma velocidade para cima.
                rb2DPlayer.velocity = Vector2.up * upVelocity;
                AS.Play();
            }

        }

    }

    void InstanciaObstaculo()
    {
        //instancia um cano, igual o objeto canos, já definido
        cano = Instantiate(canos);
        // sorteia um numero entre minY e maxY
        float y = Random.Range(minY, maxY);
        //atribui a posição desejada
        cano.transform.position = new Vector3(canos.transform.position.x, y, canos.transform.position.z);
        //atribui a velocidade velCanos ao objeto
        cano.GetComponent<Rigidbody2D>().velocity = new Vector2(-velCanos, 0);
        //Destroi depois de 15 segundos
        Destroy(cano, tParaDestruirCanos);
    }


}
