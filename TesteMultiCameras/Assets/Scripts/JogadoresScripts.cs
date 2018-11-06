using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadoresScripts : MonoBehaviour {

    //Definindo parametros de velocidade para movimentacao
    public float velocidade;
    //Definindo parametros para controler de players
    public static int idJogadorAtual;
    public GameObject jogadorAtual;
    //Criando uma variavel publica que é do tipo este script para conseguir acessar o mesmo em qualquer local
    public static JogadoresScripts instance;
	void Start () {
        //Utilizado para referencia a variavel a este script
        if (instance == null)
        {
            instance = this;
        }

        //Setando o primeiro jogador
        idJogadorAtual = 1;
        //Pegando minha variavel que recebe um gameObject e atribuio um gameObject que busco por Tag.
        jogadorAtual = GameObject.FindGameObjectWithTag("Player1");
	}
	
	void Update () {
        if(idJogadorAtual == 1) {
            //Pegando dados de X e Z para movimentação do player
            float x1 = Input.GetAxis("Horizontal") * velocidade * Time.deltaTime;
            float z1 = Input.GetAxis("Vertical") * velocidade * Time.deltaTime;
            //Aqui eu pego meu jogadorAtual (Que é uma variavel que recebe gameObject)
            //E irei movimentar somente este jogador com os parametros anteriores
            jogadorAtual.transform.Translate(x1, 0.0f, z1);
        }else if (idJogadorAtual == 2)
        {
            float x2 = Input.GetAxis("HorizontalPlayer2") * velocidade * Time.deltaTime;
            float z2 = Input.GetAxis("VerticalPlayer2") * velocidade * Time.deltaTime;
            //Aqui eu pego meu jogadorAtual (Que é uma variavel que recebe gameObject)
            //E irei movimentar somente este jogador com os parametros anteriores
            jogadorAtual.transform.Translate(x2, 0.0f, z2);
        }

        //Mudança de Jogador a partir de um input, no caso o de Jump
        if (Input.GetButtonDown("Jump"))
        {
            //Se ele for o jogador 1
            if(idJogadorAtual == 1)
            {
                //Coloque ele agora para player 2 pois ele decidiu que quer mudar o Player
                idJogadorAtual = 2;
                //Coloque o gameObject do Player para o Player2
                jogadorAtual = GameObject.FindGameObjectWithTag("Player2");
            }
            else
            //Se o player nao for 1 logo ele é 2
            {
                //Entao troque seu ID para o Player1
                idJogadorAtual = 1;
                jogadorAtual = GameObject.FindGameObjectWithTag("Player1");
            }
        }
	}

}
