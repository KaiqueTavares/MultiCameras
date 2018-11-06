using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoScript : MonoBehaviour
{

    public float velocidade;
    public float distVisao;
    private Vector3 posicaoInicial;
    private GameObject alvo;

    public bool perseguir;
    public bool patrulhar;
    public bool retornar;

    private bool possovoltar;

    SpriteRenderer sr;
    void Start()
    {
        posicaoInicial = transform.position;
        sr = GetComponent<SpriteRenderer>();
        alvo = GameObject.Find("Player");
    }

    void Update()
    {

        float d = Vector3.Distance(transform.position, alvo.transform.position);

        if (d < distVisao)
        {
            //Andando atras do meu alvo
            transform.position = Vector3.Lerp(transform.position, alvo.transform.position,
                Mathf.Abs(velocidade) * Time.deltaTime);

            Debug.Log("Entrei na distancia menor que minha area de vista");
            //Posso retornar ao meu ponto de partida
            retornar = true;

        }
        else
        {
            //Meu retornar é falso
            if (!retornar)
            {
                //Fique em patrulha novamente
                Debug.Log("Retornar é falso");
                transform.Translate(Vector3.left * velocidade * Time.deltaTime);

                if (transform.position.x < posicaoInicial.x - 3.0f || transform.position.x > posicaoInicial.x + 3.0f)
                {
                    velocidade *= -1;
                    if(velocidade == -1.5)
                    {
                        sr.flipX = false;
                    }
                    else
                    {
                        sr.flipX = true;
                    }
                }
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, posicaoInicial, velocidade * Time.deltaTime);

                if (Vector3.Distance(transform.position, posicaoInicial) < 0.1f)
                {
                    Debug.Log("Retornou");
                    retornar = false;
                }

            }
            retornar = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Projetil"))
        {
            Destroy(this.gameObject);
        }

        if (col.collider.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
