using pilkarzeMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Windows;

namespace pilkarzeMVVM
{
    class ListOfPlayers
    {
        public ObservableCollection<Player> Players { get; set; } = new ObservableCollection<Player>();

        public ListOfPlayers()
        {
            if(File.Exists("PlayersList.json"))
            {
                byte[] jsonbytes = File.ReadAllBytes("PlayersList.json");
                var readOnlySpan = new ReadOnlySpan<byte>(jsonbytes);
                Players = JsonSerializer.Deserialize<ObservableCollection<Player>>(readOnlySpan);
            }
        }

        public void AddPlayer(Player pl)
        {
            Players.Add(pl); 
        }

        public void DeletePlayer(int index)
        {
            if (index < Players.Count && index >= 0)
                Players.RemoveAt(index);
        }

        public void EditPlayer(int index, Player pl)
        {
            if (index < Players.Count && index >= 0)
            {
                Players[index] = pl;
            }
        }

        public void SavePlayers()
        {
            byte[] jsonUtf8Bytes;
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(Players, options);
            File.WriteAllBytes("PlayersList.json", jsonUtf8Bytes);
        }
    }
}
