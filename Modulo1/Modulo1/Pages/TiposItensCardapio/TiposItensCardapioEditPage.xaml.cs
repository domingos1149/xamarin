using Modulo1.Dal;
using Modulo1.Modelo;
using PCLStorage;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Modulo1.Pages.TiposItensCardapio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TiposItensCardapioEditPage : ContentPage
    {
        private TipoItemCardapio tipoItemCardapio;
        private string caminhoArquivo;
        private TipoItemCardapioDAL dalTiposItensCardapio = new TipoItemCardapioDAL();

        public TiposItensCardapioEditPage(TipoItemCardapio tipoItemCardapio)
        {
            InitializeComponent();
            PopularFormulario(tipoItemCardapio);
            RegistraClickBotaoCamera(idtipoitemcardapio.Text.Trim());
            RegistraClickBotaoAlbum();
        }

        // Esse método está errado na forma que foi escrita no livro
        private void PopularFormulario(TipoItemCardapio tipoItemCardapio)
        {
            this.tipoItemCardapio = tipoItemCardapio;
            idtipoitemcardapio.Text = tipoItemCardapio.TipoItemCardapioId.ToString();
            nome.Text = tipoItemCardapio.Nome;
            //    caminhoArquivo = tipoItemCardapio.Nome;
            //fototipoitemcardapio.Source = ImageSource.FromStream(tipoItemCardapio.Foto);

        }

        private void RegistraClickBotaoAlbum()
        {
            BtnAlbum.Clicked += async (sender, args) =>
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await this.DisplayAlert("Álbum não suportado", "Não existe permissão para acessar o álbum de fotos", "OK");
                    return;
                }

                // abre o álbum
                var file = await CrossMedia.Current.PickPhotoAsync();

                if (file == null)
                    return;

                caminhoArquivo = file.Path;

                fototipoitemcardapio.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;

                });
            };
        }


        private void RegistraClickBotaoCamera(string idparafoto)
        {
            BtnCamera.Clicked += async (sender, args) =>
            {
                // Inicialização do plugin com a cãmera
                await CrossMedia.Current.Initialize();

                // Verifica disponibilidade da câmera
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("Não existe câmera", "A câmera não está disponível.", "OK");
                    return;
                }

                /* Método que habilita a câmera, informando a pasta onde a foto deverá ser armazenada, o nome
                 * a ser dado ao arquivo e se é ou não para armazenar a foto também no álbum */
                var file = await CrossMedia.Current.TakePhotoAsync(
                    new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        Directory = FileSystem.Current.LocalStorage.Name,
                        Name = "tipoitem_" + idparafoto + ".jpg"
                    });

                // Caso o usuário não tenha tirado a foto, clicando no botão cancelar
                if (file == null)
                    return;

                fototipoitemcardapio.Source = ImageSource.FromFile(file.Path);

                caminhoArquivo = file.Path;
            };
        }


        public async void BtnGravarClick(object sender, EventArgs e)
        {
            if (nome.Text.Trim() == string.Empty)
            {
                await this.DisplayAlert("Erro", "Você precisa informar o nome para o novo tipo de itemdo cardápio.", "OK");
            }
            else
            {
                this.tipoItemCardapio.Nome = nome.Text;
             //   this.tipoItemCardapio.CaminhoArquivoFoto = caminhoArquivo;

                dalTiposItensCardapio.Update(this.tipoItemCardapio);

                await Navigation.PopModalAsync();
            }
        }

    }
}
