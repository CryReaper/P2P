using System;
using System.Data;
using System.Xml;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Windows;
using Rocket.API;
using Rocket.Unturned;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using UnityEngine;

namespace P2PCommands
{
	public class RCommand : IRocketCommand
	{
		public void Execute(IRocketPlayer caller, string[] command)
		{
			UnturnedPlayer cPlayer = (UnturnedPlayer)caller;
			UnturnedPlayer target;
			if (command.Length < 1)
			{

			}
			else if (command.Length == 1)
			{
				if (cPlayer.CSteamID != P2P.Plugin.Instance.lastReceived[cPlayer.CSteamID])
				{
					int cLength = command.Length;
					string messageText = command[0].ToString();

					target = UnturnedPlayer.FromCSteamID(P2P.Plugin.Instance.lastReceived[cPlayer.CSteamID]);
					Rocket.Unturned.Chat.UnturnedChat.Say (cPlayer, cPlayer.CharacterName + ": " + messageText, Color.blue);
					Rocket.Unturned.Chat.UnturnedChat.Say (target, cPlayer.CharacterName + ": " + messageText, Color.cyan);
				}
				else if (cPlayer.CSteamID == P2P.Plugin.Instance.lastReceived[cPlayer.CSteamID])
				{
					Rocket.Unturned.Chat.UnturnedChat.Say (cPlayer, "You have not received a pm!", Color.yellow);
				}
			}
			else if (command.Length > 1)
			{
				if (cPlayer.CSteamID != P2P.Plugin.Instance.lastReceived[cPlayer.CSteamID])
				{
					int cLength = command.Length;
					string messageText = "";
					for (int i = 0; i < cLength; i++)
					{
						messageText = messageText + command[i].ToString();
					}
					target = UnturnedPlayer.FromCSteamID(P2P.Plugin.Instance.lastReceived[cPlayer.CSteamID]);
					Rocket.Unturned.Chat.UnturnedChat.Say (cPlayer, cPlayer.CharacterName + ": " + messageText, Color.blue);
					Rocket.Unturned.Chat.UnturnedChat.Say (target, cPlayer.CharacterName + ": " + messageText, Color.cyan);
				}
				else if (cPlayer.CSteamID == P2P.Plugin.Instance.lastReceived[cPlayer.CSteamID])
				{
					Rocket.Unturned.Chat.UnturnedChat.Say (cPlayer, "You have not received a pm!", Color.yellow);
				}
			}
		}
		public bool AllowFromConsole
		{
			get
			{
				return false;
			}
		}
		public bool RunFromConsole
		{
			get { return false; }
		}
		public string Name
		{
			get { return "p2p.r"; }
		}
		public List<string> Aliases
		{
			get { return new List<string> { "r" }; }
		}
		public string Syntax
		{
			get
			{
				return "<message>";
			}
		}
		public string Help
		{
			get { return "Enter text to reply to last received message."; }
		}
		public List<string> Permissions
		{
			get
			{
				return new List<string>() { 
					"p2p.r"
				};
			}
		}
	}

}