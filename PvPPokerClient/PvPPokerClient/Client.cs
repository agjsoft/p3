using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.WebSockets;
using ScatterSharp;
using P3Packet;

namespace PvPPokerClient
{
    public static class Client
    {
        public static string mApplicationTitle = "PvP Poker v1.00";
        public static string mScatterTitle = "PvP Poker";
        public static Scatter mScatter;
        public static ScatterSharp.Api.Network mNetwork;
        public static AClient mAClient;

        public static void Init()
        {
            mNetwork = new ScatterSharp.Api.Network()
            {
                Blockchain = Scatter.Blockchains.EOSIO,
                Host = "api.eosnewyork.io",
                Port = 443,
                Protocol = "https",
                ChainId = "aca376f206b8fc25a6ed44dbdc66547c36c6c33e3a119ffbeaef943642f0e906"
            };
        }

        public static async void Login()
        {
            mScatter = new Scatter(mScatterTitle, mNetwork);

            try
            {
                await mScatter.Connect();
            }
            catch (WebSocketException ex)
            {
                string errorMsg = "";
                switch (ex.ErrorCode)
                {
                    case 10061:
                        errorMsg = "데스크탑 스캐터가 실행중이지 않습니다.";
                        break;
                    default:
                        errorMsg = ex.Message;
                        break;
                }
                Form1.tbConsoleQueue.Enqueue(errorMsg);
                Form1.btnLoginQueue.Enqueue(true);
                return;
            }

            var identity = await mScatter.GetIdentity(new ScatterSharp.Api.IdentityRequiredFields()
            {
                Accounts = new List<ScatterSharp.Api.Network>()
                {
                    mNetwork
                },
                Location = new List<ScatterSharp.Api.LocationFields>(),
                Personal = new List<ScatterSharp.Api.PersonalFields>()
            });
            string accountName = identity.Accounts[0].Name;
            Form1.tbConsoleQueue.Enqueue("PvP Poker 서버에 " + accountName + " 계정으로 로그인중입니다.");

            var sendPacket = new LoginReqPacket()
            {
                AccountName = accountName
            };
            mAClient.Send(sendPacket);
        }
    }
}