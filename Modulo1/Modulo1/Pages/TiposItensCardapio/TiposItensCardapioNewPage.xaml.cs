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
   // [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TiposItensCardapioNewPage : ContentPage
    {
        private TipoItemCardapioDAL dalTiposItensCardapio = new TipoItemCardapioDAL();
        private byte[] bytesFoto; 

        public TiposItensCardapioNewPage()
        {
            InitializeComponent();
            PreparaParaNovoTipoItemCardapio();
            RegistraClickBotaoCamera(idtipoitemcardapio.Text.Trim());
            RegistraClickBotaoAlbum();
        }

        private void PreparaParaNovoTipoItemCardapio()
        {
            var novoId = dalTiposItensCardapio.GetAll().Max(x => x.TipoItemCardapioId) + 1;
            idtipoitemcardapio.Text = novoId.ToString().Trim();
            nome.Text = string.Empty;
            fototipoitemcardapio.Source = null;
        }

        private void RegistraClickBotaoCamera(string idparafoto)
        {
            // Criação do método anômimo para captura do evento click do botão
            BtnCamera.Clicked += async (sender, args) =>
            {
                // Inicialização do plugin de interação com a câmera e álbum
                await CrossMedia.Current.Initialize();

                // Verificação de disponibilização da câmera e se é possível tirar foto
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
                        Name = "tipoitem_" + idparafoto + ".jpg",
                        SaveToAlbum = true
                    });

                // Caso o usuário não tenha tirado a foto, clicando no botão cancelar
                if (file == null)
                    return;

                var stream = file.GetStream();
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);

                // Recupera a foto e atribui para o controle visual
                fototipoitemcardapio.Source = ImageSource.FromStream(() => 
                {
                    var s = file.GetStream();
                    file.Dispose(); // recurso não gerenciado pelo VCL, deve ser desalocado
                    return s;
                });
                bytesFoto = memoryStream.ToArray();
            };            
        }


        private void RegistraClickBotaoAlbum()
        {
            // Criação do método anônimo para captura do evento click do botão
            BtnAlbum.Clicked += async (sender, args) =>
            {
                // Inicialização do plugin de interação com a câmera e álbum
                await CrossMedia.Current.Initialize();

                // Verificação da disponibilização do álbum
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Álbum não suportado", "Não existe permissão para acessar o álbum de fotos", "OK");
                    return;
                }

                // Método que habilita o álbum e permite seleção de uma foto
                var file = await CrossMedia.Current.PickPhotoAsync();

                // Caso o usuário não tenha selecionado uma foto, clicando no botão cancelar
                if (file == null)
                    return;

                var stream = file.GetStream();
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);

                fototipoitemcardapio.Source = ImageSource.FromStream(() => 
                {
                    var s = file.GetStream();
                    file.Dispose();
                    return s;
                });
                bytesFoto = memoryStream.ToArray();

            };     
        }


        public void BtnGravarClick(object sender, EventArgs e)
        {
            if (nome.Text.Trim() == string.Empty)
            {
                this.DisplayAlert("Erro", "Você precisa informar o nome para o novo tipo de item do cardápio.", "OK");
            }
            else
            {
                dalTiposItensCardapio.Add(new TipoItemCardapio()
                {
                    Nome = nome.Text,
                    Foto = bytesFoto
                });
                PreparaParaNovoTipoItemCardapio();
            }
        }
    }
}