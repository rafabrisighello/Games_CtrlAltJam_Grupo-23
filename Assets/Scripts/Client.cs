using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Client : MonoBehaviour
{
    [SerializeField]
    private int[] clientIndexes = { 0, 1, 2, 3, 4, 5, 6, 7, 8};

    [SerializeField]
    private Sprite[] avatarArray;
    private Sprite[,] colateralArray;

    [SerializeField]
    private string[] frases;

    [SerializeField]
    public List<int> clientsLeft = new List<int>();

    [SerializeField]
    private int currentIndex;
    
    [SerializeField]
    private TextBox textBox;

    [SerializeField]
    private string currentText;

    [SerializeField]
    private ImageBox imageBox;

    [SerializeField]
    private Sprite currentImage;

    [SerializeField]
    private string[] currentFrases;

    [SerializeField]
    private int[] currentResults;

    [SerializeField]
    private GameLoop gameloop;

    [SerializeField]
    private Sprite[] assetArray;

    [SerializeField]
    private bool proceed = false;

    private System.Random random;

    public event Action OnClientLoaded;
    public event Action OnClientEndAction;
    public event Action OnClientEmptyAction;
    public event Action OnBossDismiss;
    public event Action OnBossProceedAction;

    private void Awake()
    {
        foreach (int index in clientIndexes)
        {
            clientsLeft.Add(index);
        }

        Debug.Log(clientsLeft.Count);
        currentFrases = new string[9] { "", "", "", "", "", "", "", "", "" };
        colateralArray = new Sprite[9,3];
        AssetInitialize();
    }

    private void OnEnable()
    {
        gameloop = GameObject.FindWithTag("GameManager").GetComponent<GameLoop>();
        gameloop.OnBossIntroAction += BossIntro;
        gameloop.OnGameStartAction += ChangeClient;
        gameloop.OnMedAppliedAction += Colateral;
        gameloop.OnChangeClientAction += ChangeClient;

        random = new System.Random();
    }

    public int GetClientIndex()
    {
        return currentIndex;
    }

    private void BossIntro()
    {
        StartCoroutine(Boss());
    }

    public void ChangeClient()
    {
        Debug.Log("Change Client");
        if (clientsLeft.Count > 0)
        {
            currentIndex = RandomChoose();
            clientsLeft.Remove(currentIndex);
            currentImage = avatarArray[currentIndex];
            imageBox.SetSprite(currentImage);
            GetClientInfo(currentIndex);
            currentText = currentFrases[0];
            textBox.SetCurrentText(currentText);
            Debug.Log(clientsLeft.Count);
        }
        StartCoroutine(ClientLoaded());
    }

    private int RandomChoose()
    {
        int randomIndex = random.Next(clientsLeft.Count);
        int clientChosen = clientsLeft[randomIndex];
        return clientChosen;
    }


    private void Colateral()
    {
        int choice = GameObject.FindWithTag("Medicine").GetComponent<Medicine>().GetMed();
        Debug.Log("Remedio escolhido: " + choice);
        StartCoroutine(ColateralRoutine(choice));
    }

    private void ClientCheck()
    {
        if (clientsLeft.Count > 0)
        {
            OnClientEndAction();
        }
        else OnClientEmptyAction();
    }

    IEnumerator ClientLoaded()
    {
        yield return new WaitForSeconds(3.0f);
        OnClientLoaded();
    }

    IEnumerator ColateralRoutine(int choice)
    {
        yield return new WaitForSeconds(1.0f);
        currentImage = colateralArray[currentIndex, choice];
        imageBox.SetSprite(currentImage);
        GetClientInfo(currentIndex);
        currentText = currentFrases[choice + 1];
        textBox.SetCurrentText(currentText);
        yield return new WaitForSeconds(1.0f);
        gameloop.SetReputation(currentResults[choice]);
        yield return new WaitForSeconds(1.0f);
        ClientCheck();
    }

    IEnumerator Boss()
    {
        yield return new WaitForSeconds(1.0f);
        currentImage = avatarArray[9];
        imageBox.SetSprite(currentImage);
        currentText = "Gerente: Iai meu chapa.Tu que é o novo estagiário, né?";
        textBox.SetCurrentText(currentText);
        OnBossProceedAction();
        yield return new WaitWhile(() => !proceed);
        ToggleProceed();
        currentText = "Gerente: Quando chegar alguém tu ouve o problema da pessoa e pega os remédios pra resolver esse problema específico.";
        textBox.SetCurrentText(currentText);
        yield return new WaitWhile(() => !proceed);
        ToggleProceed();
        currentText = "Gerente: Só presta atenção na descrição de cada remédio porque TODOS tem algum efeito colateral, então se o cliente não gostar a farmácia fica mal vista.";
        textBox.SetCurrentText(currentText);
        yield return new WaitWhile(() => !proceed);
        ToggleProceed();
        currentText = "Gerente: A sorte é que o patrão não é mão de vaca e deixa no estoque pelo menos uns 3 remédios pra cada problema, só escolher o menos pior e torcer pra dar bom. Entendeu?";
        textBox.SetCurrentText(currentText);
        yield return new WaitWhile(() => !proceed);
        ToggleProceed();
        OnBossDismiss();
    }

    public void GetClientInfo(int index)
    {
        string[] frases = new string[4];
        int[] deltaReputation = new int[3];

        switch (index)
        {
            case 0:
                frases[0] = "Depois de algum tempo ali, aparece um Bodybuilder. \nBodybuilder com baixa autoestima: Iae men. Ei, tô com um resfriado potente aqui, ó. Me arranja um remedinho ai, vai lá? Tenho que ir pra academia jajá, posso gripar não.";
                frases[1] = "Enquanto estava na academia, ele acabou sofrendo um... episódio constrangedor por conta do efeito colateral do remédio. O vexame que passou com certeza não agradou o cliente.";
                deltaReputation[0] = 0;
                frases[2] = "Enquanto estava na academia, ele percebeu que não conseguia levantar o ferro direito, por mais que tentasse. A vergonha de não conseguir fazer os exercícios com certeza não agradou o cliente.";
                deltaReputation[1] = 0;
                frases[3] = "Enquanto estava na academia, ele suava muito em cada exercício, mais do que os demais. Todos que assistiam davam palavras de motivação e comemorava o aparentemente gigantesco esforço que era aplicado. Isso com certeza agradou o cliente.";
                deltaReputation[2] = 1;
                break;

            case 1:
                frases[0] = "Nadadora profissional: Boa tarde, você teria algum remédio para dor? Tô com uma dor chata no ombro e tenho uma prova daqui a pouco.";
                frases[1] = "Na competição, logo depois de saltar, ela começa a sentir uma sede abissal e começa a engolir a água. E engole tanta água que a piscina seca e ela é desclassificada por atrapalhar a competição. Isso com certeza não agradou a cliente.";
                deltaReputation[0] = 0;
                frases[2] = "Na competição, logo depois de saltar, ela começa a se debater fortemente dentro da água, fazendo mais movimentos que os outros e de alguma forma ultrapassando todo mundo. Isso com certeza agradou a cliente.";
                deltaReputation[1] = 1;
                frases[3] = "Na competição, logo depois de saltar, ela começa a sentir uma coceira interminável e se desconcentra, se coçando na piscina enquanto seus concorrentes terminam a prova e ela fica em último lugar. Isso com certeza não agradou a cliente.";
                deltaReputation[2] = 0;
                break;

            case 2:
                frases[0] = "Palhaço que não é engraçado: Ei amigão, eu tô com uma febre chata aqui e ta querendo aumentar, tu não tem nada pra me ajudar com isso não? Tenho uma apresentação infantil daqui a pouco.";
                frases[1] = "Enquanto estava na apresentação com as crianças ele enche um balão, mas por conta do efeito colateral do remédio o interior do balão se enche de saliva. O balão estoura, molhando a todos, e uma guerra de balões de água é declarada. Ver os risos das crianças com certeza agradou o cliente.!";
                deltaReputation[0] = 0;
                frases[2] = "Enquanto estava na apresentação com as crianças, ele tenta encher um balão, mas por conta do efeito colateral do remédio, não consegue. A falha com certeza não agradou o cliente.";
                deltaReputation[1] = 0;
                frases[3] = "Enquanto estava na apresentação com as crianças ele enche um balão e o dobra em formato de cachorro. Por conta do efeito colateral do remédio, o palhaço começa a achar que há de fato um cachorro ali, o que o assusta e estraga a apresentação. O fracasso de sua apresentação com certeza não agradou o cliente.";
                deltaReputation[2] = 1;
                break;
            case 3:
                frases[0] = "Programador calvo: Olá. Você teria alguma coisa para dor nas costas? Tenho que terminar um trabalho nessa madrugada";
                frases[1] = "Por conta do efeito colateral, ele consegue passar a noite inteira programando sem e termina o trabalho sem nem precisar tomar café. Ter entregue o trabalho a tempo e sem se sentir cansado com certeza agradou o cliente.";
                deltaReputation[0] = 1;
                frases[2] = "Tentando terminar a tempo, ele começa a perceber que o seu pouco cabelo está caindo e entra em desespero. Ele passa a madrugada inteira buscando formas de impedir sua perca total de fios capilares e acaba perdendo o prazo de sua entrega. Sua falha com o prazo e sua calvície avançada com certeza não agradou o cliente.";
                deltaReputation[1] = 0;
                frases[3] = "Por conta do efeito colateral porém, ele não consegue mais digitar, já que seus dedos se tornam como borracha. A incapacidade de programar com certeza não agradou o cliente.";
                deltaReputation[2] = 0;
                break;
            case 4:
                frases[0] = "Religiosa religiosa: Olá, meu caro. Você teria algum remédio para alergia? Estou atrasada para a missa e preciso de alguma coisa urgente, se não eu não vou conseguir ir hoje.";
                frases[1] = "Enquanto estava na missa, o soluço que vinha esporadicamente fazia muito barulho e atrapalhava a celebração, o que gerou um mal-estar na religiosa. A situação com certeza não agradou a cliente.";
                deltaReputation[0] = 0;
                frases[2] = "Enquanto estava na missa, de súbito sua pele começou a brilhar, o que chamou a atenção de todos. Por conta do efeito colateral do remédio, a religiosa foi beatificada. O ocorrido com certeza agradou a cliente.";
                deltaReputation[1] = 1;
                frases[3] = "Enquanto estava na missa, ela pegou no sono, roncando bem alto e atrapalhando a celebração e todos os que estavam presentes. O constrangimento de ter dormido na missa com certeza não agradou a cliente.";
                deltaReputation[2] = 0;
                break;

            case 5:
                frases[0] = "Mochileiro apressado: Opa! Diga ai, meu bom! Ei, eu to andei meio fraco esses dias e descobri que tô anémico, mas hoje eu vou tentar quebrar o recorde de uma escalada. Tu não tem nenhum remedinho ai pra me ajudar com essa anemia não?";
                frases[1] = "Enquanto estava escalando, o efeito colateral fez com que ele conseguisse superar trechos que demorariam bastante para ser ultrapassdos. Com os saltos mais altos do que o humanamente possível, ele quebrou o recorde de escalada mais rápida. O feito com certeza agradou o cliente.";
                deltaReputation[0] = 1;
                frases[2] = "Enquanto estava escalando, ele sentiu uma grande fome, e teve que parar para comer. Por conta do efeito colateral forte, todo o seu suprimento foi esgotado e ele teve que voltar para se reabastecer. Não conseguir tentar quebrar o recorde de escalada mais rápida com certeza não agradou o mochileiro.";
                deltaReputation[1] = 0;
                frases[3] = "Enquanto estava escalando, ele se sentiu tonto e começou a enxergar as pessoas que estavam ao seu redor como pequenas montanhas que deveriam ser escaladas. As autoridades foram chamadas. Não conseguir tentar quebrar o recorde de escalada mais rápida e ainda ser preso com certeza não agradou o cliente.";
                deltaReputation[2] = 0;
                break;
            case 6:
                frases[0] = "Duquesa narcisista: Olá, plebeu. Nos últimos dias uma terrível queda de cabelo se mostrou evidente em minha magnifica pessoa. Dê-me algo para resolver isto e será recompensado.";
                frases[1] = "Quando voltou para sua nobre morada, percebeu que todos aqueles com quem ela conversava viravam pedra. Ao se olhar no espelho, a última coisa que ela viu foi algumas serpentes saindo de sua cabeça. A visão com certeza não agradou a cliente.";
                deltaReputation[0] = 0;
                frases[2] = "Quando voltou para sua nobre morada, sentiu um peso vindo de sua cabeça e ao se olhar no espelho vislumbrou uma linda cabeleira que crescia mais e mais. Mas sua alegria durou pouco, já que os cabelos não paravam de crescer. Ter que ir ao salão de beleza duas vezes ao dia acabou com sua fortuna, e a cabeluda miséria com certeza não agradou a cliente.";
                deltaReputation[1] = 0;
                frases[3] = "Quando voltou para sua nobre morada, percebeu que seu cabelo agora estava forte, brilhante e de uma cor primária forte que mudava para outra e depois outra. O vislumbre no visual capilar com certeza agradou a cliente.";
                deltaReputation[2] = 1;
                break;
            case 7:
                frases[0] = "Vampira sangue-bom: Olá, mortal. Recentemente me envolvi num acidente com espelhos e a luz do sol, o que me causou uma queimadura bem dolorida. Tem alguma coisa ai pra me ajudar?";
                frases[1] = "Saindo da farmácia e atravessando a cidade, a vampira busca uma vítima para se alimentar, mas por conta do efeito colateral do remédio não consegue morder ninguém. A fome que se sucedeu por conta do fármaco com certeza não agradou a cliente. ";
                deltaReputation[0] = 0;
                frases[2] = "Saindo da farmácia e atravessando a cidade, a vampira busca uma vítima para se alimentar. Um caçador de vampiros vê a alimentação e a ataca, mas por conta do fármaco, ela espirra no agressor, que acaba se transformando em um vampiro também. Ganhar um parceiro de caçada com certeza agradou a cliente. ";
                deltaReputation[1] = 1;
                frases[3] = "Saindo da farmácia e atravessando a cidade, a vampira busca uma vítima para se alimentar. Um caçador de vampiros vê a alimentação e a ataca, mas por conta do fármaco, ela não vê que está a ponto de ser atacada. A estaca em seu peito com certeza não agradou a cliente.";
                deltaReputation[2] = 0;
                break;
            case 8:
                frases[0] = "Robô de festa: Saudações, humano. Estou apresentando problemas no sistema interno de refrigeração. Sei que este é um lugar de atendimento à humanos, mas talvez você possa me ajudar.";
                frases[1] = "Ele parte para seu trabalho de festa e simplesmente arrasa com os novos passos de dança que a atualização deu para ele. Seu sucesso o torna famoso e um astro. Ter alcançado o ápice tecnológico em termos da arte da dança com certeza agradou o cliente.";
                deltaReputation[0] = 1;
                frases[2] = "Ele parte para seu trabalho de festa e começa a demonstrar traços de dominação mundial. Depois de algumas horas ele já tinha anexado alguns países pequenos e estava por trás de pelo menos duas guerras intercontinentais. A destruição humana com certeza agradou o cliente (mas não foi a melhor opção).";
                deltaReputation[1] = 0;
                frases[3] = "Logo depois ele começa a se tremer e seus olhos se acendem. A última coisa que você se lembra é de um grande clarão. Ter se autodestruído com certeza não agradou o cliente.";
                deltaReputation[2] = 0;
                break;
            case 9:
                frases[0] = "Gerente: Iai meu chapa. Tu que é o novo estagiário, né? \n Já sabe como tudo aqui funciona ou quer que eu te explique?";
                break;
        }

        currentFrases = frases;
        currentResults = deltaReputation;
    }

    private void AssetInitialize()
    {
        colateralArray[0, 0] = assetArray[1];
        colateralArray[0, 1] = assetArray[2];
        colateralArray[0, 2] = assetArray[3];

        colateralArray[1, 0] = assetArray[5];
        colateralArray[1, 1] = assetArray[6];
        colateralArray[1, 2] = assetArray[7];

        colateralArray[2, 0] = assetArray[9];
        colateralArray[2, 1] = assetArray[10];
        colateralArray[2, 2] = assetArray[11];

        colateralArray[3, 0] = assetArray[13];
        colateralArray[3, 1] = assetArray[14];
        colateralArray[3, 2] = assetArray[15];

        colateralArray[4, 0] = assetArray[17];
        colateralArray[4, 1] = assetArray[18];
        colateralArray[4, 2] = assetArray[19];

        colateralArray[5, 0] = assetArray[21];
        colateralArray[5, 1] = assetArray[22];
        colateralArray[5, 2] = assetArray[23];

        colateralArray[6, 0] = assetArray[25];
        colateralArray[6, 1] = assetArray[26];
        colateralArray[6, 2] = assetArray[27];

        colateralArray[7, 0] = assetArray[29];
        colateralArray[7, 1] = assetArray[30];
        colateralArray[7, 2] = assetArray[31];

        colateralArray[8, 0] = assetArray[33];
        colateralArray[8, 1] = assetArray[34];
        colateralArray[8, 2] = assetArray[35];
    }

   public void ToggleProceed()
    {
        if (proceed)
        {
            proceed = false;
        }
        else proceed = true;
    }

}
