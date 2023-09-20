using JackPlayBot.Common.Register.ActionCtx.Model;
using Newtonsoft.Json;
using PlayerBots.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackPlayBot.Common.Register.ActionCtx.Helper
{
    internal class ActionContextBuilder
    {
        public static ActionContext Build(string recievedData, Intelligence level)
        {
            ActionContext context = new ActionContext();
            dynamic jsonData = JsonConvert.DeserializeObject(recievedData);
            Console.Write(jsonData);

            dynamic room = PathVal.Get<dynamic>(jsonData, "result", "entities", "bc:room", "1");
            context.state = PathVal.Get<string>(room, "val", "state");



            //context.Meta = DynamicParser.DynamicToStructConverter<MetaInfo>(jsonData.)
            return context;
        }
    }
}
