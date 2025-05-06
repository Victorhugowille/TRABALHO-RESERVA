using System;

namespace TRABALHO2.Models
{
    class Program
    {
        static void Main()
        {
            // Coleta de dados para configuração
            Console.WriteLine("Digite a data mínima (dd/MM/yyyy):");
            DateTime dataMinima = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

            Console.WriteLine("Digite a data máxima (dd/MM/yyyy):");
            DateTime dataMaxima = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

            Console.WriteLine("Digite a hora mínima (HH:mm):");
            TimeSpan horaMinima = TimeSpan.Parse(Console.ReadLine());

            Console.WriteLine("Digite a hora máxima (HH:mm):");
            TimeSpan horaMaxima = TimeSpan.Parse(Console.ReadLine());

            // Criando a configuração de reserva
            ConfiguracaoReserva config = new ConfiguracaoReserva(dataMinima, dataMaxima, horaMinima, horaMaxima);

            // Coleta de dados para a reserva
            Console.WriteLine("Digite a data da reserva (dd/MM/yyyy):");
            DateTime dataReserva = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

            Console.WriteLine("Digite a hora da reserva (HH:mm):");
            TimeSpan horaReserva = TimeSpan.Parse(Console.ReadLine());

            Console.WriteLine("Digite a descrição da sala:");
            string descricaoSala = Console.ReadLine();

            Console.WriteLine("Digite a capacidade da sala:");
            int capacidade = int.Parse(Console.ReadLine());

            // Criando uma nova reserva
            Reserva reserva = new Reserva(dataReserva, horaReserva, descricaoSala, capacidade);

            // Validando e exibindo a reserva
            if (reserva.ValidarReserva(config))
            {
                reserva.ExibirReserva();
            }
            else
            {
                Console.WriteLine("A reserva não foi validada.");
            }
        }
    }

    public class Reserva
    {
        public DateTime DataReserva { get; private set; }
        public TimeSpan HoraReserva { get; private set; }
        public string DescricaoSala { get; private set; }
        public int Capacidade { get; private set; }

        public Reserva(DateTime dataReserva, TimeSpan horaReserva, string descricaoSala, int capacidade)
        {
            DataReserva = dataReserva;
            HoraReserva = horaReserva;
            DescricaoSala = descricaoSala;
            Capacidade = capacidade;

            // Validação da capacidade
            if (capacidade <= 0 || capacidade >= 40)
                throw new ArgumentException("A capacidade deve ser maior que 0 e menor que 40 alunos.");
        }

        public bool ValidarReserva(ConfiguracaoReserva config)
        {
            // Verificando se a data está dentro do intervalo permitido
            if (DataReserva < config.DataMinima || DataReserva > config.DataMaxima)
            {
                Console.WriteLine("Erro: A data da reserva está fora do intervalo permitido.");
                return false;
            }

            // Verificando se a hora está dentro do intervalo permitido
            if (HoraReserva < config.HoraMinima || HoraReserva > config.HoraMaxima)
            {
                Console.WriteLine("Erro: A hora da reserva está fora do intervalo permitido.");
                return false;
            }

            return true;
        }

        public void ExibirReserva()
        {
            Console.WriteLine("\n--- Dados da Reserva ---");
            Console.WriteLine($"Sala: {DescricaoSala}");
            Console.WriteLine($"Data: {DataReserva:dd/MM/yyyy}");
            Console.WriteLine($"Hora: {HoraReserva:hh\\:mm}");
            Console.WriteLine($"Capacidade: {Capacidade} alunos");
        }
    }

    public class ConfiguracaoReserva
    {
        public DateTime DataMinima { get; private set; }
        public DateTime DataMaxima { get; private set; }
        public TimeSpan HoraMinima { get; private set; }
        public TimeSpan HoraMaxima { get; private set; }

        public ConfiguracaoReserva(DateTime dataMinima, DateTime dataMaxima, TimeSpan horaMinima, TimeSpan horaMaxima)
        {
            // Validações
            if (dataMinima >= dataMaxima)
                throw new ArgumentException("A data mínima deve ser menor que a data máxima.");

            if (horaMinima >= horaMaxima)
                throw new ArgumentException("A hora mínima deve ser menor que a hora máxima.");

            // Atribuindo valores
            DataMinima = dataMinima;
            DataMaxima = dataMaxima;
            HoraMinima = horaMinima;
            HoraMaxima = horaMaxima;
        }
    }
}