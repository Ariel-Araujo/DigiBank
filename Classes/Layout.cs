using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DigiBank.Classes
{
    public class Layout
    {
        private static List<Pessoa> pessoas = new List<Pessoa>();
        private static int opcao = 0;
        public static void TelaPrincipal()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();

            Console.WriteLine("                                                               ");
            Console.WriteLine("              Digite a opção desejada  :                       ");
            Console.WriteLine("             ================================                  ");
            Console.WriteLine("              1 - Criar Conta                                  ");
            Console.WriteLine("             ================================                  ");
            Console.WriteLine("              2 - Entrar com CPF e Senha                       ");
            Console.WriteLine("             ================================                  ");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    TelaCriarConta();
                    break;
                case 2:
                    TelaLogin();
                    break;
                default:
                    Console.WriteLine("Opção Inválida");
                    break;
            }
        }

        private static void TelaCriarConta()
        {
            Console.Clear();

            Console.WriteLine("                                                               ");
            Console.WriteLine("              Digite seu nome:                                 ");
            string nome = Console.ReadLine();
            Console.WriteLine("             ================================                  ");
            Console.WriteLine("              Digite seu CPF:                                  ");
            string cpf = Console.ReadLine();
            Console.WriteLine("             ================================                  ");
            Console.WriteLine("              Digite sua Senha:                                ");
            string senha = Console.ReadLine();
            Console.WriteLine("             ================================                  ");

            Console.WriteLine(nome);
            Console.WriteLine(cpf);
            Console.WriteLine(senha);

            ContaCorrente contaCorrente = new ContaCorrente();
            Pessoa pessoa = new Pessoa();

            pessoa.SetNome(nome);
            pessoa.SetCPF(cpf);
            pessoa.SetSenha(senha);
            pessoa.Conta = contaCorrente;

            pessoas.Add(pessoa);

            Console.Clear();

            Console.WriteLine("               Conta cadastrada com sucesso!                   ");
            Console.WriteLine("             ================================                  ");

            Thread.Sleep(2000); 

            TelaContaLogada(pessoa);
        }

        private static void TelaLogin()
        {
            Console.Clear();

            Console.WriteLine("                                                               ");
            Console.WriteLine("              Digite o CPF:                                    ");
            string cpf = Console.ReadLine();
            Console.WriteLine("             ================================                  ");
            Console.WriteLine("              Digite sua senha:                                ");
            string senha = Console.ReadLine();
            Console.WriteLine("             ================================                  ");
            Console.WriteLine("                                                               ");

            Pessoa pessoa = pessoas.FirstOrDefault(x => x.CPF == cpf && x.Senha == senha);

            if(pessoa != null)
            {
                TelaBoasVindas(pessoa);
                TelaContaLogada(pessoa);
            }
            else
            {
                Console.Clear();

                Console.WriteLine("               Pessoa não cadastrada               ");
                Console.WriteLine("             ========================              ");

                Console.WriteLine("                                                   ");
                Console.WriteLine("                                                   ");    
            }
        }

        private static void TelaBoasVindas(Pessoa pessoa)
        {
            string msgTelaBemVindo =
                $"{pessoa.Nome} | Banco: {pessoa.Conta.GetCodigoDobanco()} " +
                $"| Agencia: {pessoa.Conta.GetNumeroAgencia()} | Conta: {pessoa.Conta.GetNumeroConta()}";
            Console.WriteLine("                                             ");
            Console.WriteLine($"     Seja bem vindo, {msgTelaBemVindo}      ");
            Console.WriteLine("                                             ");
        }

        private static void TelaContaLogada(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("             Digite a opção desejada:                 ");
            Console.WriteLine("            ==========================                ");
            Console.WriteLine("             1 - Realizar um Deposito                 ");
            Console.WriteLine("            ==========================                ");
            Console.WriteLine("             2 - Realizar um Saque                    ");
            Console.WriteLine("            ==========================                ");
            Console.WriteLine("             3 - Consultar Saldo                      ");
            Console.WriteLine("            ==========================                ");
            Console.WriteLine("             4 - Extrato                              ");
            Console.WriteLine("            ==========================                ");
            Console.WriteLine("             5 - Sair                                 ");
            Console.WriteLine("            ==========================                ");

            opcao = int.Parse(Console.ReadLine());
                
            switch(opcao)
            {
                case 1:
                    TelaDeposito(pessoa);
                    break;
                case 2:
                    TelaSaque(pessoa);
                    break;
                case 3:
                    TelaSaldo(pessoa);
                    break;
                case 4:
                    TelaExtrato(pessoa);
                    break;
                case 5:
                    TelaPrincipal();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("             Opção inválida!                          ");
                    Console.WriteLine("            ==========================                ");
                    break;
            }

        }

        private static void TelaDeposito(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("          Digite o valor do deposito:                       ");
            double valor = double.Parse(Console.ReadLine());
            Console.WriteLine("         =============================                      ");

            pessoa.Conta.Deposita(valor);

            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("                                                            ");
            Console.WriteLine("                                                            ");
            Console.WriteLine("              Deposito realizado com sucesso!               ");
            Console.WriteLine("             =================================              ");
            Console.WriteLine("                                                            ");
            Console.WriteLine("                                                            ");

            OpcaoVoltarLogado(pessoa);
        }

        private static void TelaSaque(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("          Digite o valor do Saque:                          ");
            double valor = double.Parse(Console.ReadLine());
            Console.WriteLine("         =============================                      ");

            bool saqueOk = pessoa.Conta.Saca(valor);

            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("                                                            ");
            Console.WriteLine("                                                            ");

            if (saqueOk)
            {
                Console.WriteLine("              Saque foi realizado com sucesso!          ");
                Console.WriteLine("             =================================          ");
            }
            else
            {
                Console.WriteLine("                     Saldo insuficiente!                ");
                Console.WriteLine("             =================================          ");
            }

            Console.WriteLine("                                                            ");
            Console.WriteLine("                                                            ");

            OpcaoVoltarLogado(pessoa);
        }

        private static void TelaSaldo(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa); 

            Console.WriteLine($"             Seu saldo é: {pessoa.Conta.ConsultaSaldo()}   ");
            Console.WriteLine("             =================================              ");
            Console.WriteLine("                                                            ");

            OpcaoVoltarLogado(pessoa);
        }

        private static void TelaExtrato(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);
               
            if(pessoa.Conta.Extrato().Any())
            {

                double total = pessoa.Conta.Extrato().Sum(x => x.Valor);

                foreach (Extrato extrato in pessoa.Conta.Extrato())
                {
                    Console.WriteLine("                                                              ");
                    Console.WriteLine($"        Data: {extrato.Data.ToString("dd/MM/yyyy HH:mm:ss")} ");
                    Console.WriteLine($"        Tipo de Movimentação: {extrato.Descricao}            ");
                    Console.WriteLine($"        Valor: {extrato.Valor}                               ");
                    Console.WriteLine("        =================================                     ");
                }


                Console.WriteLine("                                                            ");
                Console.WriteLine("                                                            ");
                Console.WriteLine($"                    SUB TOTAL: {total}                     ");
                Console.WriteLine("             =================================              ");

            }
            else
            {
                Console.WriteLine("                Não há extrato para exibir                  ");
                Console.WriteLine("             =================================              ");
            }

            OpcaoVoltarLogado(pessoa);
        }

        private static void OpcaoVoltarLogado(Pessoa pessoa)
        {
            Console.WriteLine("                Entre com uma opção abaixo!                 ");
            Console.WriteLine("             =================================              ");
            Console.WriteLine("                1 - Voltar para minha conta                 ");
            Console.WriteLine("             =================================              ");
            Console.WriteLine("                2 - Sair                                    ");
            Console.WriteLine("             =================================              ");

            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
                TelaContaLogada(pessoa);
            else
                TelaPrincipal(); 
        }

        private static void OpcaoVoltarDesLogado()
        {
            Console.WriteLine("                Entre com uma opção abaixo!                 ");
            Console.WriteLine("             ===================================            ");
            Console.WriteLine("                1 - Voltar para o Menu Principal            ");
            Console.WriteLine("             ===================================            ");
            Console.WriteLine("                2 - Sair                                    ");
            Console.WriteLine("             ===================================            ");

            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
                TelaPrincipal();
            else
            {
                Console.WriteLine("                        Opção Inválida                  ");
                Console.WriteLine("             ===================================        ");
            }
        }



    }
}











