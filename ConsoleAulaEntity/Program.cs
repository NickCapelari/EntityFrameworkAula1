// See https://aka.ms/new-console-template for more information


using ConsoleAulaEntity;
using ConsoleAulaEntity.Models;
using Microsoft.EntityFrameworkCore;
bool teste = true;
while (teste)
{
    Console.WriteLine(@"
    Informe 1 para criar uma pessoa,
    2 para alterar o nome da pessoa,
    3 para inserir um e-mail,
    4 para excluir a pessoa,
    5 para consultar as pessoas (todas),
    6 para consultar  pelo ID,
    7 para Sair");

    int op = int.Parse(Console.ReadLine());

    Contexto contexto = new Contexto();

    switch (op)
    {

        case 1:
            try
            {
                Pessoa p = new Pessoa();
                Console.WriteLine("Insira o nome da pessoa");
                p.Nome = Console.ReadLine();

                Console.WriteLine("Insira o e-mail da pessoa");
                Email email = new Email();
                email.email = Console.ReadLine();

                p.Emails = new List<Email>(); //inicia a lista

                p.Emails.Add(email); // adiciona o e-mail a lista

                contexto.Pessoas.Add(p);
                contexto.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            break;

        case 2:
            Console.WriteLine("Informe o ID da pessoa que vc deseja alterar");
            int idPessoa2 = int.Parse(Console.ReadLine());
            Pessoa pAlt = contexto.Pessoas.Find(idPessoa2);
            Console.WriteLine("Informe o novo nome");
            pAlt.Nome = Console.ReadLine();

            contexto.Pessoas.Update(pAlt);
            contexto.SaveChanges();
            break;

        case 3:
            Console.WriteLine("Informe o ID da pessoa");
            int idPess = int.Parse(Console.ReadLine());

            Pessoa pEmail = contexto.Pessoas.Find(idPess);

            Console.WriteLine("Informe o novo e-mail");
            Email emailNovo = new Email();
            emailNovo.email = Console.ReadLine();

            if (pEmail.Emails == null)
            {
                pEmail.Emails = new List<Email>();
            }

            pEmail.Emails.Add(emailNovo);

            contexto.Pessoas.Update(pEmail);
            contexto.SaveChanges();

            break;

        case 4:
            Console.WriteLine("Infome o Id da pessoa a ser deletada");

            int idExc = int.Parse(Console.ReadLine());
            Pessoa pExc = contexto.Pessoas.Find(idExc);

            Console.WriteLine("Confirma a exclusão da Pessoa: " + pExc.Nome);
            Console.WriteLine(" e dos e-mails: ");

            foreach (Email email in pExc.Emails)
            {
                Console.WriteLine("     " + email.email);
            }

            Console.WriteLine("1- SIM --------- 2 - NÂO ");
            int retorno = int.Parse(Console.ReadLine());

            if (retorno == 1)
            {
                contexto.Pessoas.Remove(pExc);
                contexto.SaveChanges();
            }

            break;

        case 5:
            List<Pessoa> pessoas = new List<Pessoa>();

            pessoas = (from Pessoa p in contexto.Pessoas
                       select p).Include(e => e.Emails).ToList<Pessoa>();
            foreach (Pessoa pessoaItem in pessoas)
            {
                Console.WriteLine(pessoaItem.Nome);
                Console.WriteLine(pessoaItem.Id);

                foreach (Email emailItem in pessoaItem.Emails)
                {
                    Console.WriteLine("---" + emailItem.email);
                }
                Console.WriteLine();
            }
            break;

        case 6:
            Console.WriteLine("Informe o ID da Pessoa");
            int idPessoa = int.Parse(Console.ReadLine());

            Pessoa pessoa = contexto.Pessoas.Include(pessoa => pessoa.Emails)
                .FirstOrDefault(pessoa => pessoa.Id == idPessoa);
            Console.WriteLine(pessoa.Nome);
            foreach (Email emailItem in pessoa.Emails)
            {
                Console.WriteLine("---" + emailItem.email);
            }
            Console.WriteLine();

            break;

        case 7:
            teste = false;
            break;

        default:
            break;
    }




}