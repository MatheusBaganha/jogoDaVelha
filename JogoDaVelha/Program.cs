using System;
using System.Text;

namespace JogoDaVelha
{
    class Program
    {
        static string[,] matriz = new string[3, 3];
        static string jogadorDoTurno = "X";
        static int rodadaAtual = 1;
        static List<string> todasPosicoes = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        static int indexDePreenchimento = 1;

        static string continuar = "S";
        static void CriarTabuleiro()
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    matriz[i, j] = indexDePreenchimento.ToString();
                    Console.Write($" [{matriz[i, j]}] ");
                    indexDePreenchimento++;

                }
                Console.WriteLine();
            }
        }
        static string VerifyWhoWillPlayNow(string turno)
        {
            if (turno == "X")
            {
                return "O";
            }
            else
            {
                return "X";
            }
        }

        static bool PlayerWonHorizontally()
        {
          if(matriz[0, 0] == matriz[0, 1] && matriz[0, 1] == matriz[0, 2] || matriz[1, 0] == matriz[1, 1] && matriz[1, 1] == matriz[1, 2] || matriz[2, 0] == matriz[2, 1] && matriz[2, 1] == matriz[2, 2])
            {
                return true;
            } else
            {
                return false;
            }
        }

        static bool PlayerWonVertically()
        {
            if(matriz[0, 0] == matriz[1, 0] && matriz[1, 0] == matriz[2, 0] || matriz[0, 1] == matriz[1, 1] && matriz[1, 1] == matriz[2, 1] || matriz[0, 2] == matriz[1, 2] && matriz[1, 2] == matriz[2, 2])
            {
                return true;
            } else
            {
                return false;
            }
        }

        static bool PlayerWonDiagonal()
        {
            if (matriz[0, 0] == matriz[1, 1] && matriz[1, 1] == matriz[2, 2] || matriz[0, 2] == matriz[1, 1] && matriz[1, 1] == matriz[2, 0])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void FinalMessage(string jogadorDoTurnoAtual)
        {
            Console.WriteLine("\nFim de Jogo!");
            Console.WriteLine($"O jogador do {jogadorDoTurnoAtual} ganhou!");

            while (continuar != "N" || continuar != "S")
            {
                Console.WriteLine("Deseja continuar jogando?\n S - Sim\n N- Não");
                continuar = Console.ReadLine().ToUpper();
                if(continuar == "N")
                {
                    Environment.Exit(0);
                }
                if(continuar == "S")
                {
                    jogadorDoTurno = "X";
                    rodadaAtual = 1;
                    todasPosicoes.Clear();
                    todasPosicoes = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                    indexDePreenchimento = 1;
                    Console.Clear();
                    break;
                }
            }
           
        }

        static void HandleChangeInMatriz(string posicaoDaJogada)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j] == posicaoDaJogada && todasPosicoes.Contains(posicaoDaJogada))
                    {
                        matriz[i, j] = jogadorDoTurno;
                        todasPosicoes.Remove(posicaoDaJogada);
                    }
                    Console.Write($" [{matriz[i, j]}] ");
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            while(continuar == "S")
            {
                Console.WriteLine("--- JOGO DA VELHA ---");
                CriarTabuleiro();

                Console.WriteLine($"Turno do jogador {jogadorDoTurno}. \nDigite a posição que deseja jogar: ");
                string posicaoDaJogada = Console.ReadLine();
                
                while (rodadaAtual < 9)
                {
                    Console.Clear();
                    HandleChangeInMatriz(posicaoDaJogada);



                    if (PlayerWonDiagonal())
                    {
                        FinalMessage(jogadorDoTurno);
                        break;
                    }

                    if (PlayerWonHorizontally()) {
                        FinalMessage(jogadorDoTurno);
                        break;
                    }

                    if (PlayerWonVertically())
                    {
                        FinalMessage(jogadorDoTurno);
                        break;
                    }

                    jogadorDoTurno = VerifyWhoWillPlayNow(jogadorDoTurno);
                    Console.WriteLine($"Turno do jogador {jogadorDoTurno}. \nDigite a posição que deseja jogar: ");

                    posicaoDaJogada = Console.ReadLine();


                    while (!todasPosicoes.Contains(posicaoDaJogada))
                    {
                        Console.WriteLine("Essa posição é inválida. Tente novamente: ");
                        posicaoDaJogada = Console.ReadLine();
                    }

                    rodadaAtual++;
                    Console.Clear();
                }

            if(rodadaAtual == 9)
            {
                CriarTabuleiro();
                Console.WriteLine("Fim de Jogo.\nO resultado foi um empate!");
            }
         }
        }
    }
}

