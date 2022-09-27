namespace CadastroDeClientes
{
    internal class Program
    {
        static Dictionary<int, string> _cadastro = new Dictionary<int, string>();
        static string _fileName = @"C:\Users\aleaz\OneDrive\Documentos\cadastro.txt";
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
                    InformarRendaMedia();
                    break;
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
            Cabecalho("Alterar cliente");
            Console.Write("Insira o codigo do cliente que você deseja alterar:");
            int codigo = int.Parse(Console.ReadLine());
            bool alterou = false;

            // Busca registro no dicionario, exclui registro atual e insere novo com os dados informados
            foreach (KeyValuePair<int, string> linha in _cadastro)
            {
                if (linha.Key == codigo)

                {
                    string[] campos = linha.Value.Split(";");
                    Console.WriteLine();

                    Console.WriteLine("Dados antigos salvos: ");
                    Console.Write("Nome..........: ");
                    Console.WriteLine(campos[1]);
                    Console.Write("Celular.......: ");
                    Console.WriteLine(campos[2]);
                    Console.Write("e-mail........: ");
                    Console.WriteLine(campos[3]);
                    Console.Write("Dta Nascimento: ");
                    Console.WriteLine(campos[4]);
                    Console.Write("Renda Anual...: ");
                    Console.WriteLine(campos[5]);
                    Console.Write("Ativo.........: ");
                    Console.WriteLine(campos[6]);

                    Console.WriteLine();
                    Console.WriteLine();


                    Console.WriteLine("Insira os novos dados a serem cadastrados abaixo:");
                    _cadastro.Remove(codigo);
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

                    string linhaCadastro = $"{codigo};{nome};{celular};{email};{dtaNascimento};{rendaAnual};{ativo}";
                    _cadastro.Add(codigo, linhaCadastro);

                    alterou = true;
                    break;
                }
            }

            if (alterou)
            {
                // Recria o arquivo com o dicionario atualizado
                File.Delete(_fileName);
                RecarregarArquivo();
            }
            else
            {
                Console.WriteLine("Não existe cliente com este código.");
                Console.ReadKey();
            }
        }
        static void RecarregarArquivo()
        {
            using (StreamWriter outputFile = new StreamWriter(_fileName))
            {
                foreach (KeyValuePair<int, string> linha in _cadastro)
                {
                    string[] campos = linha.Value.Split(";");
                    string linhaCadastro = String.Join(";", campos);
                    outputFile.WriteLine(linhaCadastro);
                }
            }
        }
        static float InformarRendaMedia()
        {
            int rendaCount = 0;
            float rendaMedia = 0, somaRenda = 0;
            foreach (KeyValuePair<int, string> clientes in _cadastro)
            {
                string[] vetor = clientes.Value.Split(";");
                somaRenda += float.Parse(vetor[5]);
                rendaCount++;
            }
            rendaMedia = somaRenda / rendaCount;
            Cabecalho("Media da Renda Anual dos Clientes");
            Console.WriteLine($"A media da renda atual é de R${rendaMedia}");
            Console.ReadKey();
            return 0;
        }


        static void ExcluirCliente()
        {
            Cabecalho("Excluir cliente");
            Console.Write("Insira o codigo do cliente que você deseja excluir:");
            int codigo = int.Parse(Console.ReadLine());
            bool excluiu = false;

            // Busca e remove registro do dicionário
            foreach (KeyValuePair<int, string> linha in _cadastro)
            {
                if (linha.Key == codigo)
                {
                    _cadastro.Remove(codigo);
                    excluiu = true;
                    break;
                }
            }

            if (excluiu)
            {
                // Recria o arquivo com o dicionario atualizado
                File.Delete(_fileName);
                RecarregarArquivo();
            }
            else
            {
                Console.WriteLine("Não existe cliente com este código.");
                Console.ReadKey();
            }

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

        static string ConsultarClienteNome()
        {
            string nomeCliente, msg;

            Cabecalho("Consultar cliente por Nome");
            Console.WriteLine("Digite o nome a ser consultado:");
            Console.WriteLine("================================");
            nomeCliente = Console.ReadLine();
            msg = "Cliente não encontrado.";

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
                    return vetor[1];
                }

            }
            Cabecalho(msg);
            Console.ReadKey();
            return msg;
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
            if (File.Exists(_fileName))
            {
                foreach (string line in File.ReadLines(_fileName))
                {
                    if (line != "")
                    {
                        string[] campos = line.Split(";");
                        _cadastro.Add(int.Parse(campos[0]), line);
                    }
                }
            }
            else
            {
                using (StreamWriter outputFile = File.CreateText(_fileName))
                {

                }
            }
        }

        static void InformarAniversarios()
        {
            string hoje = DateTime.Now.ToShortDateString();
            Cabecalho(" Aniversariantes do dia de hoje:" + hoje);
            Console.WriteLine("================================");
            Console.WriteLine("Codigo\t\tNome");
            Console.WriteLine("================================");
            foreach (KeyValuePair<int, string> linha in _cadastro)
            {
                string[] vetor = linha.Value.Split(";");

                if (hoje.Remove(5) == vetor[4].Remove(5))
                {
                    Console.WriteLine(vetor[0] + "   \t\t" + vetor[1]);
                }
            }
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
