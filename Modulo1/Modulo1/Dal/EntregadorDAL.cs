using Modulo1.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulo1.Dal
{
    public class EntregadorDAL
    {
        private ObservableCollection<Entregador> Entregadores = new ObservableCollection<Entregador>(); // permite que os controles que fazem uso da coleção, sejam atualizados
        private static EntregadorDAL EntregadorInstance = new EntregadorDAL();

        // garante que a classe não terá um construtor para ser chamado de fora
        private EntregadorDAL()
        {
            Entregadores.Add(new Entregador() { Id = 1, Nome = "Brauzio", Telefone = "99917.8133" });
            Entregadores.Add(new Entregador() { Id = 2, Nome = "Entencius", Telefone = "99914.8133" });
            Entregadores.Add(new Entregador() { Id = 3, Nome = "Cartucious", Telefone = "99917.8999" });
            Entregadores.Add(new Entregador() { Id = 4, Nome = "Adoliterio", Telefone = "99918.9933" });
            Entregadores.Add(new Entregador() { Id = 5, Nome = "Castrogildo", Telefone = "99977.7733" });
            Entregadores.Add(new Entregador() { Id = 6, Nome = "Asdrugio", Telefone = "99717.7777" });
            Entregadores.Add(new Entregador() { Id = 7, Nome = "Gesfredio", Telefone = "99915.8135" });
            Entregadores.Add(new Entregador() { Id = 8, Nome = "Gesfrundio", Telefone = "99916.8166" });
            Entregadores.Add(new Entregador() { Id = 9, Nome = "Kentencio", Telefone = "99988.8888" });
            Entregadores.Add(new Entregador() { Id = 10, Nome = "Gesifrelio", Telefone = "99777.8773" });
        }

        public static EntregadorDAL GetInstance()
        {
            return EntregadorInstance;
        }

        public ObservableCollection<Entregador> GetAll()
        {
            return this.Entregadores;
        }

        public void Add(Entregador entregador)
        {
            this.Entregadores.Add(entregador);
        }
    }
}
