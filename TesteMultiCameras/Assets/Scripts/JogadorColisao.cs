using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorColisao : MonoBehaviour {

    //Quando eu colidir com algo
    private void OnTriggerEnter(Collider col)
    {
        //Se meu idJogador Atual for 1 e eu colidir com um cubo com tag Verde
        if(JogadoresScripts.idJogadorAtual==1 && col.CompareTag("Verde"))
        {
            //Destruo o que eu colidi
            Destroy(col.gameObject);

        //Se for O Player2 e colidir com cubos com Tag Azul
        }else if (JogadoresScripts.idJogadorAtual==2 && col.CompareTag("Azul"))
        {
            //Destruo o que eu colidi
            Destroy(col.gameObject);
        }
    }

    //estaNoChaoE = Physics2D.Linecast(transform.position, sensorChaoE.position,
    //1 << LayerMask.NameToLayer("Chao"));
}
