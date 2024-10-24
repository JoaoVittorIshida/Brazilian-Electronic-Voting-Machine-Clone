using Microsoft.VisualBasic.Devices;
using System.Drawing.Printing;
using System.Media;

namespace Urna_eletronica
{
    public partial class Urna : Form
    {
        public Urna()
        {
            InitializeComponent();
        }

        string NumVoto = "";
        int candidato1 = 0, candidato2 = 0, branco = 0, nulo = 0;
        //finalvoto representa se a animação de final de votação está ativa ou não, se sim, todos os botões tem suas funções pausadas.
        bool finalvoto = false;

        private static void SomTeclado()
        {
            //Toca som do click do teclado
            using (MemoryStream ms = new MemoryStream(Properties.Resources.clickteclado))
            {
                SoundPlayer player = new SoundPlayer(ms);
                player.Play();
            }
        }
        private void TesteVoto(string NumVoto)
        {
            if (finalvoto == false)
            {
                //Exibição de instruções de voto, exibe preenchendo as duas casas ou selecinando Branco
                if ((NumeroCand.Text != "" && NumeroCand2.Text != "") || NumVoto == "BRANCO")
                {
                    SeuVotoPara.Visible = true;
                }
                else
                {
                    SeuVotoPara.Visible = false;
                }

                if (NumeroCand2.Text != "" || NumVoto == "BRANCO")
                {
                    Instrucoes.Visible = true;
                }
                else
                {
                    Instrucoes.Visible = false;
                }
                //Garante a exibição correta da interface de acordo com o voto
                if (NumVoto != "BRANCO")
                {
                    NumeroCand.Visible = true;
                    NumeroCand2.Visible = true;
                    VotoBO.Text = "";
                }

                //Testa o voto atual e realiza as devidas alterações na tela, como foto ou textos adicionais
                switch (NumVoto)
                {
                    case "10":
                        FotoPref.Image = Properties.Resources.candidato1;
                        FotoVice.Image = Properties.Resources.vice1;
                        Tela.Image = Properties.Resources.tela_urna;
                        NomeCand.Text = "Nome: Candidato 1\n\nPartido: Partido 1\n\nVice-prefeito: Vice-prefeito 1";
                        break;
                    case "20":
                        FotoPref.Image = Properties.Resources.candidato2;
                        FotoVice.Image = Properties.Resources.vice2;
                        Tela.Image = Properties.Resources.tela_urna;
                        NomeCand.Text = "Nome: Candidato 2\n\nPartido: Partido 2\n\nVice-prefeito: Vice-prefeito 2";
                        break;
                    case "BRANCO":
                        FotoPref.Image = null;
                        FotoVice.Image = null;
                        VotoBO.Text = "VOTO EM BRANCO";
                        NumeroCand.Visible = false;
                        NumeroCand2.Visible = false;
                        Tela.Image = Properties.Resources.tela_urna;
                        break;
                    case "":
                        FotoPref.Image = null;
                        FotoVice.Image = null;
                        NomeCand.Text = "";
                        Tela.Image = Properties.Resources.tela_branca;
                        break;
                    default:
                        FotoPref.Image = null;
                        FotoVice.Image = null;
                        NomeCand.Text = "";
                        if (NumeroCand2.Text != "")
                        {
                            NomeCand.Text = "NÚMERO ERRADO";
                            VotoBO.Text = "VOTO NULO";
                            Tela.Image = Properties.Resources.tela_urna;
                        }
                        break;
                }
            }
        }
       
        private void DigitoVoto(string NumDigitado)
        {
            //Função para digitar individualmente nas duas caixas de texto
            if (finalvoto == false)
            {
                if (NumVoto != "BRANCO")
                {
                    if (NumeroCand.Text == "")
                    {
                        NumeroCand.Text = NumDigitado;
                    }
                    else if (NumeroCand2.Text == "")
                    {
                        NumeroCand2.Text = NumDigitado;
                    }
                    NumVoto = NumeroCand.Text + NumeroCand2.Text;
                }
            }
        }
        private async void FimVoto()
        {
            //Esconde os elementos e exibe a barra de progresso
            using (MemoryStream ms = new MemoryStream(Properties.Resources.ClickUrna))
            {
                SoundPlayer player = new SoundPlayer(ms);
                player.Play();
            }
            finalvoto = true;
            Tela.Image = Properties.Resources.tela_branca;
            TipoCand.Visible = false;
            NumeroCand.Visible = false;
            NumeroCand2.Visible = false;
            FotoPref.Image = null;
            FotoVice.Image = null;
            Instrucoes.Visible = false;
            NomeCand.Text = "";
            VotoBO.Visible = false;
            SeuVotoPara.Visible = false;
            ProgressoVoto.Visible = true;
            while (ProgressoVoto.Value < 100)
            {
                await Task.Delay(80);
                ProgressoVoto.Value += 5;
            }
            //Termina a barra de progresso, oculta ela e exibe o FIM
            ProgressoVoto.Visible = false;
            Fim.Visible = true;
            using (MemoryStream ms = new MemoryStream(Properties.Resources.ConfirmaUrna))
            {
                SoundPlayer player = new SoundPlayer(ms);
                player.Play();
            }
            await Task.Delay(1000);
            System.Threading.Thread.Sleep(2000);
            //Pergunta se deseja continuar a votação, se não, exibe os resultados e encerra
            DialogResult result = MessageBox.Show("Deseja continuar a votação?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                MessageBox.Show($"Candidato 1: {candidato1} votos.\nCandidato 2: {candidato2} votos.\nBranco: {branco} votos.\nNulo: {nulo} votos.", "Resultado da votação");
                Application.Exit();
            }
            //Reexibe todos os elementos da votação novamente
            Tela.Image = Properties.Resources.tela_urna;
            ProgressoVoto.Value = 0;
            TipoCand.Visible = true;
            NumeroCand.Visible = true;
            NumeroCand2.Visible = true;
            VotoBO.Visible = true;
            FotoPref.Visible = true;
            Fim.Visible = false;
            finalvoto = false;
            NumVoto = "";
            NumeroCand.Text = "";
            NumeroCand2.Text = "";
            VotoBO.Text = "";
            TesteVoto(NumVoto);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DigitoVoto("1");
            SomTeclado();
            TesteVoto(NumVoto);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DigitoVoto("2");
            SomTeclado();
            TesteVoto(NumVoto);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DigitoVoto("3");
            SomTeclado();
            TesteVoto(NumVoto);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DigitoVoto("4");
            SomTeclado();
            TesteVoto(NumVoto);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DigitoVoto("5");
            SomTeclado();
            TesteVoto(NumVoto);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DigitoVoto("6");
            SomTeclado();
            TesteVoto(NumVoto);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DigitoVoto("7");
            SomTeclado();
            TesteVoto(NumVoto);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DigitoVoto("8");
            SomTeclado();
            TesteVoto(NumVoto);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DigitoVoto("9");
            SomTeclado();
            TesteVoto(NumVoto);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DigitoVoto("0");
            SomTeclado();
            TesteVoto(NumVoto);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //Ao teclar Corrige, verifica se está ocorrendo a animação de final de voto, se sim, não faz nada (Evita bugs visuais)
            SomTeclado();
            if (finalvoto == false)
            {
                NumVoto = "";
                NumeroCand.Text = NumVoto;
                NumeroCand2.Text = NumVoto;
                TesteVoto(NumVoto);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //Ao teclar Branco, verifica se está ocorrendo a animação de final de voto, se sim, não faz nada (Evita bugs visuais)
            SomTeclado();
            if (finalvoto == false)
            {
                    if (NumeroCand.Text == "")
                    {
                        NumVoto = "BRANCO";
                    }
                    TesteVoto(NumVoto);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //Registro dos votos
            if (finalvoto == false)
            {
                if (NumVoto != "BRANCO" && NumeroCand2.Text == "")
                {
                    MessageBox.Show("Para CONFIRMAR é necessário digitar o número do candidato ou votar em BRANCO.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (NumVoto == "10")
                    {
                        candidato1 += 1;
                    }
                    else if (NumVoto == "20")
                    {
                        candidato2 += 1;
                    }
                    else if (NumVoto == "BRANCO")
                    {
                        branco += 1;
                    }
                    else
                    {
                        nulo += 1;
                    }
                    //Chama função para finalizar a tela de votação, em seguida reinicia os parâmetros principais da tela e sistema caso a votação continue
                    FimVoto();
                }
            }
        }

        private void Urna_Load(object sender, EventArgs e)
        {
            //Quando carrega a urna, apaga o texto de informações dos candidatos e o voto problemático (fiz isso para ajudar na montagem do desing)
            NomeCand.Text = "";
            VotoBO.Text = "";
        }
    }
}
