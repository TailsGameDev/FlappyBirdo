using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bateu : MonoBehaviour {

    public static bool acabou;
    public float tempoDeTelaTravada;
    public float vel;
    public Text t, c;
    AudioSource AS;

	void Start () {
        acabou = false;
        tempoDeTelaTravada = 2f;
        t = GameObject.FindGameObjectWithTag("Placar").GetComponent<Text>();
        c = GameObject.FindGameObjectWithTag("CPJ").GetComponent<Text>();
        AS = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(vel, 0, 0);
            Destroy(collision.GetComponent<Collider2D>());
            Destroy(Camera.main.GetComponent<FlappyScript>());
            t.text = "Game Over. Score: " + Pontua.pontos;
            Invoke("Acaba", tempoDeTelaTravada);
            AS.Play();
        }
    }

    public void OnTriggerEnter2Dsqn()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<Rigidbody2D>().velocity = new Vector3(vel, 0, 0);
        Destroy(Player.GetComponent<Collider2D>());
        Destroy(Camera.main.GetComponent<FlappyScript>());
        t.text = "Game Over. Score: " + Pontua.pontos;
        Invoke("Acaba", tempoDeTelaTravada);
        AS.Play();
    }

    void Acaba()
    {
        c.enabled = true;
        acabou = true;
        Pontua.pontos = 0;
        MoveFundo.numFundos = 0;
    }
}
