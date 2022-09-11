using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Medicine : MonoBehaviour
{
    [SerializeField]
    private Sprite[] medicineSprites;

    [SerializeField]
    private int currentMed;

    [SerializeField]
    private TextBox textBox;

    public event Action OnMedChosenAction;

    public void ChangeMedInfo(int medChoice)
    {
        SetMed(medChoice);
        textBox.SetCurrentText(GetMedInfo(medChoice));
        Debug.Log("Remedio escolhido: " + medChoice);
        OnMedChosenAction();
    }

    public int GetMed()
    {
        return currentMed;
    }

    public void SetMed(int index)
    {
        currentMed = index;
    }

    private string GetMedInfo(int index)
    {
        string[] medInfo = { "", "", "" };

        int clientIndex = GameObject.FindWithTag("Cliente").GetComponent<Client>().GetClientIndex();

        switch (clientIndex)
        {
            case 0:
                medInfo[0] = "\"Esse aqui vai te deixar leve\" (Causa diarréia)";
                medInfo[1] = "\"Esse vai fazer o monstro sair da jaula\" (Gera fraqueza)";
                medInfo[2] = "\"Esse vai te ajudar com a hidratação\" (Gera suor excessivo)";
                break;
            case 1:
                medInfo[0] = "\"Tem esse, bom com esportes de água\" (Gera sede excessiva)";
                medInfo[1] = "\"Michael Felps usa muito esse\" (Causa movimentos involuntários)";
                medInfo[2] = "\"Esse não tem contra-indicação\" (Causa coceira excessiva)";
                break;
            case 2:
                medInfo[0] = "\"Esse é de estourar a boca do balão\" (Causa salivação excessiva)";
                medInfo[1] = "\"Esse é muito bom, de tirar o fôlego\" (Gera falta de ar)";
                medInfo[2] = "\"Te recomendo esse aqui, nunca falha\" (Causa alucinação)";
                break;
            case 3:
                medInfo[0] = "\"Com esse você passa a noite toda sem dor\" (Causa insônia)";
                medInfo[1] = "\"Esse é tão bom que vai te deixar de cabelo em pé\" (Gera queda de cabelo)";
                medInfo[2] = "\"Com isso aqui você vai ficar bem relaxado\" (Causa elasticidade excessiva)";
                break;
            case 4:
                medInfo[0] = "\"Esse é a solução perfeita para a senhora\" (Causa soluços esporádicos)";
                medInfo[1] = "\"Esse é brilhante, com certeza vai lhe ajudar\" (Gera brilho epidérmico)";
                medInfo[2] = "\"Esse aqui vai acalmar os sintomas, pode confiar\" (Causa sono excessivo)";
                break;
            case 5:
                medInfo[0] = "\"Isso vai dar um salto na sua saúde\" Causa pulos altos demais";
                medInfo[1] = "\"Isso vai te ajudar a comer melhor\" Gera fome excessiva";
                medInfo[2] = "\"Esse é muito bom, vai te ajudar a escalar qualquer coisa\" Causa alucinação";
                break;
            case 6:
                medInfo[0] = "\"Todos ficarão paralisados com sua beleza\" (Transforma quem olha em pedra)";
                medInfo[1] = "\"Com isso, queda de cabelo nunca mais será um problema\" (Cabelo cresce indefinidamente)";
                medInfo[2] = "\"Seu cabelo terá brilho próprio\" (Cabelo brilhante)";
                break;
            case 7:
                medInfo[0] = "\"Esse é incrível, de cair o queixo\" (Gengiva mais fraca)";
                medInfo[1] = "\"Esse vai te ajudar, é bem forte\" (Super espirros)";
                medInfo[2] = "\"Esse vai acabar com seus problemas\" (Baixa visão)";
                break;
            case 8:
                medInfo[0] = "\"Acho que isso aqui com certeza vai te ajudar\" (update dance mode)";
                medInfo[1] = "\"Toma isso, acho que dá certo\" (update domination mode)";
                medInfo[2] = "\"Com isso seu problema vai pro espaço\" (update 01110100 01101111 01101101 01100101 defender)";
                break;
            case 9:
                medInfo[0] = "Info0";
                medInfo[1] = "Info1";
                medInfo[2] = "Info2";
                break;
        }

        return medInfo[index];
    }

}
