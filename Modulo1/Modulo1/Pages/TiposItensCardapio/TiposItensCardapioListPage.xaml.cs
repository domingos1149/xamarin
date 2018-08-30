using Modulo1.Dal;
using Modulo1.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Modulo1.Pages.TiposItensCardapio
{
	public partial class TiposItensCardapioListPage : ContentPage
	{
        private TipoItemCardapioDAL dalTipoItemCardapio = new TipoItemCardapioDAL();

		public TiposItensCardapioListPage()
		{
			InitializeComponent();

           // lvTiposItensCardapio.ItemsSource = dalTipoItemCardapio.GetAll(); // comentaar?
		}

        public async void OnRemoverClick(object sender, EventArgs e)
        {
            // objeto recebido como argumento é convertido para MenuItem
            var mi = ((MenuItem)sender);
            var item = mi.CommandParameter as TipoItemCardapio;
            var opcao = await DisplayAlert("Confirmação de exclusão",
                "Confirma excluir o item " + item.Nome.ToUpper() + "?", "Sim", "Não");

            if (opcao)
            { 
                dalTipoItemCardapio.DeleteById((long) item.TipoItemCardapioId);
                dalTipoItemCardapio.GetAll();
            }
        }  

        public async void OnAlterarClick(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var item = mi.CommandParameter as TipoItemCardapio;
            await Navigation.PushModalAsync(new TiposItensCardapioEditPage(item)); 
        }

        // Método invocado cada vez que a página se torna visível
        protected override void OnAppearing()
        {
            base.OnAppearing();        
            lvTiposItensCardapio.ItemsSource = dalTipoItemCardapio.GetAll();
        }

    }
}