using AutoMapper;
using InnoGotchi.ApiClient.Clients;
using InnoGotchi.ApiClient.Models.Users;
using InnoGotchi.Client.Components;
using InnoGotchi.Client.Models;
using InnoGotchi.Frontend.Utilities;
using Microsoft.Win32;
using System;
using System.Net.Mail;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace InnoGotchi.Client.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private RegisterModel register;

        private ClientWithoutToken commands;
        private readonly IMapper mapper;
        public RegisterViewModel(ClientWithoutToken commands, IMapper mapper)
        {
            register = new RegisterModel();
            Error = "";
            this.mapper = mapper;
            this.commands = commands;
            GameViewModel.OnLoggedOut += GameViewModel_OnLoggedOut;
        }
        private void GameViewModel_OnLoggedOut()
        {
            InputIsActive = true;
            Email = "";
            FirstName = "";
            LastName = "";
            Error = "";
            Avatar = null;
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

        public string Email
        {
            get => register.Email;
            set => register.Email = value;
        }
        public string FirstName
        {
            get => register.FirstName;
            set => register.FirstName = value;
        }
        public string LastName
        {
            get => register.LastName;
            set => register.LastName = value;
        }
        public string Password
        {
            private get => register.Password;
            set => register.Password = value;
        }

        private string repeatPassword;
        public string RepeatPassword
        {
            private get => repeatPassword;
            set => repeatPassword = value;
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

        public BitmapFrame Avatar 
        {
            get => register.Avatar;
            set
            {
                register.Avatar = value;
                OnPropertyChanged();
            }
        }

        public ICommand ButtonPickAvatar_Click
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    var dialog = new OpenFileDialog()
                    {
                        Filter = "Picture files|*.png;*.jpg"
                    };
                    if (dialog.ShowDialog() == true)
                    {
                        Avatar = BitmapFrame.Create(new BitmapImage(new Uri(dialog.FileName)));
                        
                    }
                });
            }
        }
        public ICommand ButtonEnter_Click
        {
            get
            {
                return new RelayCommand(async (obj) =>
                {
                    Error = "";
                if (register.FirstName is "") Error += "First Name is empty; ";
                if (register.LastName is "") Error += "Last Name is empty; ";
                if (register.Email is "") Error += "Email is empty; ";
                else
                    try { MailAddress address = new MailAddress(Email); }
                    catch { Error += "Email is wrong; "; }

                if (register.Password is "") Error += "Password is empty; ";
                if (register.Password.Length < 8) Error += "Password is small; ";
                if (register.Password != repeatPassword) Error += "Password are not equal";

                if (error is "")
                {
                        InputIsActive = false;
                        if (Avatar == null) Avatar = Properties.Resources.DefaultAvatar.ByteToImage();
                        var e = mapper.Map<CreateUserDto>(register);
                        

                        var result = await commands.CreateUser(e);




                        if (result)
                        {
                            var token = await commands.GetToken(mapper.Map<GetTokenDto>(register));
                            if (token != null) AccessToken.Token = token;
                        }
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
