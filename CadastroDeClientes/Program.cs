namespace CadastroDeClientes
{
    internal class Program
    {
        static Dictionary<int, string> _cadastro = new Dictionary<int, string>();
        static string _fileName = @"c:\cadastro.txt";
        static void Main(string[] args)
        {
            int opcao = 0;
            LerArquivo();
            while (opcao != 10)
            {
                Cabecalho("Menu Principal");
                Menu();
                Console.Write("Digite um opção: ");
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Opção Inválida, digite novamente!");
                    Console.ReadKey();
                }
                SelecionarOpcaoDoMenu(opcao);
            }
        }

        /// <summary>
        /// Mostrar o Cabelho do Menu principal ou da opção selecionada
        /// </summary>
        /// <param name="titulo">Titulo atual da ação</param>
        static void Cabecalho(string titulo)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("==================================================");
            Console.WriteLine("= " + titulo);
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        /// <summary>
        /// Mostra as opções do Menu
        /// </summary>
        static void Menu()
        {
            Console.WriteLine(" 1 - Cadastrar Cliente");
            Console.WriteLine(" 2 - Alterar dados de um cliente");
            Console.WriteLine(" 3 - Excluir dados de um cliente");
            Console.WriteLine(" 4 - Listar todos os clientes");
            Console.WriteLine(" 5 - Consultar todos os clientes ativos");
            Console.WriteLine(" 6 - Informar a média da renda anual dos clientes ativos");
            Console.WriteLine(" 7 - Informar os aniversariantes do dia");
            Console.WriteLine(" 8 - Pesquisar um cliente pelo código");
            Console.WriteLine(" 9 - Pesquisar um cliente pelo nome");
            Console.WriteLine(" 10 - Sair");
            Console.WriteLine();
        }
        /// <summary>
        /// Chama a função escolhida pelo usuário
        /// </summary>
        /// <param name="opcao">Opção digitada pelo usuário</param>
        static void SelecionarOpcaoDoMenu(int opcao)
        {
            switch (opcao)
            {
                case 1:
                    CriarNovoCliente();
                    break;
                case 2:
                    AlterarCliente();
                    break;
                case 3:
                    ExcluirCliente();
                    break;
                case 4:
                    ConsultarTodosClientes();
                    break;
                case 5:
                    ConsultarTodosClientesAtivos();
                    break;
                case 6:
                //InformarRendaMedia();
                case 7:
                    InformarAniversarios();
                    break;
                case 8:
                    ConsultarClienteCodigo();
                    break;
                case 9:
                    ConsultarClienteNome();
                    break;
                case 10:
                    break;
                default:
                    Console.WriteLine("Opção fora do intervalor de 1 até 10, por favor, digite novamente!!!");
                    Console.ReadKey();
                    break;
            }
        }

        static void CriarNovoCliente()
        {
            Cabecalho("Inserir um novo cliente");
            //Console.Write("Código........: ");
            //int codigo = int.Parse(Console.ReadLine());
            Console.Write("Nome..........: ");
            string nome = Console.ReadLine();
            Console.Write("Celular.......: ");
            string celular = Console.ReadLine();
            Console.Write("e-mail........: ");
            string email = Console.ReadLine();
            Console.Write("Dta Nascimento: ");
            string dtaNascimento = Console.ReadLine();
            Console.Write("Renda Anual...: ");
            float rendaAnual = float.Parse(Console.ReadLine());
            Console.Write("Ativo.........: ");
            int ativo = int.Parse(Console.ReadLine());
            //-----------------------------------------
            // Obter um codigo disponivel na lista
            //-----------------------------------------
            int codigo = ObterNovoCodigoCliente();
            //-----------------------------------------
            string linhaCadastro = $"{codigo};{nome};{celular};{email};{dtaNascimento};{rendaAnual};{ativo}";
            _cadastro.Add(codigo, linhaCadastro);
            GravarDadosArquivo(linhaCadastro);
        }

        static void AlterarCliente()
        {


        }

        static void ExcluirCliente()
        {

            Cabecalho("Excluir cliente");
            Console.Write("Insira o codigo do cliente que você deseja excluir:");
            //Pega código do cliente a ser excluido
            int codigoExcluir = int.Parse(Console.ReadLine());
            //Auxiliar para sobescrever o arquivo
            int primeiraLinha = 0;
            foreach (string line in System.IO.File.ReadAllLines(_fileName))
            {
                string[] campos = line.Split(";");
                //Checa se o cliente atual é o cliente a ser excluído
                if (codigoExcluir == int.Parse(campos[0]))
                {
                    if (int.Parse(campos[0]) == 0)
                    {
                        //Só não está excluindo se for o ultimo cliente porque nao sei o que colocar aqui para criar o arquivo do 0.
                    }
                }
                else
                {
                    //Checa se é a primeira linha e caso seja, sobescreve totalmente o arquivo, para seguir com a escrita normal a partir do proximo passo.
                    if (primeiraLinha == 0)
                    {
                        using (StreamWriter outputFile = new StreamWriter(_fileName, false))
                        {
                            outputFile.WriteLine(line);
                        }
                        ++primeiraLinha;
                    }
                    else
                    //gravação das linhas que não sejam a primeira (sem sobescrever)
                    {
                        GravarDadosArquivo(line);
                    }


                }
            }
            //Re-lê os clientes cadastrados para ficar com a base de dados atualizada após exclusão
            LerArquivo();

        }
        static void ConsultarTodosClientes()
        {
            Cabecalho("Consultar todos os clientes");
            Console.WriteLine("Codigo\t\tNome");
            Console.WriteLine("================================");
            foreach (KeyValuePair<int, string> linha in _cadastro)
            {
                string[] vetor = linha.Value.Split(";");
                Console.WriteLine("{0}\t\t{1}", linha.Key, vetor[1]);
            }
            Console.ReadKey();
        }
        static int ConsultarClienteCodigo()
        {
            int codigoCliente;

            Cabecalho("Consultar cliente por código");
            Console.WriteLine("Digite o código a ser consultado:");
            Console.WriteLine("================================");
            codigoCliente = Convert.ToInt32(Console.ReadLine());

            foreach (KeyValuePair<int, string> linha in _cadastro)
            {
                if (codigoCliente == linha.Key)
                {
                    Console.WriteLine($"Cliente com o código {linha.Key} encontrado");
                    string[] vetor = linha.Value.Split(";");
                    Console.Clear();
                    Cabecalho("Cliente encontrado");
                    Console.WriteLine("================================");
                    Console.WriteLine("{0}\t\t{1}", linha.Key, vetor[1]);
                    Console.WriteLine("================================");
                    Console.ReadKey();
                    return linha.Key;
                }
            }
            Console.WriteLine($"Cliente com o código {codigoCliente} não encontrado.");
            Console.ReadKey();
            return 0;
        }

        static void ConsultarClienteNome()
        {
            string nomeCliente;

            Cabecalho("Consultar cliente por Nome");
            Console.WriteLine("Digite o nome a ser consultado:");
            Console.WriteLine("================================");
            nomeCliente = Console.ReadLine();

            foreach (KeyValuePair<int, string> linha in _cadastro)
            {
                string[] vetor = linha.Value.Split(";");

                if (nomeCliente == vetor[1])
                {
                    Console.WriteLine($"Cliente com o nome {vetor[1]} encontrado");

                    Console.Clear();
                    Cabecalho("Cliente encontrado");
                    Console.WriteLine("================================");
                    Console.WriteLine("{0}\t\t{1}", linha.Key, vetor[1]);
                    Console.WriteLine("================================");
                    Console.ReadKey();

                }
                else
                {
                    Console.WriteLine("Cliente com não encontrado.");
                }
            }
            Console.ReadKey();

        }

        static void ConsultarTodosClientesAtivos()
        {
            Dictionary<int, string> clientesAtivos = new Dictionary<int, string>();
            Cabecalho("Clientes ativos");
            Console.WriteLine("================================");
            Console.WriteLine("Codigo\t\tNome");
            Console.WriteLine("================================");

            foreach (KeyValuePair<int, string> linha in _cadastro)
            {
                string[] vetor = linha.Value.Split(";");

                if (1 == int.Parse(vetor[6]))
                {
                    Console.WriteLine(vetor[0] + "   \t\t" + vetor[1]);
                }
            }
            Console.ReadKey();

        }

        static void GravarDadosArquivo(string linhaCadastro)
        {
            using (StreamWriter outputFile = new StreamWriter(_fileName, true))
            {
                outputFile.WriteLine(linhaCadastro);
            }
        }

        static void LerArquivo()
        {
            _cadastro.Clear();
            foreach (string line in System.IO.File.ReadLines(_fileName))
            {
                string[] campos = line.Split(";");
                _cadastro.Add(int.Parse(campos[0]), line);
            }

        }

        static void InformarAniversarios()
        {
            string hoje = DateTime.Now.ToShortDateString();
            Cabecalho(" Aniversariantes do dia de hoje:" + hoje);

            Console.ReadKey();
        }

        static int ObterNovoCodigoCliente()
        {
            int codigo = 0;
            int codigoDB;
            foreach (KeyValuePair<int, string> linha in _cadastro)
            {
                codigoDB = linha.Key;
                if (codigoDB > codigo)
                    codigo = codigoDB;
            }
            return ++codigo;
        }
    }
}