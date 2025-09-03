using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppRpgEtec.Models;
using AppRpgEtec.Services.Personagens;
using AppRpgEtec.Models.Enuns;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppRpgEtec.ViewModels.Personagens
{
    public class CadastroPersonagemViewModel : BaseViewModel
    {
        private PersonagemService pService;
        public ICommand SalvarCommand { get; }

        public CadastroPersonagemViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new PersonagemService(token);
            _ = ObterClasses();

            SalvarCommand = new Command(async () => { await SalvarPersonagem(); });
        }        

        private int id;
        private string nome;
        private int pontosVida;
        private int forca;
        private int defesa;
        private int inteligencia;
        private int disputas;
        private int vitorias;
        private int derrotas;

        private ObservableCollection<TipoClasse> listaTipoClasse;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        private TipoClasse tipoClasseSelecionado;
        public TipoClasse TipoClasseSelecionado
        {
            get { return tipoClasseSelecionado; }
            set
            {
                if (value != null)
                {
                    tipoClasseSelecionado = value;
                    OnPropertyChanged();
                }
            }
        }
    
        public ObservableCollection<TipoClasse> ListaTipoClasse
        {
            get { return listaTipoClasse; }
            set
            {
                if (value != null)
                {
                    listaTipoClasse = value;
                    OnPropertyChanged();
                }
            }
        }

        public async Task ObterClasses()
        {
            try
            {
                ListaTipoClasse = new ObservableCollection<TipoClasse>();
                ListaTipoClasse.Add(new TipoClasse() { Id = 1, Descricao = "Cavaleiro" });
                ListaTipoClasse.Add(new TipoClasse() { Id = 2, Descricao = "Mago" });
                ListaTipoClasse.Add(new TipoClasse() { Id = 3, Descricao = "Clerigo" });
                OnPropertyChanged(nameof(ListaTipoClasse));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task SalvarPersonagem()
        {
            try
            {
                Personagem model = new Personagem()
                {
                    Nome = this.nome,
                    PontosVida = this.pontosVida,
                    Defesa = this.defesa,
                    Derrotas = this.derrotas,
                    Disputas = this.disputas,
                    Forca = this.forca,
                    Inteligencia = this.inteligencia,
                    Vitorias = this.vitorias,
                    Id = this.id,
                    Classe = (ClasseEnum)tipoClasseSelecionado.Id
                };
                if (model.Id == 0)
                    await pService.PostPersonagemAsync(model);

                await Application.Current.MainPage.DisplayAlert("Mensagem", "Dados salvos cm sucesso!", "Ok");
                await Shell.Current.GoToAsync(".."); //Remove a página atual da pilha de páginas
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public string Nome { get => nome; set => nome = value; }
        public int PontosVida { get => pontosVida; set => pontosVida = value; }
        public int Forca { get => forca; set => forca = value; }
        public int Defesa { get => defesa; set => defesa = value; }
        public int Inteligencia { get => inteligencia; set => inteligencia = value; }
        public int Disputas { get => disputas; set => disputas = value; }
        public int Vitorias { get => vitorias; set => vitorias = value; }
        public int Derrotas { get => derrotas; set => derrotas = value; }
    }
}
