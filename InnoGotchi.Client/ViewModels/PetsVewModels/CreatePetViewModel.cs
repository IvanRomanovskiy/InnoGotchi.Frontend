using AutoMapper;
using InnoGotchi.ApiClient.Clients;
using InnoGotchi.ApiClient.Models.Pets;
using InnoGotchi.Client.Components;
using InnoGotchi.Client.Models;
using InnoGotchi.Client.Views.PetViews;
using InnoGotchi.Frontend.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace InnoGotchi.Client.ViewModels.PetsVewModels
{
    public class CreatePetViewModel : ViewModelBase
    {
        public delegate void PetCreated();
        public static event PetCreated OnPetCreated;

        private CreatePetModel model;
        public string Name
        {
            get => model.Name;
            set 
            {
                model.Name = value;
                OnPropertyChanged();
            }
        }
        public string BodyPath
        {
            get => model.Appearance.BodyPath;
            set
            {
                model.Appearance.BodyPath = value;
                OnPropertyChanged();
            }
        }
        public string EyesPath
        {
            get => model.Appearance.EyesPath;
            set
            {
                model.Appearance.EyesPath = value;
                OnPropertyChanged();
            }
        }
        public string MouthPath
        {
            get => model.Appearance.MouthPath;
            set
            {
                model.Appearance.MouthPath = value;
                OnPropertyChanged();
            }
        }
        public string NosePath
        {
            get => model.Appearance.NosePath;
            set
            {
                model.Appearance.NosePath = value;
                OnPropertyChanged();
            }
        }

        private PetView currentPage;
        public PetView CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
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


        LinkedList<string> BodiesList = new LinkedList<string>();
        LinkedList<string> EyesList = new LinkedList<string>();
        LinkedList<string> MouthsList = new LinkedList<string>();
        LinkedList<string> NosesList = new LinkedList<string>();
        LinkedListNode<string> BodyNode;
        LinkedListNode<string> EyesNode;
        LinkedListNode<string> MouthNode;
        LinkedListNode<string> NoseNode;

        private PetView PetView;
        private PetClient client;
        private IMapper mapper;
        public CreatePetViewModel(PetClient client, IMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
            InitParts();
            model = new CreatePetModel()
            {
                Appearance = new PetAppearance()
                {
                    BodyPath = BodyNode.Value,
                    EyesPath = EyesNode.Value,
                    MouthPath = MouthNode.Value,
                    NosePath = NoseNode.Value
                }
            };
            PetView = new PetView(model.Appearance);
            CurrentPage = PetView;
        }

        private void InitParts()
        {    
            BodiesList.AddLast(new LinkedListNode<string>(@"/Components/Images/Bodies/body1.svg"));
            BodiesList.AddLast(new LinkedListNode<string>(@"/Components/Images/Bodies/body2.svg"));

            EyesList.AddLast(new LinkedListNode<string>(@"/Components/Images/Eyes/eye1.svg"));
            EyesList.AddLast(new LinkedListNode<string>(@"/Components/Images/Eyes/eye2.svg"));
            EyesList.AddLast(new LinkedListNode<string>(@"/Components/Images/Eyes/eye3.svg"));
            EyesList.AddLast(new LinkedListNode<string>(@"/Components/Images/Eyes/eye4.svg"));

            MouthsList.AddLast(new LinkedListNode<string>(@"/Components/Images/Mouths/mouth1.svg"));
            MouthsList.AddLast(new LinkedListNode<string>(@"/Components/Images/Mouths/mouth2.svg"));
            MouthsList.AddLast(new LinkedListNode<string>(@"/Components/Images/Mouths/mouth3.svg"));
            MouthsList.AddLast(new LinkedListNode<string>(@"/Components/Images/Mouths/mouth4.svg"));
            MouthsList.AddLast(new LinkedListNode<string>(@"/Components/Images/Mouths/mouth5.svg"));

            NosesList.AddLast(new LinkedListNode<string>(@"/Components/Images/Noses/nose1.svg"));
            NosesList.AddLast(new LinkedListNode<string>(@"/Components/Images/Noses/nose2.svg"));
            NosesList.AddLast(new LinkedListNode<string>(@"/Components/Images/Noses/nose3.svg"));
            NosesList.AddLast(new LinkedListNode<string>(@"/Components/Images/Noses/nose4.svg"));
            NosesList.AddLast(new LinkedListNode<string>(@"/Components/Images/Noses/nose5.svg"));
            NosesList.AddLast(new LinkedListNode<string>(@"/Components/Images/Noses/nose6.svg"));

            BodyNode = BodiesList.First;
            EyesNode = EyesList.First;
            MouthNode = MouthsList.First;
            NoseNode = NosesList.First;
        }

        public ICommand ButtonBodyPrev_Click
        {
            get => new RelayCommand((obj) =>
            {
                if (BodyNode.Previous != null) BodyNode = BodyNode.Previous;
                else BodyNode = BodiesList.Last;
                BodyPath = BodyNode.Value;
                CurrentPage.SetNewBodyAppearance(BodyPath);
            });
        }
        public ICommand ButtonBodyNext_Click
        {
            get => new RelayCommand((obj) =>
            {
                if (BodyNode.Next != null) BodyNode = BodyNode.Next;
                else BodyNode = BodiesList.First;
                BodyPath = BodyNode.Value;
                CurrentPage.SetNewBodyAppearance(BodyPath);
            });
        }
        public ICommand ButtonEyesPrev_Click
        {
            get => new RelayCommand((obj) =>
            {
                if (EyesNode.Previous != null) EyesNode = EyesNode.Previous;
                else EyesNode = EyesList.Last;
                EyesPath = EyesNode.Value;
                CurrentPage.SetNewEyesAppearance(EyesPath);
            });
        }
        public ICommand ButtonEyesNext_Click
        {
            get => new RelayCommand((obj) =>
            {
                if (EyesNode.Next != null) EyesNode = EyesNode.Next;
                else EyesNode = EyesList.First;
                EyesPath = EyesNode.Value;
                CurrentPage.SetNewEyesAppearance(EyesPath);
            });
        }
        public ICommand ButtonMouthPrev_Click
        {
            get => new RelayCommand((obj) =>
            {
                if (MouthNode.Previous != null) MouthNode = MouthNode.Previous;
                else MouthNode = MouthsList.Last;
                MouthPath = MouthNode.Value;
                CurrentPage.SetNewMouthAppearance(MouthPath);
            });
        }
        public ICommand ButtonMouthNext_Click
        {
            get => new RelayCommand((obj) =>
            {
                if (MouthNode.Next != null) MouthNode = MouthNode.Next;
                else MouthNode = MouthsList.First;
                MouthPath = MouthNode.Value;
                CurrentPage.SetNewMouthAppearance(MouthPath);
            });
        }
        public ICommand ButtonNosePrev_Click
        {
            get => new RelayCommand((obj) =>
            {
                if (NoseNode.Previous != null) NoseNode = NoseNode.Previous;
                else NoseNode = NosesList.Last;
                NosePath = NoseNode.Value;
                CurrentPage.SetNewNoseAppearance(NosePath);
            });
        }
        public ICommand ButtonNoseNext_Click
        {
            get => new RelayCommand((obj) =>
            {
                if (NoseNode.Next != null) NoseNode = NoseNode.Next;
                else NoseNode = NosesList.First;
                NosePath = NoseNode.Value;
                CurrentPage.SetNewNoseAppearance(NosePath);
            });
        }

        public ICommand ButtonCreate_Click
        {
            get => new RelayCommand(async (obj) =>
            {
                Error = "";
                var command = mapper.Map<CreatePetDto>(model);

                if (await client.CreatePet(command, AccessToken.Token))
                {
                    OnPetCreated.Invoke();
                }
                else Error = "Pet with same name already exists.";

            },(obj) => Name != "");
        }

        
    }
}
