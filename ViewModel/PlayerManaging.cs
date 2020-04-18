using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pilkarzeMVVM.ViewModel
{
    using Model;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Navigation;
    using ViewModel.BaseClass;

    internal class PlayerManaging : ViewModelBase
    {
        private ListOfPlayers _players = new ListOfPlayers();

        private string _name = "";
        private string _surname = "";
        private int? _age = null;
        private double _weight = 50;

        public string Name {
            get => _name; 
            set
            {
                _name = value;
                onPropertyChanged(nameof(Name));
            }
        }
        public string Surname {
            get => _surname;
            set
            {
                _surname = value;
                onPropertyChanged(nameof(Surname));
            }
        }
        public int? Age {
            get => _age;
            set
            {
                _age = value;
                onPropertyChanged(nameof(Age));
            }
        }
        public double Weight {
            get => Math.Round(_weight, 1);
            set
            {
                _weight = value;
                onPropertyChanged(nameof(Weight));
            }
        }
        public int? SelectedIndex { get; set;
        }

        public int[] AgeList
        {
            get
            {
                int[] age = new int[25];
                for (int i = 0; i < 25; i++)
                    age[i] = i + 16;

                return age;
            }
        }

        public ObservableCollection<Player> Players
        {
            get => _players.Players;
            set
            {
                _players.Players = value;
                onPropertyChanged(nameof(Players));
            }
        }

        #region Dodawanie piłkarza do listy:

        private ICommand _addPlayerToList = null;

        public ICommand AddPlayerToList
        {
            get
            {
                if (_addPlayerToList == null)
                {
                    _addPlayerToList = new RelayCommand(
                        arg => {
                            _players.AddPlayer(new Player(Name, Surname, Math.Round(Weight, 1), (int)Age));
                            Name = "";
                            Surname = "";
                            Age = null;
                            Weight = 50;
                        },
                        arg => (Name != "") && (Surname != "") && (Age != null) && (Weight >= 50)
                        );
                }

                return _addPlayerToList;
            }
        }

        #endregion

        #region Usuniecie Pilkarza z listy:

        private ICommand _deletePlayerFromList = null;

        public ICommand DeletePlayerFromList
        {
            get
            {
                if (_deletePlayerFromList == null)
                {
                    _deletePlayerFromList = new RelayCommand(
                        arg => {
                            _players.DeletePlayer((int)SelectedIndex);
                            Name = "";
                            Surname = "";
                            Age = null;
                            Weight = 50;
                            SelectedIndex = null; onPropertyChanged(nameof(SelectedIndex));
                        },
                        arg => (SelectedIndex != null)
                        );
                }

                return _deletePlayerFromList;
            }
        }

        #endregion

        #region Edytowanie piłkarza:

        private ICommand _editPlayer = null;

        public ICommand EditPlayer
        {
            get
            {
                if(_editPlayer == null)
                {
                    _editPlayer = new RelayCommand(
                        arg => { _players.EditPlayer((int)SelectedIndex, new Player(_name, _surname, Math.Round((double)_weight, 1), (int)_age));
                            Name = "";
                            Surname = "";
                            Age = null;
                            Weight = 50;
                        },
                        arg => (Name != "") && (Surname != "") && (Age != null) && (Weight >= 50) && (SelectedIndex != null)
                        ) ;
                }

                return _editPlayer;
            }
        }
    
        #endregion

        #region Wpisanie danych wybranego piłkarza do formularza

        private ICommand _loadData = null;

        public ICommand LoadData
        {
            get
            {
                if (_loadData == null)
                {
                    _loadData = new RelayCommand(
                        arg =>
                        {
                            if((int)SelectedIndex < _players.Players.Count && (int)SelectedIndex >= 0)
                            {
                                Name = _players.Players[(int)SelectedIndex].Name; onPropertyChanged(nameof(Name));
                                Surname = _players.Players[(int)SelectedIndex].Surname; onPropertyChanged(nameof(Surname));
                                Age = AgeList[_players.Players[(int)SelectedIndex].Age - 16]; onPropertyChanged(nameof(Age));
                                Weight = _players.Players[(int)SelectedIndex].Weight; onPropertyChanged(nameof(Weight));
                            }
                        },
                        arg => (SelectedIndex != null)
                        );
                }

                return _loadData;
            }
        }

        #endregion


        #region Zapis obiektow do plików:

        private ICommand _saveObjects = null;

        public ICommand SaveObjects
        {
            get
            {
                if(_saveObjects == null)
                {
                    _saveObjects = new RelayCommand(x => _players.SavePlayers());
                }
                return _saveObjects;
            }
        }

        #endregion

    }
}
