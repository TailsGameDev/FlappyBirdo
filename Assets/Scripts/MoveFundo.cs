using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Este Script faz dois fundos se deslocarem dando ideia de continuidade. Como o fundo do Flappy Bird e SpaceShooter
public class MoveFundo : MonoBehaviour {

    //numFundos é o número de Fundos, short é um tipo com menos bits que int
    public static short numFundos;

    //novoFundo será o Fundo instanciado por esse script. É o segundo fundo.
    GameObject novoFundo;

    //a velocidade do fundo;
    public float vel;

    //z será o componente z da transform.position. Ele será usado mais de uma vez
    float z;

    GameObject gatilhoPMoveFundo;

	void Start () {
        //capturando o valor e armazenando.
        z = transform.position.z;

        //caso seja o primeiro fundo do jogo, faça o novoFundo
        if (GameObject.FindGameObjectWithTag("G") != null)
        {
            DestroyImmediate(GameObject.FindGameObjectWithTag("G"));
            //esse numFundos deu o resultado que eu queria para gerar os nomes, mas não sei porquê.
            numFundos = 1;
            InstanciaFundo();
            numFundos = 2;

        }

        // gerencia o numFundos para gerar nomes adequados aos gameObjects
        if (numFundos < 2)
        {
            numFundos += 1;
        } else
        {
            numFundos -= 1;
        }
	}
	
	void Update () {

        //acabei colocando esse trecho aqui. Tinha que estar em algo que permanecesse na cena
        if (Bateu.acabou && Input.anyKey)
        { //ele verifica se o jogo acabou, e quando uma tecla é apertada, recarrega a cena
            Bateu.acabou = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //x será o incremento em x a cada frame. Multiplica-se pelo tempo do último frame para que a posição
        //não dependa do tempo de execução do frame. a Velocidade fica em unidades por segundo, dessa forma.
        float x = transform.position.x - vel * Time.deltaTime;

        //decrementa a posição
        transform.position = new Vector3(x , 0, z);

        //se a posição em x é menor que a escala, ou seja, se tá muito pra esquerda.
        if(-transform.position.x > transform.localScale.x)
        {
            //faça com que o novoFundo faça um novo novoFundo
            novoFundo.SendMessage("InstanciaFundo");

            //destrua esse fundo
            Destroy(this.gameObject);
        }
	}

    
    private void LateUpdate()
    {
        //assegurando que não haja crash
        if (novoFundo == null)
            return;

        //se estão distantes, aproxime eles para que não fique uma linha os separando.
        if(Mathf.Abs(this.transform.position.x - novoFundo.transform.position.x) > this.transform.localScale.x)
        {
            novoFundo.transform.position -= new Vector3(0.02f,0,0);
        }
    }

    public void InstanciaFundo()
    {
        //instancie o fundo e guarde na variável novoFundo
        novoFundo = Instantiate(Resources.Load("FundoMesh")) as GameObject;

        //inc é a posição em x a ser colocada a instância
        float inc = transform.position.x + transform.localScale.x;

        //ajuste a posição
        novoFundo.transform.position = new Vector3(inc, 0, z);

        //troque o nome pra algo melhot que Fundo (Clone), até para evitar o futuro Fundo (Clone)(Clone)...
        novoFundo.gameObject.name = "Fundo " + numFundos;

        //faz com que a instância tenha a mesma velocidade que este objeto
        novoFundo.GetComponent<MoveFundo>().vel = vel;
    }
}
