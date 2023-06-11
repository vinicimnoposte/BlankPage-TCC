using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteDosDados : MonoBehaviour
{
    //Script da rolagem de dados
    [SerializeField] private RolagemDeDados rolagemDeDados;

    //Animators
    [SerializeField] private Animator dadoUmAnimator;
    [SerializeField] private Animator dadoDoisAnimator;

    //Resultados
    private int resultadoDadoUm;
    private int resultadoDadoDois;

    public void IniciaAnimacao()
    {
        dadoUmAnimator.enabled = true;
        dadoDoisAnimator.enabled = true;
    }

    public void ParaDados()
    {
        //Opera��es com o dado 1
        //Rola o resultado
        resultadoDadoUm = rolagemDeDados.RolarDado();
        //Desativa o animator para parar a anima��o
        dadoUmAnimator.enabled = false;
        //Reinicia a rota��o do dado
        dadoUmAnimator.gameObject.transform.rotation = Quaternion.identity;
        //Rotaciona o dado para mostrar o resultado
        dadoUmAnimator.gameObject.transform.Rotate(rolagemDeDados.FaceDoDado(resultadoDadoUm));

        //Opera��es com o dado 2
        //Rola o resultado
        resultadoDadoDois = rolagemDeDados.RolarDado();
        //Desativa o animator para parar a anima��o
        dadoDoisAnimator.enabled = false;
        //Reinicia a rot���o do dado        
        dadoDoisAnimator.gameObject.transform.rotation = Quaternion.identity;
        //Rotaciona o dado para mostrar o resultado        
        dadoDoisAnimator.gameObject.transform.Rotate(rolagemDeDados.FaceDoDado(resultadoDadoDois));
    }
}