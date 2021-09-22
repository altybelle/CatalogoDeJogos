using DIO.CatalogoDeJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIO.CatalogoDeJogos.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>(){

            { Guid.Parse("ab8b428f-d574-4d44-bb1d-37f99579906e"), new Jogo{ Id = Guid.Parse("ab8b428f-d574-4d44-bb1d-37f99579906e"), Nome = "God Of War 4", Produtora = "Sony", Preco = 199.99 }},
            { Guid.Parse("bbc04bb6-8272-4451-8b33-45afef7f35ea"), new Jogo{ Id = Guid.Parse("bbc04bb6-8272-4451-8b33-45afef7f35ea"), Nome = "Guilty Gear -Strive-", Produtora = "Arc System Works", Preco = 129.99 }},
            { Guid.Parse("a193aef3-f23d-4abc-9987-8535aa4c7c97"), new Jogo{ Id = Guid.Parse("a193aef3-f23d-4abc-9987-8535aa4c7c97"), Nome = "The Legend of Zelda - Breath of the Wild", Produtora = "Nintendo", Preco = 378.00 }},
            { Guid.Parse("22c57aa6-2bba-4972-9ccb-366b9c8fefea"), new Jogo{ Id = Guid.Parse("22c57aa6-2bba-4972-9ccb-366b9c8fefea"), Nome = "Hollow Knight", Produtora = "Team Cherry", Preco = 234.99 }},
            { Guid.Parse("f3bacc05-46ac-401a-aca7-160778cbd36e"), new Jogo{ Id = Guid.Parse("f3bacc05-46ac-401a-aca7-160778cbd36e"), Nome = "Dark Souls III", Produtora = "FromSoftware", Preco = 159.90 }},
            { Guid.Parse("12a33080-101a-44bf-97d4-0a8c7215af4f"), new Jogo{ Id = Guid.Parse("12a33080-101a-44bf-97d4-0a8c7215af4f"), Nome = "The Witcher III: Wild Hunt", Produtora = "CD PROJEKT RED", Preco = 79.99 }}
        };

        public Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Jogo> Obter(Guid id)
        {
            if (!jogos.ContainsKey(id))
                return null;

            return Task.FromResult(jogos[id]);
        }

        public Task<List<Jogo>> Obter(string nome, string produtora)
        {
            return Task.FromResult(jogos.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());
        }
      
        public Task Inserir(Jogo jogo)
        {
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }

        public Task Atualizar(Jogo jogo)
        {
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }


        public Task Remover(Guid id)
        {
            jogos.Remove(id);
            return Task.CompletedTask;
        }
        public void Dispose()
        {
        }
    }
}
