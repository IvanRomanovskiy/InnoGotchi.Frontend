using AutoMapper;
using InnoGotchi.ApiClient.Clients;
using InnoGotchi.ApiClient.Models.Users;
using InnoGotchi.Client.Models;
using InnoGotchi.Frontend.Utilities;
using System.Net.Mail;
using System.Windows.Input;

namespace InnoGotchi.Client.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private LoginModel login;

        private ClientWithoutToken commands;
        private readonly IMapper mapper;
        public LoginViewModel(ClientWithoutToken commands, IMapper mapper)
        {
            login = new LoginModel();
            this.commands = commands;
            this.mapper = mapper;
            Error = "";
            GameViewModel.OnLoggedOut += GameViewModel_OnLoggedOut;
        }

        private void GameViewModel_OnLoggedOut()
        {
            Email = "";
            Password = "";
            Error = "";
            InputIsActive = true;
        }

        public string Email 
        {
            get => login.Email;    
            set
            {
                login.Email = value; 
                OnPropertyChanged();
            }            
        }
        public string Password
        {
            private get => login.Password;
            set => login.Password = value;
        }

        private string error;
        public string Error
        {
            private get => error;
            set
            {
                error = value;
                OnPropertyChanged();
            }
        }

        private bool inputIsActive = true;
        public bool InputIsActive
        {
            get { return inputIsActive; }
            set
            {
                inputIsActive = value;
                OnPropertyChanged();
            }
        }

        public ICommand ButtonEnter_Click
        {
            get
            {
                return new RelayCommand(async (obj) =>
                {
                    Error = "";
                    if (login.Email is "") Error += "Email is empty; ";
                    else
                        try { MailAddress address = new MailAddress(Email); }
                        catch { Error += "Email is wrong; "; }

                    if (login.Password is "") Error += "Password is empty; ";


                    if (error is "")
                    {
                        InputIsActive = false;
                        var token = await commands.GetToken(mapper.Map<GetTokenDto>(login));
                        if (token != null) AccessToken.Token = token;
                        else
                        {
                            Error = "Failed connect to server";
                            InputIsActive = true;
                        }
                    }
                });
            }
        }

    }
}
