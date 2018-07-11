using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//esse script trada dos pontos quando o Player passa pelo vão entre os canos
public class Pontua : MonoBehaviour {

    public static int pontos;
    public Text p;
    AudioSource AS;

	void Start () {
        //ele usa o texto Placar
        p = GameObject.FindGameObjectWithTag("Placar").GetComponent<Text>();
        AS = GetComponent<AudioSource>();
	}


    private void OnTriggerExit2D(Collider2D collision)
    {
        //ele soma um ponto só se a velocidade do Player em x foi zero, pois se não for, significa que bateu
        if (collision.GetComponent<Rigidbody2D>().velocity.x == 0)
            { pontos += 1;
            AS.Play();
        }
        //imprime os pontos
        p.text = pontos.ToString();

        //destroi esse colisor para que não aconteça de pontuar duas vezes, apesar de que isso seria praticamente impossível
        Destroy(this.gameObject.GetComponent<Collider2D>());
        Destroy(this);
    }

}
