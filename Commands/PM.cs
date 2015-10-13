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
	public class PMCommand : IRocketCommand
	{
		public void Execute(IRocketPlayer caller, string[] command)
		{
			int cLength = command.Length;
			UnturnedPlayer cPlayer = (UnturnedPlayer)caller;
			UnturnedPlayer target;

			if (command.Length < 1)
			{

			}
			else if (command.Length == 1)
			{
				if (command[0].ToString().ToLower() == "set")
				{
					Rocket.Unturned.Chat.UnturnedChat.Say (cPlayer, "Cannot leave player name empty!", Color.yellow);
				}
				if (command[0].ToString().ToLower() != "set")
				{
					//send single message to set recipient, command[0] is message
					string messageText = command[0].ToString().ToLower();
					if (P2P.Plugin.Instance.setPlayers.ContainsKey(cPlayer.CSteamID))
					{
						Steamworks.CSteamID setPlayerSID = P2P.Plugin.Instance.setPlayers[cPlayer.CSteamID];
						UnturnedPlayer setPlayer = UnturnedPlayer.FromCSteamID(setPlayerSID);

						Rocket.Unturned.Chat.UnturnedChat.Say (cPlayer, cPlayer.CharacterName + ": " + messageText, Color.blue);
						Rocket.Unturned.Chat.UnturnedChat.Say (setPlayer, cPlayer.CharacterName + ": " + messageText, Color.cyan);
						P2P.Plugin.Instance.lastReceived[setPlayerSID] = cPlayer.CSteamID;
					}
					else
					{
						Rocket.Unturned.Chat.UnturnedChat.Say (cPlayer, "You do not have a player set for quick PMs!", Color.yellow);
					}
				}
			}
			else if (command.Length == 2)
			{
				target = UnturnedPlayer.FromName(command[1].ToString());
				if (command[0].ToString().ToLower() == "set" && target != null)
				{
					//set recipient
					if (P2P.Plugin.Instance.setPlayers.ContainsKey(cPlayer.CSteamID))
					{
						P2P.Plugin.Instance.setPlayers[cPlayer.CSteamID] = target.CSteamID;
					}
					else if (!P2P.Plugin.Instance.setPlayers.ContainsKey(cPlayer.CSteamID))
					{
						P2P.Plugin.Instance.setPlayers.Add(cPlayer.CSteamID, target.CSteamID);
					}
					Rocket.Unturned.Chat.UnturnedChat.Say (cPlayer, target.CharacterName + " has been set for quick PMs!", Color.green);
				}
				else if (command[0].ToString().ToLower() == "set" && target == null)
				{
					Rocket.Unturned.Chat.UnturnedChat.Say (cPlayer, "Player does not exist or is offline!", Color.yellow);
				}
				else if (command[0].ToString().ToLower() != "set")
				{
					target = UnturnedPlayer.FromName(command[0].ToString());
					if (target != null)
					{
						//send single message to unset recipient, command[1] is message
						string messageText = command[1].ToString();

						Rocket.Unturned.Chat.UnturnedChat.Say (cPlayer, cPlayer.CharacterName + ": " + messageText, Color.blue);
						Rocket.Unturned.Chat.UnturnedChat.Say (target, cPlayer.CharacterName + ": " + messageText, Color.cyan);
						P2P.Plugin.Instance.lastReceived[target.CSteamID] = cPlayer.CSteamID;
					}
					else if (target == null)
					{
						Rocket.Unturned.Chat.UnturnedChat.Say (cPlayer, "Player does not exist or is offline!", Color.yellow);
					}
				}
			}
			else if (command.Length > 2)
			{
				target = UnturnedPlayer.FromName(command[0].ToString());
				if (target == null)
				{
					//send multi message to set recipient
					string messageText = "";
					for (int i = 0; i < cLength; i++)
					{
						messageText = messageText + command[i].ToString() + " ";
					}
					if (P2P.Plugin.Instance.setPlayers.ContainsKey(cPlayer.CSteamID))
					{
						Steamworks.CSteamID setPlayerSID = P2P.Plugin.Instance.setPlayers[cPlayer.CSteamID];
						UnturnedPlayer setPlayer = UnturnedPlayer.FromCSteamID(setPlayerSID);

						Rocket.Unturned.Chat.UnturnedChat.Say (cPlayer, cPlayer.CharacterName + ": " + messageText, Color.blue);
						Rocket.Unturned.Chat.UnturnedChat.Say (setPlayer, cPlayer.CharacterName + ": " + messageText, Color.cyan);
						P2P.Plugin.Instance.lastReceived[setPlayerSID] = cPlayer.CSteamID;
					}
					else
					{
						Rocket.Unturned.Chat.UnturnedChat.Say (cPlayer, "You do not have a player set for quick PMs!", Color.yellow);
					}
				}
				else if (target != null)
				{
					//send multi message to unset recipient
					string messageText = "";
					for (int i = 1; i < cLength; i++)
					{
						messageText = messageText + command[i].ToString() + " ";
					}
					Rocket.Unturned.Chat.UnturnedChat.Say (cPlayer, cPlayer.CharacterName + ": " + messageText, Color.blue);
					Rocket.Unturned.Chat.UnturnedChat.Say (target, cPlayer.CharacterName + ": " + messageText, Color.cyan);
					P2P.Plugin.Instance.lastReceived[target.CSteamID] = cPlayer.CSteamID;
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
			get { return "p2p.pm"; }
		}
		public List<string> Aliases
		{
			get { return new List<string> { "pm" }; }
		}
		public string Syntax
		{
			get
			{
				return "<player name> <message>";
			}
		}
		public string Help
		{
			get { return "Enter text to message your set receiver."; }
		}
		public List<string> Permissions
		{
			get
			{
				return new List<string>() { 
					"p2p.pm"
				};
			}
		}
	}

}