using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EosSharp;
using Newtonsoft.Json;

namespace PvPPokerServer
{
    public partial class Form1 : Form
    {
        public static ConcurrentQueue<string> RecvEOSQueue = new ConcurrentQueue<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            Task.Run(() => Work());
        }

        private T Wait<T>(Task<T> a)
        {
            while (!a.IsCompleted)
                Thread.Sleep(1);
            if (a.IsFaulted)
                return default(T);
            else
                return a.Result;
        }

        private void Work()
        {
            string account = "agj4navercom";
            var eos = new Eos(new EosConfigurator()
            {
                HttpEndpoint = "https://nodes.get-scatter.com:443",
                ChainId = "aca376f206b8fc25a6ed44dbdc66547c36c6c33e3a119ffbeaef943642f0e906",
                ExpireSeconds = 60000,
                //SignProvider = new DefaultSignProvider( "myprivatekey")
            });

            var a0 = Wait(eos.GetActions(account));
            int actionSeq = (int)a0.Actions.Last().AccountActionSeq + 1;

            while (true)
            {
                var a2 = Wait(eos.GetActions(account, actionSeq, 0));
                if (null == a2)
                {
                    Thread.Sleep(1100);
                }
                else if (0 == a2.Actions.Count)
                {
                    Thread.Sleep(200);
                }
                else
                {
                    foreach (var action in a2.Actions)
                    {
                        if (action.ActionTrace.Act.Account != "eosio.token")
                            continue;

                        if (action.ActionTrace.Act.Name != "transfer")
                            continue;

                        var body = JsonConvert.DeserializeObject<TransferBody>(action.ActionTrace.Act.Data.ToString());
                        if (body.to != account)
                            continue;

                        string quantity = body.quantity.Split(' ')[0];
                        RecvEOSQueue.Enqueue($"From:{body.from}, Quantity:{quantity}, Memo:{body.memo}");
                    }
                    actionSeq++;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string dat;
            if (false == RecvEOSQueue.TryDequeue(out dat))
                return;

            lbRecvEOS.Items.Add(dat);
        }
    }

    public class TransferBody
    {
        public string from;
        public string to;
        public string quantity;
        public string memo;
    }
}