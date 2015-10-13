using System;
using System.Xml;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.ComponentModel;
using Rocket.Core;
using Rocket.Core.Logging;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Enumerations;
using Rocket.Unturned.Events;
using Rocket.API;
using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Plugins;
using SDG;
using SDG.Unturned;
using UnityEngine;

namespace P2P
{
	public class Plugin : RocketPlugin<Configuration>
	{
		//globals
		public Dictionary<Steamworks.CSteamID, Steamworks.CSteamID> setPlayers = new Dictionary<Steamworks.CSteamID, Steamworks.CSteamID>();
		public Dictionary<Steamworks.CSteamID, Steamworks.CSteamID> lastReceived = new Dictionary<Steamworks.CSteamID, Steamworks.CSteamID>();

		public static Plugin Instance;

		protected override void Load()
		{
			Instance = this;
			Logger.Log("P2P has been loaded!");
			U.Events.OnPlayerConnected += Events_OnPlayerConnected;
			U.Events.OnPlayerDisconnected += Events_OnPlayerDisconnected;
		}
		protected override void Unload()
		{
			U.Events.OnPlayerConnected -= Events_OnPlayerConnected;
			U.Events.OnPlayerDisconnected -= Events_OnPlayerDisconnected;
			base.Unload ();
		}
		private void Events_OnPlayerConnected(UnturnedPlayer player)
		{
			lastReceived.Add(player.CSteamID, player.CSteamID);
		}
		private void Events_OnPlayerDisconnected(UnturnedPlayer player)
		{
			lastReceived.Remove(player.CSteamID);
		}
	}
	public class Configuration : IRocketPluginConfiguration
	{
		public static Configuration Instance;

		public void LoadDefaults()
		{
			
		}
	}
}
