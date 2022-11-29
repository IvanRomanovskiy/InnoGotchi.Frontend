using InnoGotchi.ApiClient.Clients;
using InnoGotchi.ApiClient.Models.Farm;
using InnoGotchi.Client.Views;
using InnoGotchi.Frontend.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InnoGotchi.Client.ViewModels.FarmViewModels
{
    public class FarmDetailsViewModel : ViewModelBase
    {
        public delegate void PetButtonPressed();
        public delegate void StatButtonPressed();
        public static event PetButtonPressed OnPetButtonPressed;
        public static event StatButtonPressed OnStatButtonPressed;

        private readonly FarmClient client;

        AddCollaboratorDto addCollaborator;

        public string Email
        {
            get => addCollaborator.CollaboratorEmail;
            set
            {
                addCollaborator.CollaboratorEmail = value;
                OnPropertyChanged();
            }
        }
        private string error = "";

        public string Error
        {
            get => error;
            set
            {
                error = value;
                OnPropertyChanged();
            }
        }

        public FarmDetailsViewModel(FarmClient client)
        {
            this.client = client;
            addCollaborator = new AddCollaboratorDto();
        }

        public ICommand ButtonAdd_Click
        {
            get => new RelayCommand(async (obj) =>
            {
                Error = "";
                if (Email is "") Error += "Email is empty; ";
                else
                    try { MailAddress address = new MailAddress(Email); }
                    catch { Error += "Email is wrong; "; }
                if (error is "")
                {
                    if (await client.AddCollaborator(addCollaborator, AccessToken.Token))
                    {
                        Error = "Success";
                    }
                    else Error = "Something gone wrong";
                }
            });
        }
        public ICommand ButtonPets_Click
        {
            get => new RelayCommand((obj) =>
            {
                OnPetButtonPressed.Invoke();
            });
        }
        public ICommand ButtonStat_Click
        {
            get => new RelayCommand((obj) =>
            {
                OnStatButtonPressed.Invoke();
            });
        }

    }
}
