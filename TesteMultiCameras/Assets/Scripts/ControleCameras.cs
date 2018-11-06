using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleCameras : MonoBehaviour {

    //Variaveis para Split de Camera
    public Camera cameraGeral, cameraPlayer1, cameraPlayer2;
    public float limite;

    //Variaveis para Zoom da Camera de acordo com a distancia dos Players
    public float velocidadeCAM;
    public float distanciaZ;
    public float fovMin, fovMax;

    private float zoomDistanciaJogadores;
    private void Start()
    {
        //Executo o Camera Zoom logo no começo do game, não é necessário
        StartCoroutine(CameraZoom());
    }

    void Update () {

        //Atribuindo uma variavel privada para a distancia dos players
        zoomDistanciaJogadores = Vector3.Distance(cameraPlayer1.transform.position, cameraPlayer2.transform.position);

        //Se minha distancia for maior que o limite que eu coloquei, a tela deve dar Split
        if(zoomDistanciaJogadores > limite)
        {
            cameraGeral.enabled = false;
            cameraPlayer1.enabled = true;
            cameraPlayer2.enabled = true;
        }
        else
        {
            cameraGeral.enabled = true;
            cameraPlayer1.enabled = false;
            cameraPlayer2.enabled = false;
        }

        //Camera que segue o personagem
        Segue();
    }

    void Segue()
    {
        //Se eu for o jogador 1
        if (JogadoresScripts.idJogadorAtual == 1) {
            //Pego sempre a posicaoDo Player e coloco em um Vector3
            //A Sacada aqui esta em pegar o Y da camera, pois o player não pula
            //A Outra sacada é em adicionar ao Position do personagem + a distancia em Z que eu quero da camera
            Vector3 posicaoJogador = new Vector3(JogadoresScripts.instance.jogadorAtual.transform.position.x,
                cameraPlayer1.transform.position.y, JogadoresScripts.instance.jogadorAtual.transform.position.z + distanciaZ);

            //Vou setar a posicao utilizando um LERP
            //Passando a position da camera, para o Vector do Player (Localizacao), vezes a velocidade
            cameraPlayer1.transform.position =
                Vector3.Lerp(cameraPlayer1.transform.position,
                posicaoJogador, velocidadeCAM * Time.deltaTime);

        } else if (JogadoresScripts.idJogadorAtual == 2)
        {
            Vector3 posicaoJogador2 = new Vector3(JogadoresScripts.instance.jogadorAtual.transform.position.x,
            cameraPlayer2.transform.position.y, JogadoresScripts.instance.jogadorAtual.transform.position.z + distanciaZ);

            cameraPlayer2.transform.position =
                Vector3.Lerp(cameraPlayer2.transform.position,
                posicaoJogador2, velocidadeCAM * Time.deltaTime);
        }
    }

    IEnumerator CameraZoom()
    {
        //Aguardo alguns segundos
        yield return new WaitForSeconds(0.05f);

        //Se meu FOV da camera geral for menor que o meu fov maximo
        //Tenho que dar Zoom na camera
        if (cameraGeral.fieldOfView < fovMax && zoomDistanciaJogadores > 15.0f)
        {
            //Estou pegando o fov e somando com a distancia que coloquei entre os jogadores dividido por 5
            //Poderia ser um fov X setado tb.
            cameraGeral.fieldOfView = cameraGeral.fieldOfView + zoomDistanciaJogadores / 5;
        }
        //Tirando o Zoom da camera quando meu fov atingi o minimo de FOV
        else if (cameraGeral.fieldOfView > fovMin && zoomDistanciaJogadores < 5.0f)
        {
            //Subtraio do meu fov a distancia entre os players
            cameraGeral.fieldOfView = cameraGeral.fieldOfView - zoomDistanciaJogadores;
        }
        //Inicio a Corountine novamente para ela ficar em loop
        StartCoroutine(CameraZoom());
    }
}
