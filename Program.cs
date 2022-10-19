using System.Text;
using DesafioProjetoHospedagem.Models;

Console.OutputEncoding = Encoding.UTF8;

// Cria os modelos de hóspedes e cadastra na lista de hóspedes
List<Pessoa> hospedes = new List<Pessoa>();

Console.WriteLine("Seja Bem vindo(a)");
int qtHospedes = 0;
bool confReserva = false;
do
{
    do
    {
        try
        {
            Console.WriteLine("quarto para quantas pessoas ?");
            qtHospedes = int.Parse(Console.ReadLine());
            Console.Clear();
        }
        catch (Exception)
        {
            Console.Clear();
            Console.WriteLine("Valor invalido, digite apenas numeros (Pressione qualquer tecla para tentar novamente)");
            Console.ReadKey();
            Console.Clear();
        }

        if (qtHospedes > 4)
        {
            Console.WriteLine("Não temos suites para essa quantidade de hospedes (Pressione qualquer tecla para tentar novamente)");
            Console.ReadKey();
            qtHospedes = 0;
            Console.Clear();
        }

    } while (qtHospedes <= 0);

    bool confHospedes = false;
    do
    {
        for (int i = 1; i <= qtHospedes; i++)
        {
            Console.Clear();
            Console.WriteLine($"Nome Completo {i}º hospede: ");
            string nomeHospede = Console.ReadLine();
            Pessoa hospede = new Pessoa(nomecompleto: nomeHospede.ToUpper());
            hospedes.Add(hospede);
        }

        int conf = 0;
        do
        {
            Console.Clear();
            Console.WriteLine("Os hospedes estão corretos ?");
            foreach (var item in hospedes)
            {
                Console.WriteLine("--- " + item.NomeCompleto);
            }
            Console.WriteLine("1 - SIM");
            Console.WriteLine("2 - NÃO");
            Console.WriteLine("Digite apenas numeros");
            try
            {
                conf = int.Parse(Console.ReadLine());
                switch (conf)
                {
                    case 1:
                        confHospedes = true;
                        break;

                    case 2:
                        hospedes.Clear();
                        confHospedes = false;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção digitada invalida, digite novamente ( pressione qualquer tecla para continuar )");
                        Console.ReadKey();
                        break;
                }
                Console.Clear();
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("opção invalida, apenas 1 e 2 é valido (pressione qualquer tecla para continuar)");
                Console.ReadKey();
            }

        } while (conf != 1 && conf != 2);
    } while (confHospedes == false);

    Console.Clear();
    // Cria a suíte
    (int Option, string nomeSuite, int capacidadeSuite, decimal valorDiariaSuite) suiteDados = (0, "", 0, 0);
    bool confSuite = false;
    do
    {
        do
        {
            Console.Clear();
            Console.WriteLine("Escolha sua suite (Digite apenas numeros)");
            Console.WriteLine("1 -- Premium ( Capacidade: 2 Pessoas )");
            Console.WriteLine("2 -- Premium Grande ( Capacidade: 4 Pessoas )");
            Console.WriteLine("3 -- Luxo ( Capacidade: 2 Pessoas )");
            Console.WriteLine("4 -- Luxo Grande ( Capacidade: 4 Pessoas )");
            Console.WriteLine("5 -- Tradicional ( Capacidade: 2 Pessoas )");

            try
            {
                int escolhasuite = int.Parse(Console.ReadLine());
                switch (escolhasuite)
                {
                    case 1:
                        suiteDados = (1, "Premium", 2, 185);
                        break;

                    case 2:
                        suiteDados = (1, "Premium Grande", 4, 255);
                        break;

                    case 3:
                        suiteDados = (1, "Luxo", 2, 135);
                        break;

                    case 4:
                        suiteDados = (1, "Luxo Grande", 4, 195);
                        break;

                    case 5:
                        suiteDados = (1, "Tradicional", 2, 80);
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção invalida, escolha novamente (pressione qualquer tecla para continuar)");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
                Console.Clear();
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Opção invalida, escolha novamente (pressione qualquer tecla para continuar)");
                Console.ReadKey();
                Console.Clear();

            }

        } while (suiteDados.Option == 0);


        Suite suite = new Suite(tipoSuite: suiteDados.nomeSuite, capacidade: suiteDados.capacidadeSuite, valorDiaria: suiteDados.valorDiariaSuite);
        // Cria uma nova reserva, passando a suíte e os hóspedes
        Console.Clear();
        Reserva cadastro = new Reserva();
        cadastro.CadastrarSuite(suite);
        int reservaHospedes = cadastro.CadastrarHospedes(hospedes);

        // Exibe a quantidade de hóspedes e o valor da diária
        if (reservaHospedes == 0)
        {
            Console.ReadKey();
        }
        else
        {
            int diasReserva = 0;
            do
            {
                try
                {
                    Console.WriteLine("Quantos dias deseja ficar ?");
                    diasReserva = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("digite apenas numeros, escolha novamente (pressione qualquer tecla para continuar)");
                    Console.ReadKey();
                    Console.Clear();
                    diasReserva = 0;
                }

            } while (diasReserva == 0);

            cadastro = new Reserva(diasReservados: diasReserva);
            Console.Clear();
            Console.WriteLine($"Hóspedes: {cadastro.ObterQuantidadeHospedes(hospedes)}");
            Console.WriteLine("Nomes");
            foreach (var item in hospedes)
            {
                Console.WriteLine("-- "+item.NomeCompleto);
            }
            Console.WriteLine("Suite escolhida: " + suiteDados.nomeSuite);
            Console.WriteLine($"Valor total de {diasReserva} dias: {cadastro.CalcularValorDiaria(suite)}");
            confSuite = true;
        }
    } while (confSuite == false);


    int finalReserva = 0;
    do
    {
        Console.WriteLine("O que deseja fazer ?");
        Console.WriteLine("1 -- Confirmar reserva");
        Console.WriteLine("2 -- Recomeçar cadastro");
        try
        {
            finalReserva = int.Parse(Console.ReadLine());
            switch (finalReserva)
            {
                case 1:
                    confReserva = true;
                    break;

                case 2:
                    confReserva = false;
                    break;

                default:
                    finalReserva = 0;
                    Console.Clear();
                    Console.WriteLine("Opção invalida (pressione qualquer tecla para continuar)");
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
        }
        catch (Exception)
        {
            finalReserva = 0;
            Console.Clear();
            Console.WriteLine("Digite apenas numeros (pressione qualquer tecla para continuar)");
            Console.ReadKey();
            Console.Clear();
        }
    } while (finalReserva == 0);

    Console.Clear();
} while (confReserva == false);

Console.Clear();
Console.WriteLine("Reserva concluida com sucesso");
