using System;

namespace MariFlix.Series
{
    class Program
    {

        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Obrigado por utilizar nosso aplicativo. Volte Sempre -MariFlix");
            Console.ReadLine();
        }

        private static void ListarSeries()
        {
            Console.WriteLine("Listar séries");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluido();

                Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluido*" : ""));
            }
        }

        private static object[] EnumeraGenerosEPerguntaTituloAnoEDescricao()
        {
            object[] dadosEntradaDoUsuario = new object[4];

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }

            Console.Write("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());
            dadosEntradaDoUsuario[0] = entradaGenero;

            Console.Write("Digite o título da série: ");
            string entradaTitulo = Console.ReadLine();
            dadosEntradaDoUsuario[1] = entradaTitulo;

            Console.Write("Digite o ano de início da série: ");
            int entradaAno = int.Parse(Console.ReadLine());
            dadosEntradaDoUsuario[2] = entradaAno;

            Console.Write("Digite a descrição da série: ");
            string entradaDescricao = Console.ReadLine();
            dadosEntradaDoUsuario[3] = entradaDescricao;

            return dadosEntradaDoUsuario;
        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");

            var entradaUsuario = EnumeraGenerosEPerguntaTituloAnoEDescricao();

            Serie novaSerie = new Serie(
                id: repositorio.ProximoId(),
                genero: (Genero)entradaUsuario[0],
                titulo: Convert.ToString(entradaUsuario[1]),
                ano: Convert.ToInt16(entradaUsuario[2]),
                descricao: Convert.ToString(entradaUsuario[3])
            );

            repositorio.Insere(novaSerie);
        }

        private static void AtualizarSerie()
        {
            Console.Write("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            var entradaUsuario = EnumeraGenerosEPerguntaTituloAnoEDescricao();

            Serie atualizaSerie = new Serie(
                id: indiceSerie,
                genero: (Genero)entradaUsuario[0],
                titulo: Convert.ToString(entradaUsuario[1]),
                ano: Convert.ToInt16(entradaUsuario[2]),
                descricao: Convert.ToString(entradaUsuario[3])
            );

            repositorio.Atualiza(indiceSerie, atualizaSerie);
        }

        public static void ExcluirSerie()
        {
            Console.Write("Digite o id da série que deseja excluir: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            repositorio.Exclui(indiceSerie);
        }

        private static void VisualizarSerie()
        {
            Console.Write("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(indiceSerie);

            Console.WriteLine(serie);
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("TumDum~~ Bem Vindo a MariFlix...");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1- Listar séries");
            Console.WriteLine("2- Inserir nova série");
            Console.WriteLine("3- Atualizar série");
            Console.WriteLine("4- Excluir série");
            Console.WriteLine("5- Visualizar série");
            Console.WriteLine("C- Limpar tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}
