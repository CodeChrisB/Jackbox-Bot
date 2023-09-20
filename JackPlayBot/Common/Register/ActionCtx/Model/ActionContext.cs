using PlayerBots.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackPlayBot.Common.Register.ActionCtx.Model
{
    public class ActionContext
    {
        public dynamic Data { get; set; }
        public Intelligence Level { get; set;}
        public string state { get; set; }
        public string lobbyState { get; set; }
    }
}
